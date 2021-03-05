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
    public class StatusController : Controller<Status>
    {
        protected override IQueryable<Status> PagedListFilter(IQueryable<Status> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToString().ToLower().Contains(filterName.ToLower()) || s.Description.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(Status obj)
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
            var listFiltered = db.Set<Status>() as IQueryable<Status>;
            if (filter.Length > 1)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Equals(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<Status> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new StatusViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                BREV_UT = m.BREV_UT,
                FLYTAST = m.FLYTAST,
                DLISTA = m.DLISTA,
                BLISTA = m.BLISTA,
                YearList = m.YearList,
                IsOnYearList = m.IsOnYearList,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<Status>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Status()
                {
                    Code = o.Code,
                    Description = o.Description,
                    BREV_UT = o.BREV_UT,
                    FLYTAST = o.FLYTAST,
                    DLISTA = o.DLISTA,
                    BLISTA = o.BLISTA,
                    YearList = o.IsOnYearList ? "J" : "N",
                    IsOnYearList = o.IsOnYearList
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<Status>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new StatusCreateEditViewModel()
                {
                    Code = o.Code,
                    Description = o.Description,
                    BREV_UT = o.BREV_UT,
                    FLYTAST = o.FLYTAST,
                    DLISTA = o.DLISTA,
                    BLISTA = o.BLISTA,
                    YearList = o.YearList,
                    IsOnYearList = o.IsOnYearList
                };
            }
            return Edit<Status>("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StatusCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<Status>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //dbObj.Code = vmObj.Code;
                dbObj.Description = vmObj.Description;
                dbObj.BREV_UT = vmObj.BREV_UT;
                dbObj.FLYTAST = vmObj.FLYTAST;
                dbObj.DLISTA = vmObj.DLISTA;
                dbObj.BLISTA = vmObj.BLISTA;
                dbObj.YearList = vmObj.IsOnYearList ? "J" : "N";
                dbObj.IsOnYearList = vmObj.IsOnYearList;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<Status>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(StatusCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Status>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(StatusViewModel dbObj)
        {
            return DeleteObject<Status, Status>(dbObj.Id);
        }
    }
}