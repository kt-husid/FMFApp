using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BootstrapWebApplication.Hubs
{
    //[HubName("messageHub")]
    public class MessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(double progress)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastProgress(progress);
        }
    }
}