using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers
{
    public class TripDeductionController : Controller<TripDeduction>
    {
        protected override IQueryable<TripDeduction> PagedListFilter(IQueryable<TripDeduction> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.PortOfLanding.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        protected override void AddViewBag(TripDeduction tripDeduction)
        {
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.CompanyId = new SelectList(db.Companies, "Id", "CID");
            //ViewBag.DeductionTypeId = new SelectList(db.DeductionTypes, "Id", "Code");
            //ViewBag.PostalId = new SelectList(db.Postals, "Id", "CityName");
            //ViewBag.ShipId = new SelectList(db.Ships, "Id", "Code");
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies, "Id", "Name");
            //ViewBag.TripId = new SelectList(db.Trips, "Id", "TripId");
            //ViewBag.ShipTypeId = new SelectList(db.ShipTypes, "Id", "TEKSTUR");
        }

        public ActionResult TripDeductions(int? id)
        {
            return GetRelatedDataForParent<Trip>(id, "TripDeductions");
        }

        [HttpGet]
        public ActionResult Create(int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            var viewModel = new TripDeductionCreateOrEditViewModel()
            {
                //CompanyCode = parent.PortOfLanding.Code,
                //CompanyId = parent.PortOfLandingId,
                //DateOfLanding = parent.DateOfLanding
            };
            AddViewBag(null);
            return Create("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        public ActionResult Create(TripDeductionCreateOrEditViewModel vmObj)
        {
            // Convert the ViewModel to DB Object (Model)
            var trip = db.Trips.Where(x => x.Id == vmObj.TripId).FirstOrDefault();
            //var deductionType = db.DeductionTypes.Where(x => x.Id == o.DeductionTypeId).FirstOrDefault();
            var deductionType = db.DeductionTypes.Where(x => x.Code == vmObj.DeductionTypeCode).FirstOrDefault();
            if (trip == null || deductionType == null || !vmObj.UnitPrice.HasValue || !vmObj.Amount.HasValue)
            {
                return null;
            }
            // load tripdeductions explicitly
            db.Entry(trip).Collection(x => x.TripDeductions).Load();
            var tripDeductionFromDb = trip.TripDeductions.FilterDeleted().Where(x => x.DeductionType.Code == vmObj.DeductionTypeCode && x.Amount == vmObj.Amount).FirstOrDefault();
            if (tripDeductionFromDb != null)
            {
                //tripDeductionFromDb.Id = vmObj.Id;

                return UpdateRelatedObjectUsingViewModel<Trip, TripDeduction>(vmObj.TripId, vmObj, (parent) =>
                {
                    //Convert the ViewModel to DB Object (Model)
                    //var dbObj = parent.TripDeductions.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                    tripDeductionFromDb.Price += vmObj.UnitPrice;
                    return tripDeductionFromDb;
                });
            }
            var result = CreateRelatedObjectUsingViewModel<Trip, TripDeduction>(vmObj.TripId, vmObj, (parent) =>
            {
                var dbObj = new TripDeduction()
                {
                    TripId = trip.Id,
                    Amount = vmObj.Amount,
                    Price = vmObj.UnitPrice,
                    DateOfLanding = trip.DateOfLanding,
                    DeductionType = deductionType,
                    ShippingCompanyId = trip.Ship != null ? trip.Ship.ShippingCompanyId : null,
                    PortOfLandingId = trip.PortOfLandingId,
                    TripNumber = trip.TripNumber,
                    Ship = trip.Ship,
                    ShipType = trip.Ship != null ? trip.Ship.ShipType : null,
                    ART = deductionType != null ? deductionType.Code : null,
                };
                return dbObj;
            });

            TripHandler.UpdateTripFromTripDeduction(trip, db, this);

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load tripdeductions explicitly
            db.Entry(parent).Collection(x => x.TripDeductions).Load();
            var obj = parent != null ? parent.TripDeductions.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new TripDeductionCreateOrEditViewModel()
                {
                    DeductionTypeCode = obj.DeductionType.Code,
                    DeductionTypeId = obj.DeductionTypeId,
                    Amount = obj.Amount,
                    UnitPrice = obj.Price
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripDeductionCreateOrEditViewModel vmObj)
        {
            Trip trip = null;
            var result = UpdateRelatedObjectUsingViewModel<Trip, TripDeduction>(vmObj.TripId, vmObj, (parent) =>
            {
                // load tripdeductions explicitly
                db.Entry(parent).Collection(x => x.TripDeductions).Load();
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.TripDeductions.Where(x => x.Id == vmObj.Id).FirstOrDefault();

                trip = parent; // important

                //var deductionType = db.DeductionTypes.Where(x => x.Code == vmObj.DeductionTypeCode).FirstOrDefault();
                //dbObj.DeductionType = deductionType;
                dbObj.Price = vmObj.UnitPrice;
                dbObj.Amount = vmObj.Amount;
                return dbObj;
            });

            TripHandler.UpdateTripFromTripDeduction(trip, db, this);

            return result;
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var trip = db.Set<Trip>().Find(id);
            if (trip != null)
            {
                // load tripdeductions explicitly
                db.Entry(trip).Collection(x => x.TripDeductions).Load();

                var result = trip.TripDeductions.FilterDeleted().OrderByDescending(x => x.Id).Select(m => new TripDeductionViewModel()
                {
                    Id = m.Id,
                    TripId = m.TripId,
                    DeductionTypeCode = m.DeductionType != null ? m.DeductionType.Code : "",
                    Amount = m.Amount,
                    UnitPrice = m.Price,
                    TotalPrice = m.TotalPrice,
                    UpdatedOn = m.ChangeEvent != null ? new Nullable<DateTime>(m.ChangeEvent.UpdatedOn) : null,
                    UpdatedByUserIdCode = m.ChangeEvent != null ? m.ChangeEvent.UpdatedByUserIdCode : null
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load tripdeductions explicitly
            db.Entry(parent).Collection(x => x.TripDeductions).Load();
            var obj = parent != null ? parent.TripDeductions.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TripDeductionViewModel dbObj)
        {
            var trip = db.Trips.Where(x => x.Id == dbObj.TripId).FirstOrDefault();
            if (trip != null)
            {
                TripHandler.UpdateTripFromTripDeduction(trip, db, this);
            }
            return DeleteObject<TripDeduction, Trip>(dbObj.Id, dbObj.TripId);
        }
    }
}