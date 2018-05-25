using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

using System.Net.Http.Formatting;

namespace CMA.DataProvider.Service
{
    static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
#if DEBUG
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://192.168.1.105:9001");
            config.Routes.MapHttpRoute("default", "data/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            ////默认返回 json  
            //config.Formatters.JsonFormatter.MediaTypeMappings.Add(
            //    new QueryStringMapping("datatype", "json", "application/json"));
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Application.Run(new Form1());
            }
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DataProviderService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string strException = $"{DateTime.Now}发生系统异常。\r\n{e.ExceptionObject.ToString()}\r\n\r\n\r\n";
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemException.log"), strException);
        }
    }
}
