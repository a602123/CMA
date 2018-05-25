using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.WebApiClient
{
    public class WebApiClientException:Exception
    {
        private HttpResponseMessage _response;
        /// <summary>
        /// 发生异常的Response
        /// </summary>
        public HttpResponseMessage Response
        {
            get { return _response; }
        }
        public WebApiClientException(string message, HttpResponseMessage response) : base(message)
        {
            _response = response;
        }
    }
}
