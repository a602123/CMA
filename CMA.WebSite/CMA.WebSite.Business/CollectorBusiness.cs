using CMA.Common.Model;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.Business
{
    public class CollectorBusiness
    {
        private CollectorDBClient _dbCleint = new CollectorDBClient();

        public IEnumerable<CollectorDBModel> GetList()
        {
            return _dbCleint.GetList();
        }
        public void Add(CollectorDBModel model)
        {
            var result=_dbCleint.Add(model);
            if (result==false)
            {
                throw new AppException("添加用户失败!");
            }
        }
    }
}
