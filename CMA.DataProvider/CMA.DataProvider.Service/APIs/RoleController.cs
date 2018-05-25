using CMA.Common.Model;
using CMA.DataProvider.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMA.DataProvider.Service
{
    public class RoleController : ApiController
    {
        private RoleDBBusiness dbBusiness = new RoleDBBusiness();
        public IEnumerable<RoleModel> GetList()
        {
            try
            {
                return dbBusiness.GetList();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
        public RoleModel Get(long id)
        {
            try
            {
                return dbBusiness.Get(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
        public bool Add(RoleModel model)
        {
            try
            {
                return dbBusiness.Add(model);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
        [HttpGet]
        public bool Delete(int id)
        {
            try
            {
                return dbBusiness.Delete(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }


        public bool Edit(RoleModel model)
        {
            try
            {
                return dbBusiness.Edit(model);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
            return false;
        }
    }
}
