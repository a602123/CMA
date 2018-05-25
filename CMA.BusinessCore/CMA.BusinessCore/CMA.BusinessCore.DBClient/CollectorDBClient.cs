using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.BusinessCore.DBClient
{
    public class CollectorDBClient
    {
        public IEnumerable<CollectorDBModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Collector/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<CollectorDBModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool Add(CollectorDBModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Collector/Add");
                WebApiClient client = new WebApiClient();
                return client.Post<bool, CollectorDBModel>(url,model);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool Delete(string host)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Collector/Delete?host={host}");
                WebApiClient client = new WebApiClient();
                return client.Get<bool>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
