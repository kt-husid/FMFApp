using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Newtonsoft.Json;

namespace BootstrapWebApplication.Controllers.API
{
    public class TripController : ApiController<Trip>
    {
        protected override IQueryable<Trip> PagedListFilter(IQueryable<Trip> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.From.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.To.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(Trip))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Trip/5
        [ResponseType(typeof(Trip))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Trip/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Trip Trip)
        {
            return base.Put(id, Trip);
        }

        // PUT: api/Trip/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string TripObject)
        {
            Trip Trip = JsonConvert.DeserializeObject<Trip>(TripObject);
            
            return base.Post(Trip);
        }

        // POST: api/Trip
        [ResponseType(typeof(Trip))]
        public override IHttpActionResult Post(Trip Trip)
        {
            return base.Post(Trip);
        }

        [ResponseType(typeof(Trip))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [ResponseType(typeof(TripCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetTripCreateRelatedDataListViewModel(string type = null)
        {
            var viewModel = new TripCreateEditGetRelatedDataViewModel()
            {
                Companies = db.Companies.ToList(),
                Ships = db.Ships.ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

        [ResponseType(typeof(TripCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetTripEditRelatedDataListViewModel(string type = null)
        {
            var viewModel = new TripCreateEditGetRelatedDataViewModel()
            {
                Companies = db.Companies.ToList(),
                Ships = db.Ships.ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

        public HttpResponseMessage GetInfo(int? tripId, string type = null)
        {
            var tripLines = db.TripLines.Where(x => x.TripId == tripId).FilterDeleted();
            var tripDeductions = db.TripDeductions.Where(x => x.TripId == tripId).FilterDeleted();
            var tripLinesTotalCost = tripLines.Sum(x => x.Amount * x.UnitPrice);
            var tripLinesTotalWeight = tripLines.Sum(x => x.Amount);
            var tripDeductionTotalCost = tripDeductions.Sum(x => x.Amount * x.Price);
            var tripDeductionTotalWeight = tripDeductions.Sum(x => x.Amount);
            return Request.GetResponse(new {
                TripLinesTotalCost = tripLinesTotalCost,
                TripLinesTotalWeight = tripLinesTotalWeight,
                TripDeductionTotalCost = tripDeductionTotalCost,
                TripDeductionTotalWeight = tripDeductionTotalWeight
            }, type);
        }

    }
}