using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using BootstrapWebApplication.Model;
using BootstrapWebApplication.ViewModels;

namespace BootstrapWebApplication.Hubs
{
    //[HubName("messageHub")]
    public class LundinBookingsHub : Hub
    {
        //public void UpdateProgress(double progress)
        //{
        //    // Call the broadcastMessage method to update clients.
        //    Clients.All.updateProgress(progress);
        //}

        //public void AddNewItem(Bokingar boking)
        //{
        //    // Call the broadcastMessage method to update clients.
        //    Clients.All.addNewItem(boking);
        //}

        //public void AddOldItem(Bokingar boking)
        //{
        //    // Call the broadcastMessage method to update clients.
        //    Clients.All.addOldItem(boking);
        //}

        //public void AddErrorItem(BootstrapWebApplication.Controllers.LundinBokingarController.ImportError errorItem)
        //{
        //    // Call the broadcastMessage method to update clients.
        //    Clients.All.addErrorItem(errorItem);
        //}
        public void UpdateProgress(double progress)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.updateProgress(progress);
        }

        public void AddNewItem(BokingarViewModel boking)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addNewItem(boking);
        }

        public void AddOldItem(BokingarViewModel boking)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addOldItem(boking);
        }

        public void AddErrorItem(BootstrapWebApplication.Controllers.LundinBokingarController.ImportError errorItem)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.addErrorItem(errorItem);
        }
    }
}