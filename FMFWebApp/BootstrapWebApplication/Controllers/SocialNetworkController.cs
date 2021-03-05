using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.DAL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class SocialNetworkController : Controller<SocialNetwork>
    {
        protected override IQueryable<SocialNetwork> PagedListFilter(IQueryable<SocialNetwork> list, string filterName = null)
        {
            return list.Where(s => s.UserName.ToUpper().Contains(filterName.ToUpper())
                                   || s.Uri.ToUpper().Contains(filterName.ToUpper())
                                   || s.Description.ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(SocialNetwork socialNetwork)
        {
            
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
