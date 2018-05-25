using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.DBClient
{
   public  class MessageDBClient
    {
        

        public MessageModel Get(int  id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Message/Get/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<MessageModel>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Add(MessageModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Message/Add");
                WebApiClient client = new WebApiClient();
                var temp = client.Post<bool, MessageModel>(url, model);
                return temp;
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<MessageModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Message/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<MessageModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Delete(int id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Message/Delete/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<bool>(url);
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public bool Edit (MessageModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Message/Edit");
                WebApiClient client = new WebApiClient();
                var temp = client.Post<bool, MessageModel>(url, model);
                return true;
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
    }
}
