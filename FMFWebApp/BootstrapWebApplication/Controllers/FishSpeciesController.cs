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
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers
{
    public class FishSpeciesController : Controller<FishSpecies>
    {
        public JsonResult GetAll()
        {
            var result = db.FishSpecies.FilterDeleted().Select(m => new FishSpeciesViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description,
                OldCode = m.OldCode,
                OldName = m.OldName,
                RAD = m.RAD
                //ChangeEvent = m.ChangeEvent
            });
            return Json(new DataSourceResult() { Data = result, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Set<FishSpecies>() as IQueryable<FishSpecies>;
            if (filter.Length > 1)
            {
                listFiltered = listFiltered.Where(s =>
                    ("" + s.Code).ToLower().Equals(filter.ToLower()) ||
                    ("" + s.Name).ToLower().Contains(filter.ToLower()) ||
                    ("" + s.Description).ToLower().Contains(filter.ToLower()) ||
                    ("" + s.OldCode).ToLower().Equals(filter.ToLower()) ||
                    ("" + s.OldName).ToLower().Contains(filter.ToLower())
                    );
            }
            listFiltered = listFiltered.OrderBy(x => x.Code).ThenBy(x => x.Name);
            var result = listFiltered.FilterDeleted().Select(m => new FishSpeciesViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description,
                OldCode = m.OldCode,
                OldName = m.OldName,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<FishSpecies> PagedListFilter(IQueryable<FishSpecies> list, string filterName = null)
        {
            return list.Where(s => ("" + s.Code).ToLower().Equals(filterName.ToLower())
                || ("" + s.OldCode).ToLower().Equals(filterName.ToLower())
                || ("" + s.Name).ToLower().Contains(filterName.ToLower())
                || ("" + s.OldName).ToLower().Contains(filterName.ToLower())
                );
        }

        protected override void AddViewBag(FishSpecies obj)
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
            //var listFiltered = db.Set<FishSpecies>() as IQueryable<FishSpecies>;
            //if (filter.Length > 1)
            //{
            //    //DateTime dt = DateTime.Now;
            //    //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //    listFiltered = listFiltered.Where(s => s.Code.ToLower().Equals(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower())
            //    );
            //}
            //return GetFiltered(request, listFiltered);
            var filtered = new FishSpeciesHandler().GetFiltered(db.FishSpecies, filter, 1);
            return GetFiltered(request, filtered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<FishSpecies> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new FishSpeciesViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                OldCode = m.OldCode,
                Name = m.Name,
                Description = m.Description,
                OldName = m.OldName,
                RAD = m.RAD,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<FishSpecies>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FishSpeciesCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new FishSpecies()
                {
                    FishSpeciesNumber = o.FishSpeciesNumber,
                    Code = o.Code,
                    OldCode = null,
                    Name = o.Name,
                    OldName = null,
                    Description = o.Description,
                    RAD = o.RAD,
                    ALIAS = null,
                    LINK = null
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<FishSpecies>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new FishSpeciesCreateEditViewModel()
                {
                    FishSpeciesNumber = o.FishSpeciesNumber,
                    Code = o.Code,
                    Name = o.Name,
                    Description = o.Description,
                    RAD = o.RAD
                };
            }
            return Edit<FishSpecies>("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FishSpeciesCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<FishSpecies>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //dbObj.Code = vmObj.Code;
                dbObj.FishSpeciesNumber = vmObj.FishSpeciesNumber;
                dbObj.Name = vmObj.Name;
                dbObj.Description = vmObj.Description;
                dbObj.RAD = vmObj.RAD;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<FishSpecies>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(FishSpeciesCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<FishSpecies>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FishSpeciesViewModel dbObj)
        {
            return DeleteObject<FishSpecies, FishSpecies>(dbObj.Id);
        }
    }
}