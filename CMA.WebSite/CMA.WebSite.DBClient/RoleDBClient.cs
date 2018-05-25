using CMA.Common.Model;
using CMA.Common.WebApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMA.WebSite.DBClient
{
    public class RoleDBClient
    {

        public RoleModel Get(long id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Role/Get/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<RoleModel>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 添加角色 zhanglianghui
        /// </summary>
        /// <param name="Rmodel">角色实体</param>
        /// <returns>返回true为成功</returns>
        public bool Add(RoleModel Rmodel)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Role/Add");
                WebApiClient client = new WebApiClient();
                var temp = client.Post<bool, RoleModel>(url, Rmodel);
                return temp;
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public bool Delete(int id)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Role/Delete/{id}");
                WebApiClient client = new WebApiClient();
                return client.Get<bool>(url);
            }
            catch (WebApiClientException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoleModel> GetList()
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Role/GetList");
                WebApiClient client = new WebApiClient();
                return client.Get<IEnumerable<RoleModel>>(url);
            }
            catch (WebApiClientException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Edit(RoleModel model)
        {
            try
            {
                string url = UrlHelper.GetInstance().GetDBUrl($"Role/Edit");
                WebApiClient client = new WebApiClient();
                var temp = client.Post<bool, RoleModel>(url, model);
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
