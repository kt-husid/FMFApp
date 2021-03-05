using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kthusid.Web;
using PagedList;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.DAL;
using System.Data.Entity.Infrastructure;
using BootstrapCore.Attributes;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class PersonController : Controller<Person>
    {
        protected override IQueryable<Person> PagedListFilter(IQueryable<Person> list, string filterName = null)
        {
            return list.Where(s => s.FirstName.ToLower().Contains(filterName.ToLower())
                                   || s.MiddleName.ToLower().Contains(filterName.ToLower())
                                   || s.LastName.ToLower().Contains(filterName.ToLower())
                                   || s.FullName.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(Person person)
        {
            ViewBag.TitleId = new SelectList(db.PersonTitles.ToList().Select(m => new SelectListItem
            {
                Text = m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", person != null ? person.TitleId : null);
            ViewBag.GenderId = new SelectList(db.PersonGenders.ToList().Select(m => new SelectListItem
            {
                Text = m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", person != null ? person.GenderId : null);
        }

        public ActionResult Websites(int? id)
        {
            return GetRelatedData(id, "Websites");
        }

        public ActionResult Phones(int? id)
        {
            return GetRelatedData(id, "Phones");
        }

        public ActionResult Addresses(int? id)
        {
            return GetRelatedData(id, "Addresses");
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
