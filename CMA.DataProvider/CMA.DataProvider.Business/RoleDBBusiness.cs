using CMA.Common.Model;
using CMA.DataProvider.DataOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.DataProvider.Business
{
    public class RoleDBBusiness
    {

        public IEnumerable<RoleModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_role> tRole = new DOBase<tb_role>(context);
                return tRole.Where(r => true).ToList().Select(n => GetModel(n));
            }
        }
        public RoleModel Get(long id)
        {
            RoleModel result = null;
            using (var context = new db_cmaEntities())
            {
                //DOBase<tb_role> tRole = new DOBase<tb_role>(context);

                result = GetModel(context.tb_role.FirstOrDefault(n => n.Id == id), context.tb_role.FirstOrDefault(n => n.Id == id).tb_action.ToList());
            }
            return result;
        }

        public bool Delete(int id)
        {
            var temp = false;
            using (var context = new db_cmaEntities())
            {
                DOBase<tb_role> tRole = new DOBase<tb_role>(context);
                tRole.Delete(id);

                temp = tRole.SaveChang() > 0 ? true : false;


            }
            return temp;
        }
        public bool Add(RoleModel model)
        {
            var temp = false;
            using (var context = new db_cmaEntities())
            {
                //DOBase<tb_role> tRole = new DOBase<tb_role>(context);
                //tRole.Add(GetModel(model));
                tb_role dbmodel = new tb_role()
                {                   
                    Name = model.Name,
                    Description = model.Description                   
                };
                context.tb_role.Add(dbmodel);
                context.SaveChanges();
                if (model.ActionList.Count()>0)
                {
                    dbmodel.tb_action.Clear();
                    dbmodel.tb_action = context.tb_action.Where(n => model.ActionList.Contains(n.Id)).ToList();
                }
                //context.Configuration.ValidateOnSaveEnabled = false;
                temp = context.SaveChanges() > 0 ? true : false;
                //context.Configuration.ValidateOnSaveEnabled = true;

            }
            return temp;
        }

        private RoleModel GetModel(tb_role dbModel)
        {
            return new RoleModel()
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                cFree = dbModel.cFree,
                iFree = dbModel.iFree
            };
        }

        private RoleModel GetModel(tb_role dbModel, List<tb_action> actionList)
        {
            var actionModelList = new List<long>();
            foreach (var item in actionList)
            {
                actionModelList.Add(item.Id);
            } 

            return new RoleModel()
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                cFree = dbModel.cFree,
                iFree = dbModel.iFree,
                ActionList = actionModelList
            };
        }

        private tb_role GetModel(RoleModel dbModel)
        {
            return new tb_role()
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                //iFree = dbModel.iFree,
                cFree = dbModel.cFree
            };
        }

        public bool Edit(RoleModel model)
        {
            bool result = false;
            using (var context = new db_cmaEntities())
            {
                var dbModel = context.tb_role.First(n => n.Id == model.Id);
                dbModel.Name = model.Name;
                dbModel.cFree = model.cFree;
                dbModel.Description = model.Description;
                if (model.ActionList.Count() > 0)
                {
                    dbModel.tb_action.Clear();
                    dbModel.tb_action = context.tb_action.Where(n => model.ActionList.Contains(n.Id)).ToList();
                }

                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                context.Configuration.ValidateOnSaveEnabled = true;
                result = true;
            }
            return result;
        }
    }
}
