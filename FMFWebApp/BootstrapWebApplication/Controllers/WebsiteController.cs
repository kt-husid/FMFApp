using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.BLL;
using PagedList;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class WebsiteController : Controller<Website>
    {
        protected override IQueryable<Website> PagedListFilter(IQueryable<Website> list, string filterName = null)
        {
            return list.Where(s => s.Name.ToUpper().Contains(filterName.ToUpper())
                                       || s.Uri.ToUpper().Contains(filterName.ToUpper())
                                       || s.Description.ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(Website website)
        {
            if (website != null)
            {
                ViewBag.EntityId = website.EntityId;
                //ViewBag.PersonId = website.PersonId;
            }
            ViewBag.PersonId = new SelectList(db.Persons.ToList().Select(m => new SelectListItem
            {
                Text = m.FullName,
                Value = m.Id.ToString()
            }), "Value", "Text", website == null ? 1 : website.EntityId);// website.PersonId);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
