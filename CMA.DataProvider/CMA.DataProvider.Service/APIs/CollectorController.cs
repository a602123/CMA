using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CMA.Common.Model;
using CMA.DataProvider.Business;

namespace CMA.DataProvider.Service.APIs
{
    public class CollectorController: ApiController
    {
        private CollectorDBBusiness _business = new CollectorDBBusiness();

        [HttpGet]
        public IEnumerable<CollectorDBModel> GetList()
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

        public bool Add(CollectorDBModel model)
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
    }
}
