using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.Common.WebApiClient;

namespace CMA.WebSite.DBClient
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

        public bool Delete(long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItem/Delete/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<bool>(url);
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool Edit(MonitorItemDBModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItem/Edit");
                WebApiClient client = new WebApiClient();
                var temp = client.Post<bool, MonitorItemDBModel>(url, model);
                return true;
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Add(MonitorItemDBModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItem/Add/");
                WebApiClient client = new WebApiClient();
                return client.Post<bool, MonitorItemDBModel>(url, model);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
