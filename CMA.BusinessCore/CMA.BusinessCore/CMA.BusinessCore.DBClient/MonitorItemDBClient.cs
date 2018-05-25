using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.BusinessCore.DBClient
{
    public class MonitorItemDBClient
    {
        public IEnumerable<MonitorItemDBModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItem/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<MonitorItemDBModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MonitorItemDBModel Get(long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItem/Get/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<MonitorItemDBModel>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
