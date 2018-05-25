using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace CMA.CollectCenter.Service
{
    partial class CollectCenterService : ServiceBase
    {
        public CollectCenterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:9001");
            config.Routes.MapHttpRoute("default", "collect/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
            }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
