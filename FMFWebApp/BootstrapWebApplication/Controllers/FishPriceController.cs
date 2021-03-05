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
    public class FishPriceController : Controller<FishPrice>
    {
        //public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        //{
        //    filter = Request.Params["filter[filters][0][value]"];
        //    var listFiltered = db.Set<FishPrice>() as IQueryable<FishPrice>;
        //    if (filter.Length > 1)
        //    {
        //        listFiltered = listFiltered.Where(s => s.FishSpeciesCode.ToLower().Contains(filter.ToLower()));
        //    }
        //    listFiltered = listFiltered.OrderByDescending(x => x.Id).Take(1);

        //    if (listFiltered.Count() != 1)
        //    {
        //        var resultCount = 10;
        //        var fishSpeciesFiltered = db.Set<FishSpecies>() as IQueryable<FishSpecies>;
        //        var fishPriceFiltered = db.Set<FishPrice>() as IQueryable<FishPrice>;
        //        fishSpeciesFiltered = fishSpeciesFiltered.Where(s => 
        //            s.Code.ToLower().Contains(filter.ToLower()) 
        //            || s.OldCode.ToLower().Contains(filter.ToLower())
        //            || s.Name.ToLower().Contains(filter.ToLower())
        //            || s.OldName.ToLower().Contains(filter.ToLower())
        //            ).Take(resultCount);
        //        var tempFishSpecies = fishSpeciesFiltered.FirstOrDefault();
        //        listFiltered = fishPriceFiltered.Where(x => x.FishSpeciesCode == tempFishSpecies.Code
        //            || x.FishSpeciesCode.ToLower().Equals(tempFishSpecies.Code.ToLower())
        //            || x.FishSpeciesCode.ToLower().Equals(tempFishSpecies.OldCode.ToLower())
        //            ).OrderByDescending(x => x.Id).Take(resultCount);
        //    }

        //    var fishPrice = listFiltered.FirstOrDefault();
        //    var fishSpecies = fishPrice != null ? db.FishSpecies.Where(x => x.Code.ToLower().Equals(fishPrice.FishSpeciesCode.ToLower()) || x.OldCode.ToLower().Equals(fishPrice.FishSpeciesCode.ToLower())).FirstOrDefault() : null;
        //    var fishName = fishSpecies != null ? fishSpecies.Name : null;
        //    var result = listFiltered.Select(m => new FishPriceViewModel()
        //    {
        //        Id = m.Id,
        //        FishSpeciesCode = m.FishSpeciesCode,
        //        Name = fishName,
        //        Price = m.Price,
        //        From = m.From,
        //        To = m.To,
        //        ChangeEvent = m.ChangeEvent
        //    });
        //    return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        //    //return Json(GetFiltered(filter, request).Data, JsonRequestBehavior.AllowGet);
        //    //var filtered = new FishSpeciesHandler().GetFiltered(db.FishSpecies, filter, 1).OrderBy(x=>x.;
        //    //var result = GetFiltered(request, filtered);
        //    //return Json(result.Data, JsonRequestBehavior.AllowGet);
        //    //return Json(GetFiltered(filter, request).Data, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Set<FishPrice>() as IQueryable<FishPrice>;
            if (filter.Length > 1)
            {
                listFiltered = listFiltered.Where(s => s.FishSpeciesCode.ToLower().Contains(filter.ToLower()));
            }
            listFiltered = listFiltered.OrderByDescending(x => x.Id).Take(1);

            if (listFiltered.Count() != 1)
            {
                var resultCount = 10;
                var fishSpeciesFiltered = db.Set<FishSpecies>() as IQueryable<FishSpecies>;
                var fishPriceFiltered = db.Set<FishPrice>() as IQueryable<FishPrice>;
                fishSpeciesFiltered = fishSpeciesFiltered.Where(s =>
                    s.Code.ToLower().Contains(filter.ToLower())
                    || s.OldCode.ToLower().Contains(filter.ToLower())
                    || s.Name.ToLower().Contains(filter.ToLower())
                    || s.OldName.ToLower().Contains(filter.ToLower())
                    ).Take(resultCount);
                var tempFishSpecies = fishSpeciesFiltered.FirstOrDefault();
                listFiltered = fishPriceFiltered.Where(x => x.FishSpeciesCode == tempFishSpecies.Code
                    || x.FishSpeciesCode.ToLower().Equals(tempFishSpecies.Code.ToLower())
                    || x.FishSpeciesCode.ToLower().Equals(tempFishSpecies.OldCode.ToLower())
                    ).OrderByDescending(x => x.Id).Take(resultCount);
            }

            var fishPrice = listFiltered.FirstOrDefault();
            var fishSpecies = fishPrice != null ? db.FishSpecies.Where(x => x.Code.ToLower().Equals(fishPrice.FishSpeciesCode.ToLower()) || x.OldCode.ToLower().Equals(fishPrice.FishSpeciesCode.ToLower())).FirstOrDefault() : null;
            var fishName = fishSpecies != null ? fishSpecies.Name : null;
            var result = listFiltered.Select(m => new FishPriceViewModel()
            {
                Id = m.Id,
                FishSpeciesCode = m.FishSpeciesCode,
                Name = fishName,
                Price = m.Price,
                From = m.From,
                To = m.To,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
            //return Json(GetFiltered(filter, request).Data, JsonRequestBehavior.AllowGet);
            //var filtered = new FishSpeciesHandler().GetFiltered(db.FishSpecies, filter, 1).OrderBy(x=>x.;
            //var result = GetFiltered(request, filtered);
            //return Json(result.Data, JsonRequestBehavior.AllowGet);
            //return Json(GetFiltered(filter, request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<FishPrice> PagedListFilter(IQueryable<FishPrice> list, string filterName = null)
        {
            return list.Where(s => s.Price.ToString().ToLower().Contains(filterName.ToLower()) || s.FishSpeciesCode.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(FishPrice obj)
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
            var listFiltered = db.Set<FishPrice>() as IQueryable<FishPrice>;
            if (filter.Length > 1)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.Price.ToString().ToLower().Contains(filter.ToLower()) || s.FishSpeciesCode.ToLower().Contains(filter.ToLower()));
            }
            listFiltered = listFiltered.OrderBy(x => x.FishSpeciesCode).ThenBy(x => x.From).ThenBy(x => x.To).ThenBy(x => x.ChangeEvent.UpdatedOn);
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<FishPrice> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new FishPriceViewModel()
            {
                Id = m.Id,
                Price = m.Price,
                From = m.From,
                To = m.To,
                FishSpeciesCode = m.FishSpeciesCode,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<FishPrice>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FishPriceCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new FishPrice()
                {
                    Price = o.Price,
                    From = o.From,
                    To = o.To,
                    FishSpeciesCode = o.FishSpeciesCode
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<FishPrice>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new FishPriceCreateEditViewModel()
                {
                    Price = o.Price,
                    From = o.From,
                    To = o.To,
                    FishSpeciesCode = o.FishSpeciesCode
                };
            }
            return Edit<FishPrice>("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FishPriceCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<FishPrice>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Price = vmObj.Price;
                dbObj.From = vmObj.From;
                dbObj.To = vmObj.To;
                dbObj.FishSpeciesCode = vmObj.FishSpeciesCode;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<FishPrice>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(FishPriceCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<FishPrice>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FishPriceViewModel dbObj)
        {
            return DeleteObject<FishPrice, FishPrice>(dbObj.Id);
        }
    }
}