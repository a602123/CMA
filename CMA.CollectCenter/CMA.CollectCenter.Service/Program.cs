using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web;
using System.Windows.Forms;
using System.Web.Http;
using System.IO;
using System.ServiceProcess;
using CMA.CollectCenter.Base;
using CMA.Common.MySQLHelper;
using System.Configuration;
using CMA.Common.Model;
using ServiceStack.Redis.Support;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Collector1;

namespace CMA.CollectCenter.Service
{
    static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDoman_UnhandleException);
        }

        private static void CurrentDoman_UnhandleException(object sender, UnhandledExceptionEventArgs e)
        {
            string strException = $"{DateTime.Now}发生系统异常。\r\n{e.ExceptionObject.ToString()}\r\n\r\n\r\n";
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemException.log"), strException);
        }


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() 
        {
#if DEBUG

            #region 准备MonitorItemList
            //DBParamter dbParamter = new DBParamter() { DBName = "db_cma", Host = "192.168.1.234", Interval = 4000, Password = "qwe123!@#", Port = 3306, Username = "sa" };
            //DBParamter dbParamter2 = new DBParamter() { DBName = "test", Host = "127.0.0.1", Interval = 10000, Password = "qwe123!@#", Port = 3306, Username = "sa" };
            //WebCollector webCollector = new WebCollector(dbParamter);
            //WebCollector webCollector2 = new WebCollector(dbParamter2);
            //new CollectorInfoDataProvider().Insert(webCollector);
            //new CollectorInfoDataProvider().Insert(webCollector2);
            #endregion

            //开始
            //CollectorCenter centerCollector = new Service.CollectorCenter(CollectorType.Web, dbParamter2);
            //centerCollector.StartMonitor();

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://192.168.1.105:9002");
            config.Routes.MapHttpRoute("default", "collect/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                CollectorCenter.GetInstance().StartMonitor();

                Application.Run(new Form1());

            }


#else
            ServiceBase[] servicesToRun = new ServiceBase[] {
                new CollectCenterService()
            };
            ServiceBase.Run(servicesToRun);  
#endif

        }
    }
}
