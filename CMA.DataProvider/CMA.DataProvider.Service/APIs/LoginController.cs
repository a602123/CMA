using CMA.DataProvider.Business;
using CMA.DataProvider.DataOperator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMA.DataProvider.Service
{
    public class LoginController: ApiController
    {
        private LoginDataBusiness _business = new LoginDataBusiness();

        [HttpGet]
        public bool Login(string username, string password)
        {
            try
            {
                Thread.Sleep(5000);
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception("用户名不为空");
                }
                return username == password;
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
        public bool LoginPost([FromBody]tb_user model)
        {
            try
            {
                return _business.Login(model);
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

        public class LoginModel
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}
