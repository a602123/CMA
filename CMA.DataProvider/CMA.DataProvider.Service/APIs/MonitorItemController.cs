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
    public class MonitorItemController : ApiController
    {
        private MonitorItemBusiness _business = new MonitorItemBusiness();

        [HttpGet]
        public IEnumerable<MonitorItemDBModel> GetList()
        {
            try
            {
                return _business.GetList();
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

        public bool Add(MonitorItemDBModel model)
        {
            try
            {
                return _business.Add(model);
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
        public MonitorItemDBModel Get(int id)
        {
            try
            {
                return _business.Get(id);
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

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                return _business.Delete(id);
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

        [HttpPost]
        public bool Edit(MonitorItemDBModel model)
        {
            try
            {
                return _business.Edit(model);
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
    }
}
