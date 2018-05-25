using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.DataProvider.DataOperator;

namespace CMA.DataProvider.Business
{
    public class ActionDBBusiness
    {
        public IEnumerable<ActionModel> GetList()
        {
            using (var context = new db_cmaEntities())
            {
                var dbModelList = context.tb_action.ToList();
                return dbModelList.Select(n => GetModel(n));
            }
        }

        public ActionModel GetModel(tb_action n)
        {
            return new ActionModel()
            {
                cFree = n.cFree,
                Description = n.Description,
                Id = n.Id,
                iFree = n.iFree,
                Name = n.Name
            };
        }

        internal IEnumerable<ActionModel> GetListFromRole(long id)
        {
            using (var context = new db_cmaEntities())
            {
                //var dbModelList = context.tb_action.Where(n=>n.).ToList();
                //return dbModelList.Select(n => GetModel(n));
            }
            return null;
        }
    }
}
