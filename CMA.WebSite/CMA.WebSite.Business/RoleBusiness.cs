using CMA.Common.Model;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.Business
{
    public class RoleBusiness
    {
        private RoleDBClient _dbCleint = new RoleDBClient();


        public IEnumerable<RoleModel> GetList()
        {
            return _dbCleint.GetList();
        }

        public RoleModel Get(long id)
        {
            var model = _dbCleint.Get(id);
            if (model == null)
            {
                throw new ApplicationException("角色不存在");
            }
            else
            {
                return model;
            }
        }

        public void Edit(RoleModel model)
        {
            bool result = _dbCleint.Edit(model);
            if (!result)
            {
                throw new ApplicationException("修改失败");
            }
        }
        public void Add(RoleModel Rmodel)
        {
            bool result = _dbCleint.Add(Rmodel);
            if (result == false)
            {
                throw new AppException("添加失败！");
            }
        }
        public void Delete(int id)
        {
            bool result = _dbCleint.Delete(id);
            if (result == false)
            {
                throw new AppException("删除失败！");
               
            }
        }


    }
}
