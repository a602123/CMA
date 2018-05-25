using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.BusinessCore.CollectorClient
{
    /// <summary>
    /// 对 下级采集器 进行操作的类
    /// </summary>
    public class MonitorItemCollectorClient
    {
        public bool AddMonitorItem(string collectorHost, DBParamter model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetCollectUrl(collectorHost, $"Collect/AddDBMonitor");
                WebApiClient client = new WebApiClient();
                return client.Post<bool, DBParamter>(url, model);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditMonitorItem(string collectorHost, DBParamter paramter)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetCollectUrl(collectorHost, $"Collect/EditDBMonitor");
                WebApiClient client = new WebApiClient();
                return client.Post<bool, DBParamter>(url, paramter);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveMonitorItem(string collectorHost, long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetCollectUrl(collectorHost, $"Collect/RemoveCollector/{id}");
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
