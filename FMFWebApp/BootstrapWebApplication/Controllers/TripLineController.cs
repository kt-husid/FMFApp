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
using BootstrapWebApplication.BLL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class TripLineController : Controller<TripLine>
    {

        protected override IQueryable<TripLine> PagedListFilter(IQueryable<TripLine> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                    || s.Trip.PortOfLanding.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                    || s.Trip.Ship.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                //|| s.Company.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                //|| s.Ship.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        protected override void AddViewBag(TripLine obj)
        {
            ViewBag.FishSpeciesId = new SelectList(db.FishSpecies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name + " (" + m.Code + ")",
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.FishSpeciesId : null);

            ViewBag.CompanyId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name + " (" + m.Code + ")",
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.CompanyId : null);

            //ViewBag.CompanyId = new SelectList(db.Companies, "Id", "CID");
            //ViewBag.CompanyId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name + " (" + m.CompanyCode + ")",
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.CompanyId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.CompanyId = new SelectList(db.Companies, "Id", "CID");
            //ViewBag.FishSpeciesId = new SelectList(db.FishSpecies, "Id", "Code");
            //ViewBag.PostalId = new SelectList(db.Postals, "Id", "CityName");
            //ViewBag.ShipId = new SelectList(db.Ships, "Id", "Code");
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies, "Id", "Name");
            //ViewBag.TripId = new SelectList(db.Trips, "Id", "TripId");
            //ViewBag.ShipTypeId = new SelectList(db.ShipTypes, "Id", "TEKSTUR");
        }

        public ActionResult TripLines(int? id)
        {
            return GetRelatedDataForParent<Trip>(id, "TripLines");
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var trip = db.Set<Trip>().Find(id);
            if (trip != null)
            {
                // load triplines explicitly
                db.Entry(trip).Collection(x => x.TripLines).Load();

                var result = trip.TripLines.FilterDeleted().OrderByDescending(x => x.Id).Select(m => new TripTripLineViewModel()
                {
                    Id = m.Id,
                    TripId = m.TripId,
                    FishSpeciesId = m.FishSpecies != null ? new Nullable<int>(m.FishSpecies.Id) : null,
                    FishSpeciesCode = m.FishSpecies != null ? m.FishSpecies.Code : "",
                    FishSpeciesName = m.FishSpecies != null ? m.FishSpecies.Name : "",
                    Amount = m.Amount,
                    Price = m.UnitPrice,
                    KR = m.TotalPrice,
                    CompanyName = m.Company != null ? m.Company.Name : "",
                    PortOfLanding = m.PortOfLanding,
                    DateOfLanding = m.DateOfLanding != null ? new Nullable<DateTime>(m.DateOfLanding) : null,
                    UpdatedOn = m.ChangeEvent != null ? new Nullable<DateTime>(m.ChangeEvent.UpdatedOn) : null,
                    UpdatedByUserIdCode = m.ChangeEvent != null ? m.ChangeEvent.UpdatedByUserIdCode : null
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        //[HttpGet]
        //public JsonResult GetCreateData(int? tripId)
        //{
        //    var parent = db.Set<Trip>().Find(tripId);
        //    return Json(new TripTripLineCreateViewModel()
        //    {
        //        //CompanyCode = parent.PortOfLanding.Code,
        //        CompanyId = parent.PortOfLandingId,
        //        DateOfLanding = parent.DateOfLanding
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult Create(int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            var viewModel = new TripTripLineCreateViewModel()
            {
                //CompanyCode = parent.PortOfLanding.Code,
                //CompanyId = parent.PortOfLandingId,
                //DateOfLanding = parent.DateOfLanding
            };
            AddViewBag(null);
            return Create("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripTripLineCreateViewModel o)
        {
            // Convert the ViewModel to DB Object (Model)
            if (!o.TripId.HasValue)
            {
                return null;
            }
            var trip = db.Trips.Where(x => x.Id == o.TripId).FirstOrDefault();
            //var typa = db.ShipTypes.Where(s => s.TypaCode == o.TypaId).FirstOrDefault();
            //var postal = db.Postals.Where(s => s.PostalCode == o.PostNrPostNr).FirstOrDefault();
            var fishSpecies = db.FishSpecies.Where(x => x.Code.ToLower().Equals(o.FishSpeciesCode.ToLower())).FirstOrDefault();

            if (trip == null || fishSpecies == null)
            {
                return null;
            }

            var result = CreateRelatedObjectUsingViewModel<Trip, TripLine>(o.TripId, o, (parent) =>
            {
                var dbObj = new TripLine()
                {
                    // start temp properties
                    //ShippingCompanyCode = o.ReidariId,
                    //Code = o.SkipId,
                    //FishSpeciesCode = o.FiskArtId,
                    //TypaCode = o.TypaId,
                    //PostalCode = o.PostNrPostNr,
                    //CompanyCode = o.AVR_STAD,
                    // end temp properties
                    ChangeEvent = CreateChangeEvent(),
                    TripNumber = trip.TripNumber,// trip.TripId,
                    Amount = o.Amount,
                    UnitPrice = o.UnitPrice,
                    AlternativePrice = o.Amount * o.UnitPrice,
                    KR = o.Amount * o.UnitPrice,
                    PortOfLanding = trip.PortOfLanding.Code,// o.PortOfLanding,
                    DateOfLanding = trip.DateOfLanding.Value,// o.DateOfLanding.Value,
                    //RAD = o.RAD,
                    Year = trip.DateOfLanding.Value.ToString("yy"),
                    //SK_ID = o.SK_ID,
                    STUD_M = null,
                    STUD_S = null,
                    //ShippingCompany = shippingCompany,
                    //Ship = ship,
                    Trip = trip,
                    FishSpecies = fishSpecies,
                    //Typa = typa,
                    //Postal = postal,
                    Company = trip.PortOfLanding
                };
                return dbObj;
            });

            TripHandler.UpdateTripFromTripLine(trip, db, this);

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load triplines explicitly
            db.Entry(parent).Collection(x => x.TripLines).Load();
            var obj = parent != null ? parent.TripLines.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new TripTripLineCreateViewModel()
                {
                    Id = obj.Id,
                    FishSpeciesCode = obj.FishSpecies.Code,
                    FishSpeciesId = obj.FishSpeciesId,
                    Amount = obj.Amount,
                    UnitPrice = obj.UnitPrice
                    //CompanyId = obj.CompanyId,
                    //DateOfLanding = obj.DateOfLanding
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripTripLineCreateViewModel vmObj)
        {
            Trip trip = null;
            var result = UpdateRelatedObjectUsingViewModel<Trip, TripLine>(vmObj.TripId, vmObj, (parent) =>
            {
                // load triplines explicitly
                db.Entry(parent).Collection(x => x.TripLines).Load();
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.TripLines.Where(x => x.Id == vmObj.Id).FirstOrDefault();

                trip = parent; // important


                //var trip = db.Trips.Where(x => x.Id == o.TripId).FirstOrDefault();
                //var fishSpecies = db.FishSpecies.Where(x => x.Id == o.FishSpeciesId).FirstOrDefault();

                // start temp properties
                //ShippingCompanyCode = o.ReidariId,
                //Code = o.SkipId,
                //FishSpeciesCode = o.FiskArtId,
                //TypaCode = o.TypaId,
                //PostalCode = o.PostNrPostNr,
                //CompanyCode = o.AVR_STAD,
                // end temp properties
                //dbObj.ChangeEvent = CreateChangeEvent();
                //dbObj.TripNumber = null;
                dbObj.Amount = vmObj.Amount;
                dbObj.UnitPrice = vmObj.UnitPrice;
                dbObj.AlternativePrice = null;
                //KR = 
                //dbObj.PortOfLanding = vmObj.PortOfLanding;
                //dbObj.DateOfLanding = vmObj.DateOfLanding.Value;
                //RAD = vmObj.RAD;
                //dbObj.Year = dbObj.DateOfLanding.ToString("yy");
                //SK_ID = vmObj.SK_ID;
                dbObj.STUD_M = null;
                dbObj.STUD_S = null;
                //ShippingCompany = shippingCompany;
                //Ship = ship;
                //dbObj.TripId = vmObj.TripId;// parent.Id;
                //dbObj.FishSpeciesId = vmObj.FishSpeciesId;
                //Typa = typa;
                //Postal = postal;
                //Company = company
                return dbObj;
            });

            TripHandler.UpdateTripFromTripLine(trip, db, this);

            return result;
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load triplines explicitly
            db.Entry(parent).Collection(x => x.TripLines).Load();
            var obj = parent != null ? parent.TripLines.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TripTripLineViewModel dbObj)
        {
            var trip = db.Trips.Where(x => x.Id == dbObj.TripId).FirstOrDefault();
            if (trip != null)
            {
                TripHandler.UpdateTripFromTripLine(trip, db, this);
            }
            return DeleteObject<TripLine, Trip>(dbObj.Id, dbObj.TripId);
        }
    }
}