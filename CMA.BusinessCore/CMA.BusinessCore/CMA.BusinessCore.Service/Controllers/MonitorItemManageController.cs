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

namespace CMA.BusinessCore.Service
{
    /// <summary>
    /// 用于接收 web 发来的 有关设备 的请求，如添加新的设备，编辑删除设备
    /// </summary>
    public class MonitorItemManageController : ApiController
    {
        [HttpPost]
        public bool Add(MonitorItemDBModel model)
        {
            try
            {
                return MonitorItemBusiness.GetInstance().AddMonitorItem(model);
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
                return MonitorItemBusiness.GetInstance().EditMonitorItem(model);
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
                //return MonitorItemBusiness.GetInstance().DeleteMonitorItem(id);
                return false;
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
