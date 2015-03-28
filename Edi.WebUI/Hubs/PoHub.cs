using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edi.WebUI.DbNotifier;
using Microsoft.AspNet.SignalR;

namespace Edi.WebUI.Hubs
{
    public class PoHub : Hub
    {
        public void Start()
        {
            var changeNotifier = new PoChangeNotifier();
            changeNotifier.GetAllPos();
        }

        public static void UpdatePos()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PoHub>();
            context.Clients.All.updatePos();
        }
    }
}