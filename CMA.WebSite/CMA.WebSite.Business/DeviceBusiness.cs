using CMA.Common.Model;
using CMA.WebSite.CoreClient;
using CMA.WebSite.DBClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.Business
{
    public class DeviceBusiness
    {
        private MonitorItemDBClient _dbCleint = new MonitorItemDBClient();
        private MonitorItemCoreClient _coreClient = new MonitorItemCoreClient();

        public IEnumerable<MonitorItemDBModel> GetList()
        {
            return _dbCleint.GetList();
        }

        public MonitorItemDBModel Get(long id)
        {
            return _dbCleint.Get(id);
        }

        public bool Edit(MonitorItemDBModel model)
        {
            if (_dbCleint.Edit(model))
            {
                if (_coreClient.Edit(model))
                {
                    return true;
                }
                else
                {
                    throw new AppException("更新采集器失败");
                }
            }
            else
            {
                throw new AppException("编辑数据库失败");
            }
        }

        public bool Add(MonitorItemDBModel model)
        {
            if (_dbCleint.Add(model))
            {
                if (_coreClient.Add(model))
                {
                    return true;
                }
                else
                {
                    throw new AppException("更新采集器失败");
                }
            }
            else
            {
                throw new AppException("添加数据库失败");
            }            
        }

        public bool Delete(long id)
        {
            if (_dbCleint.Delete(id))
            {
                if (_coreClient.Delete(id))
                {
                    return true;
                }
                else
                {
                    throw new AppException("更新采集器失败");
                }
            }
            else
            {
                throw new AppException("删除数据库失败");
            }
        }
    }
}
