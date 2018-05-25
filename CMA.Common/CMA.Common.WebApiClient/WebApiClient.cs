using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.WebApiClient
{
    /// <summary>
    /// WebApi客户端
    /// </summary>
    public class WebApiClient
    {
        /// <summary>
        /// Get访问WebApi
        /// </summary>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="url">访问的url</param>
        /// <returns></returns>
        public R Get<R>(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    R result = response.Content.ReadAsAsync<R>().Result;
                    return result;
                }
                else
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }

        /// <summary>
        /// Get访问WebApi（不需要返回值）
        /// </summary>
        /// <param name="url">访问的url</param>
        public void Get(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }

        /// <summary>
        /// 异步Get访问WebApi
        /// </summary>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="url">访问的url</param>
        /// <param name="action">处理返回值的委托</param>
        public async void GetAsync<R>(string url, Action<R> action)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    R result = await response.Content.ReadAsAsync<R>();
                    action(result);
                }
                else
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }
        /// <summary>
        /// Post访问WebApi
        /// </summary>
        /// <typeparam name="R">返回值的类型</typeparam>
        /// <typeparam name="S">推送数据的类型</typeparam>
        /// <param name="url">访问的url</param>
        /// <param name="data">推送的数据</param>
        /// <returns></returns>
        public R Post<R,S>(string url, S data)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync<S>(url, data).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    R result = response.Content.ReadAsAsync<R>().Result;
                    return result;
                }
                else
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }

        /// <summary>
        /// Post访问WebApi（不需要返回值）
        /// </summary>
        /// <typeparam name="S">推送数据的类型</typeparam>
        /// <param name="url">访问的url</param>
        /// <param name="data">推送的数据</param>
        public void Post<S>(string url, S data)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(url, data).Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }

        /// <summary>
        /// 异步访问WebApi
        /// </summary>
        /// <typeparam name="R">返回值的类型</typeparam>
        /// <typeparam name="S">推送的数据的类型</typeparam>
        /// <param name="url">访问的url</param>
        /// <param name="data">推送的数据</param>
        /// <param name="action">处理返回结果的委托</param>
        public async void PostAsync<R, S>(string url, S data, Action<R> action)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync<S>(url, data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    R result = response.Content.ReadAsAsync<R>().Result;
                    action(result);
                }
                else
                {
                    throw new WebApiClientException(response.Content.ReadAsStringAsync().Result, response);
                }
            }
        }

        /// <summary>
        /// 同步Head请求
        /// </summary>
        /// <param name="url">Head的Url</param>
        /// <returns></returns>
        public bool Head(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Head,url);
                HttpResponseMessage response = httpClient.SendAsync(message).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 异步Head请求
        /// </summary>
        /// <param name="url">Head的Url</param>
        /// <param name="action">处理返回结果的委托</param>
        public async void HeadAsync(string url, Action<bool> action)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Head, url);
                HttpResponseMessage response = await httpClient.SendAsync(message);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    action(true);
                }
                else
                {
                    action(false);
                }
            }
        }
    }
}
