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
    public class ActionController : ApiController
    {
        private ActionDBBusiness _dbBusiness = new ActionDBBusiness();

        [HttpGet]
        public IEnumerable<ActionModel> GetList()
        {
            try
            {
                return _dbBusiness.GetList();
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
