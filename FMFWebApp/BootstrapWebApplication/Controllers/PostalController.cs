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
using BootstrapWebApplication.BLL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class PostalController : Controller<Postal>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Postals as IQueryable<Postal>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Contains(filter.ToLower()) || s.CityName.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new PostalViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                CityName = m.CityName,
                CountryCode = m.CountryCode,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<Postal> PagedListFilter(IQueryable<Postal> list, string filterName = null)
        {
            return list.Where(s => s.CityName.ToUpper().Contains(filterName.ToUpper())
                                || s.CountryCode.ToUpper().Contains(filterName.ToUpper())
                                || s.Code.ToString().ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(Postal obj)
        {
            int? countryId = null;
            if (obj != null)
            {
                var country = db.Countries.Where(x => x.Code == obj.CountryCode).FirstOrDefault();
                countryId = country != null ? new Nullable<int>(country.Id) : null;
            }
            ViewBag.CountryId = new SelectList(db.Countries.ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " " + m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? countryId : null);
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
            var listFiltered = db.Set<Postal>() as IQueryable<Postal>;
            if (filter.Length > 1)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.CityName.Contains(filter.ToLower()) || s.Code.ToString().ToLower().Contains(filter.ToLower()) || s.CountryCode.ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<Postal> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new PostalViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                CityName = m.CityName,
                CountryCode = m.CountryCode,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        //protected override DataSourceResult Get(int id, DataSourceRequest request)
        //{
        //    // Avoid the circular reference by creating a View Model object and skiping the Customer property
        //    var result = db.Postals.FilterDeleted().Select(m => new PostalViewModel()
        //    {
        //        Id = m.Id,
        //        Code = m.Code,
        //        CityName = m.CityName,
        //        CountryCode = m.CountryCode,
        //        ChangeEvent = m.ChangeEvent
        //    });
        //    return result.ToDataSourceResult(request);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<Postal>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostalCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Postal()
                {
                    Code = o.Code,
                    CityName = o.CityName,
                    CountryCode = o.CountryCode
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.Postals.Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new PostalCreateEditViewModel()
                {
                    Code = obj.Code,
                    CityName = obj.CityName,
                    CountryCode = obj.CountryCode
                };
            }
            return Edit<Postal>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostalCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Postals.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Code = vmObj.Code;
                dbObj.CityName = vmObj.CityName;
                dbObj.CountryCode = vmObj.CountryCode;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<Postal>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(PostalCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Postal>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PostalViewModel dbObj)
        {
            return DeleteObject<Postal, Postal>(dbObj.Id);
        }
    }
}