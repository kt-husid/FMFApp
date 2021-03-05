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
    public class ShipTypeController : Controller<ShipType>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.ShipTypes as IQueryable<ShipType>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Contains(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new ShipTypeViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<ShipType> PagedListFilter(IQueryable<ShipType> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToString().ToLower().Contains(filterName.ToLower()) || s.Description.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(ShipType obj)
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
            var listFiltered = db.Set<ShipType>() as IQueryable<ShipType>;
            if (filter.Length >= 1)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Equals(filter.ToLower()) || ("" + s.Description).ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<ShipType> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new ShipTypeViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                YearList = m.YearList,
                PctToShip = m.PctToShip,
                PctToCrewMember = m.PctToCrewMember,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<ShipType>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipTypeCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new ShipType()
                {
                    Code = o.Code,
                    Description = o.Description,
                    YearList = o.YearList,
                    PctToShip = o.PctToShip,
                    PctToCrewMember = o.PctToCrewMember
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<ShipType>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new ShipTypeCreateEditViewModel()
                {
                    Code = o.Code,
                    Description = o.Description,
                    YearList = o.YearList,
                    PctToShip = o.PctToShip,
                    PctToCrewMember = o.PctToCrewMember
                };
            }
            return Edit<ShipType>("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShipTypeCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<ShipType>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //dbObj.Code = vmObj.Code;
                dbObj.Description = vmObj.Description;
                dbObj.YearList = vmObj.YearList;
                dbObj.PctToShip = vmObj.PctToShip;
                dbObj.PctToCrewMember = vmObj.PctToCrewMember;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<ShipType>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(ShipTypeCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<ShipType>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipTypeViewModel dbObj)
        {
            return DeleteObject<ShipType, ShipType>(dbObj.Id);
        }
    }
}