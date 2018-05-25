using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMA.WebSite.Web.Controllers
{
    public class CoreMessageController : Controller
    {
        public void ReceMessage(string message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hub.Clients.All.ReceMessage(message);
        }
    }
}