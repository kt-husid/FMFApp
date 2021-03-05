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
using System.Globalization;

namespace BootstrapWebApplication.Controllers
{
    public class MinimumWageController : Controller<MinimumWage>
    {
        protected override IQueryable<MinimumWage> PagedListFilter(IQueryable<MinimumWage> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToLower().Contains(filterName.ToLower()) || (s.PerDay.HasValue && s.PerDay.Value.ToString().ToLower().Contains(filterName.ToLower())));
        }

        protected override void AddViewBag(MinimumWage obj)
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
            var listFiltered = db.Set<MinimumWage>() as IQueryable<MinimumWage>;
            if (filter.Length > 3)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Contains(filter.ToLower()) || (s.PerDay.HasValue && s.PerDay.Value.ToString().ToLower().Contains(filter.ToLower())));
            }
            return GetFiltered(request, listFiltered.OrderByDescending(x => x.ChangeEventId));
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<MinimumWage> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().OrderByDescending(x => x.Id).Select(m => new MinimumWageViewModel()
            {
                Id = m.Id,
                PerDay = m.PerDay,
                PerMonth = m.PerMonth,
                Code = m.Code,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<MinimumWage>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MinimumWageCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new MinimumWage()
                {
                    PerMonth = o.PerMonth,
                    PerDay = o.PerDay,
                    DG_MIN = null,
                    DG_MAX = null,
                    GRAD = null,
                    DG_ST = null,
                    Code = o.Code
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.Set<MinimumWage>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new MinimumWageCreateEditViewModel()
                {
                    PerMonth = obj.PerMonth,
                    PerDay = obj.PerDay,
                    Code = obj.Code
                };
            }
            return Edit<MinimumWage>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MinimumWageCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<MinimumWage>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.PerMonth = vmObj.PerMonth;
                dbObj.PerDay = vmObj.PerDay;
                dbObj.Code = vmObj.Code;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<MinimumWage>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MinimumWageViewModel dbObj)
        {
            return DeleteObject<MinimumWage, MinimumWage>(dbObj.Id);
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<MinimumWage>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(MinimumWageCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
    }
}