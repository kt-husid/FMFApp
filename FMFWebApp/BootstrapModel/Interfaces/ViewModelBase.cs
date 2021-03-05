using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication
{
    public interface IViewModelBase
    {
        int Id { get; set; }
    }

    public abstract class ViewModelBase : IViewModelBase
    {
        public int Id { get; set; }
    }

    public abstract class ViewModelBaseWithChangeEvent : IHasChangeEvent, IViewModelBase
    {
        public int Id { get; set; }
    }
}