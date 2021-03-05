using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication
{
    public abstract class ViewModelBaseWithChangeEvent3 : ViewModelBaseWithChangeEvent, IChangeEvent
    {
        public int? ChangeEventId { get; set; }

        public ChangeEvent ChangeEvent { get; set; }
    }
}