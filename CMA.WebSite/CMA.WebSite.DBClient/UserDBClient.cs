using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.DBClient
{
    public class UserDBClient
    {
        public UserInfoModel Login(UserInfoModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/Get?username={model.Username}");
                WebApiClient client = new WebApiClient();
                return client.Get<UserInfoModel>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserInfoModel Get(long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/Get/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<UserInfoModel>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Add(UserInfoModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/Add/");
                WebApiClient client = new WebApiClient();
                return client.Post<int, UserInfoModel>(url, model);
            }            
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
 
        public bool Edit(UserInfoModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/Edit");
                WebApiClient client = new WebApiClient();               
                var temp = client.Post<bool, UserInfoModel>(url, model);
                return true;
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(long id)
        {
            try
            {
                //string url = UrlHelper.GetInstance().GetDBUrl($"User/Delete/{id}");
                string url = UrlHelper.GetInstance().GetDBUrl($"User/Delete/{id}");
                WebApiClient client = new WebApiClient();
                //return client.Post<bool, long>(url, id);
                return client.Get<bool>(url);
            }
            catch (WebApiClientException ex)
            {//{"{\"Message\":\"No HTTP resource was found that matches the request URI 'http://127.0.0.1:9001/data/User/Delete/'.\",\"MessageDetail\":\"No action was found on the controller 'User' that matches the request.\"}"}
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool RestPassword(UserInfoModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/RestPassword");
                WebApiClient client = new WebApiClient();
                return client.Post<bool,UserInfoModel>(url, model);
            }
            catch (WebApiClientException ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<UserInfoModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"User/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<UserInfoModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
