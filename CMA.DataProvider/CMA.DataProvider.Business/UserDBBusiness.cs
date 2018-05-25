using CMA.Common.Model;
using CMA.DataProvider.DataOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.DataProvider.Business
{
    public class UserDBBusiness
    {
        public UserInfoModel Get(string username)
        {
            UserInfoModel result = null;
            using (var context = new db_cmaEntities())
            {

                var dbModel = context.tb_user.Where(n => n.Username == username).FirstOrDefault();
                if (dbModel != null)
                {
                    result = GetModel(dbModel);
                }


            }
            return result;
        }

        public UserInfoModel Get(long id)
        {
            UserInfoModel result = null;
            using (var context = new db_cmaEntities())
            {
                var dbModel = context.tb_user.Where(n => n.Id == id).FirstOrDefault();
                if (dbModel != null)
                {
                    result = GetModel(dbModel, context.tb_user.Where(n => n.Id == id).FirstOrDefault().tb_role);
                }
            }
            return result;
        }

        private UserInfoModel GetModel(tb_user dbModel, ICollection<tb_role> tb_role)
        {
            var roleList = new List<long>();
            foreach (var item in tb_role)
            {
                roleList.Add(item.Id);
            }

            return new UserInfoModel()
            {
                Email = dbModel.Email,
                Id = dbModel.Id,
                Password = dbModel.Password,
                RealName = dbModel.RealName,
                Telphone = dbModel.Telphone,
                Username = dbModel.Username,
                State = dbModel.State,
                RoleList = roleList
            };
        }

        public bool Add(UserInfoModel model)
        {
            bool result = false;
            try
            {
                using (var context = new db_cmaEntities())
                {
                    tb_user dbmodel = new tb_user()
                    {
                        Email = model.Email,
                        Password = model.Password,
                        RealName = model.RealName,
                        Telphone = model.Telphone,
                        Username = model.Username,
                        State = model.State,
                    };
                    context.tb_user.Add(dbmodel);
                    context.SaveChanges();
                    //dbmodel = context.tb_user.Where(n => n.Id == dbmodel.Id).FirstOrDefault();
                    if (model.RoleList.Count > 0)
                    {
                        dbmodel.tb_role.Clear();
                        dbmodel.tb_role = context.tb_role.Where(n => model.RoleList.Contains(n.Id)).ToList();
                    }
                    result = context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 检查用户名是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns>fasle--用户名可用 无重复</returns>
        /// <returns>true --用户名不可用 有重复</returns>
        public bool IsExit(UserInfoModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                var db = context.tb_user.Where(n => n.Username == model.Username).Count();
                result = db == 0 ? false : true;
            }
            return result;
        }

        public IEnumerable<UserInfoModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                //var dbModelList = context.tb_user.ToList();
                var dbModelList = context.tb_user.Where(u => u.State == 0).ToList();
                return dbModelList.Select(n => GetModel(n));
            }
        }

        public bool Edit(UserInfoModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                var dbModel = context.tb_user.First(n => n.Id == model.Id);
                dbModel.Email = model.Email;
                dbModel.RealName = model.RealName;
                dbModel.Telphone = model.Telphone;

                if (model.RoleList.Count >0)
                {
                    dbModel.tb_role.Clear();
                    dbModel.tb_role = context.tb_role.Where(n => model.RoleList.Contains(n.Id)).ToList();
                }

                //context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                //context.Configuration.ValidateOnSaveEnabled = true;
                result = true;
            }
            return result;
        }

        public bool Delete(long id)
        {
            bool result = false;
            try
            {
                using (var context = new db_cmaEntities())
                {
                    var dbmodel = context.tb_user.First(u => u.Id == id);
                    if (dbmodel.State != 1)
                    {
                        dbmodel.State = 1;
                        context.Configuration.ValidateOnSaveEnabled = false;
                        result = context.SaveChanges() > 0;
                        context.Configuration.ValidateOnSaveEnabled = true;
                    }
                    else
                    {
                        return true;//已删除
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public bool RestPassword(UserInfoModel model)
        {
            using (var context = new db_cmaEntities())
            {
                var dbmodel = context.tb_user.Where(u => u.Id == model.Id).FirstOrDefault();
                if (dbmodel == null)
                {

                }

                dbmodel.Password = model.Password;
                return context.SaveChanges()>0;
            }
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="dbModel">dbModel装换为UserInfoModel</param>
        /// <returns></returns>
        private UserInfoModel GetModel(tb_user dbModel)
        {
            return new UserInfoModel()
            {
                Email = dbModel.Email,
                Id = dbModel.Id,
                Password = dbModel.Password,
                RealName = dbModel.RealName,
                Telphone = dbModel.Telphone,
                Username = dbModel.Username,
                State = dbModel.State
            };
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="userInfoModel">userInfo转换为dbModel</param>
        /// <returns></returns>
        private tb_user GetModel(UserInfoModel userInfoModel)
        {
            return new tb_user()
            {

                Email = userInfoModel.Email,
                Password = userInfoModel.Password,
                RealName = userInfoModel.RealName,
                Telphone = userInfoModel.Telphone,
                Username = userInfoModel.Username,
                State = userInfoModel.State,

            };
        }


    }
}
