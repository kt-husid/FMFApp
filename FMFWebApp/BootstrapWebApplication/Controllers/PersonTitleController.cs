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
using PagedList;
using System.Linq.Expressions;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class PersonTitleController : Controller<PersonTitle>
    {
        protected override IQueryable<PersonTitle> PagedListFilter(IQueryable<PersonTitle> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToLower().Contains(filterName.ToLower()) || s.Description.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(PersonTitle obj)
        {

        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var filter = "";
            if (request.Filters.Count > 0)
            {
                filter = (request.Filters[0] as Kendo.Mvc.FilterDescriptor).Value as string;
            }
            request.Filters.Clear();
            return Json(GetFiltered(filter, request));
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        private DataSourceResult GetFiltered(string filter, DataSourceRequest request)
        {
            var listFiltered = db.Set<PersonTitle>() as IQueryable<PersonTitle>;
            if (filter.Length > 2)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Equals(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<PersonTitle> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new PersonTitleViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<PersonTitle>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonTitleCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new PersonTitle()
                {
                    //Code = o.Code,
                    Description = o.Description
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<PersonTitle>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new PersonTitleCreateEditViewModel()
                {
                    //Code = o.Code,
                    Description = o.Description
                };
            }
            return Edit<PersonTitle>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonTitleCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<PersonTitle>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //dbObj.Code = vmObj.Code;
                dbObj.Description = vmObj.Description;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<PersonTitle>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(PersonTitleCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<PersonTitle>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PersonTitleViewModel dbObj)
        {
            return DeleteObject<PersonTitle, PersonTitle>(dbObj.Id);
        }
    }
}