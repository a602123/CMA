using CMA.CollectCenter.Base;
using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMA.CollectCenter.Service
{
    public class CollectController : ApiController
    {

        public bool AddDBMonitor(DBParamter paramter)
        {
            try
            {
                CollectorCenter.GetInstance().Add(CollectorType.Web, paramter);
                return true;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="itemtype">采集器类型</param>
        /// <param name="dbParamter">DBParamter参数</param>
        [HttpGet]
        public void StartCollect()
        {
            try
            {
                //开始
                CollectorCenter.GetInstance().StartMonitor();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        public void StopCollect()
        {
            try
            {
                CollectorCenter.GetInstance().Stop();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }

        public void RemoveCollector(long id)
        {
            try
            {
                CollectorCenter.GetInstance().Remove(id);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(ex.Message)
                });
            }
        }
    }
}
