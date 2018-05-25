using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using CMA.Common.Model;

namespace CMA.WebSite.Web
{
    [HubName("MessageHub")]
    public class MessageHub : Hub
    {

    }
}