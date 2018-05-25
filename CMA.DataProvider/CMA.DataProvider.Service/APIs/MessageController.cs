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

namespace CMA.DataProvider.Service.APIs
{
   public  class MessageController : ApiController
    {
        private MessageDBBusiness Mdb = new MessageDBBusiness();

        public bool Edit(MessageModel model)
        {
            return Mdb.Edit(model);
        }
        public IEnumerable<MessageModel> GetList()
        {
            try
            {
                return Mdb.GetList();
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
        public MessageModel Get(long id)
        {
            try
            {
                return Mdb.Get(id);
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
        public bool Add(MessageModel model)
        {
            try
            {
                return Mdb.Add(model);
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
                return Mdb.Delete(id);
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
