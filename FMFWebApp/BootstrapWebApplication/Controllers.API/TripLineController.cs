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
    public class TripLineController : ApiController<TripLine>
    {
        protected override IQueryable<TripLine> PagedListFilter(IQueryable<TripLine> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.TripId.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(TripLine))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/TripLine/5
        [ResponseType(typeof(TripLine))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/TripLine/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, TripLine TripLine)
        {
            return base.Put(id, TripLine);
        }

        // PUT: api/TripLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string TripLineObject)
        {
            TripLine TripLine = JsonConvert.DeserializeObject<TripLine>(TripLineObject);

            return base.Post(TripLine);
        }

        // POST: api/TripLine
        [ResponseType(typeof(TripLine))]
        public override IHttpActionResult Post(TripLine TripLine)
        {
            return base.Post(TripLine);
        }

        [ResponseType(typeof(TripLine))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [ResponseType(typeof(TripLineCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetTripLineCreateRelatedDataListViewModel(string type = null)
        {
            var viewModel = new TripLineCreateEditGetRelatedDataViewModel()
            {
                Companies = db.Companies.ToList(),
                FishSpecies = db.FishSpecies.ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

        [ResponseType(typeof(TripLineCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetTripLineEditRelatedDataListViewModel(string type = null)
        {
            var viewModel = new TripLineCreateEditGetRelatedDataViewModel()
            {
                Companies = db.Companies.ToList(),
                FishSpecies = db.FishSpecies.ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

    }
}