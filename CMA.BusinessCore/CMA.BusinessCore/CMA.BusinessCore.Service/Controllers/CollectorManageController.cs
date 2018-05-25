using CMA.BusinessCore.Business;
using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMA.BusinessCore.Service.Controllers
{
    /// <summary>
    /// 用于接收 web 发来的 有关采集器 的请求，如 发现新的采集器  当前所以采集器的状态 
    /// </summary>
    public class CollectorManageController : ApiController
    {
        [HttpGet]
        public string CheckState()
        {
            try
            {
                CollectorBusiness business = CollectorBusiness.GetInstance();
                return business.GetCollectorState();
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
        public bool Add(CollectorDBModel model)
        {
            try
            {
                CollectorBusiness business = CollectorBusiness.GetInstance();
                return business.AddHost(model);
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
        public bool Delete(string host)
        {
            try
            {
                CollectorBusiness business = CollectorBusiness.GetInstance();
                return business.RemoveHost(host);
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
        public List<string> FindNew(string host)
        {
            try
            {
                string startHost = host.Split('*')[0].Trim();
                string endHost = host.Split('*')[1].Trim();
                int port = Convert.ToInt32(host.Split('*')[2].Trim());
                CollectorBusiness business = CollectorBusiness.GetInstance();
                business.FindNewHost(startHost, endHost, port);

                return null;
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
