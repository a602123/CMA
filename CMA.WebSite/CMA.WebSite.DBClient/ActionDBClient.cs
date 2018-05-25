using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMA.Common.Model;
using CMA.Common.WebApiClient;

namespace CMA.WebSite.DBClient
{
    public class ActionDBClient
    {
        public IEnumerable<ActionModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Action/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<ActionModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
