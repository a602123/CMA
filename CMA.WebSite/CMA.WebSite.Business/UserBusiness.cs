using CMA.Common.Model;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.EncryptionHelper;
using System.Reflection;

namespace CMA.WebSite.Business
{
    public class UserBusiness
    {
        private UserDBClient _dbCleint = new UserDBClient();


        public UserInfoModel Get(long id)
        {
            var model = _dbCleint.Get(id);
            if (model == null)
            {
                throw new AppException("用户不存在！");
            }
            else
            {
                return model;
            }
        }

        public void Edit(UserInfoModel model)
        {
            bool result = _dbCleint.Edit(model);
            if (result == false)
            {
                throw new AppException("修改失败！");
            }
        }

        public void Add(UserInfoModel model)
        {
            //添加时添加 添加用户的默认属性
            model.Password = Encryption.GetInstance().MD5Encrypt(model.Password);
            model.State = 0;
            var reslut = _dbCleint.Add(model);
            if (reslut == Convert.ToInt32(AddUserError.Error))
            {
                throw new AppException("未能成功添加用户！");
            }
            else if(reslut==Convert.ToInt32(AddUserError.UserNameExits))
            {
                throw new AppException("用户名已存在！");
            }            
        }       
        public void Delete(long id)
        {
            var result = _dbCleint.Delete(id);
            if (result == false)
            {
                throw new AppException("删除失败！");
            }
        }
        public void RestPassword(long id)
        {
            //var result = _dbCleint.RestPassword(model);           
            var restpassword = "111111";
            UserInfoModel model = new UserInfoModel()
            {
                Id = id,
                Password = Encryption.GetInstance().MD5Encrypt(restpassword)
            };
             _dbCleint.RestPassword(model);
           
        }
        public IEnumerable<UserInfoModel> GetList()
        {
            return _dbCleint.GetList();
        }
    }
}
