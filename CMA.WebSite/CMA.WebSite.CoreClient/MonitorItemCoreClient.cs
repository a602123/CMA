using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.CoreClient
{
    public class MonitorItemCoreClient
    {

        public bool Delete(long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItemManage/Delete/{id}");
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
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItemManage/Edit/");
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
                string url = UrlHelper.GetInstance().GetDBUrl($"MonitorItemManage/Add/");
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
