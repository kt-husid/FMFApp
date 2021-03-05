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
    public class TripDeductionController : ApiController<TripDeduction>
    {
        protected override IQueryable<TripDeduction> PagedListFilter(IQueryable<TripDeduction> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.PortOfLanding.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(TripDeduction))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/TripDeduction/5
        [ResponseType(typeof(TripDeduction))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/TripDeduction/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, TripDeduction obj)
        {
            return base.Put(id, obj);
        }

        // PUT: api/TripDeduction/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string obj)
        {
            TripDeduction tripDeduction = JsonConvert.DeserializeObject<TripDeduction>(obj);
            return base.Post(tripDeduction);
        }

        // POST: api/TripDeduction
        [ResponseType(typeof(TripDeduction))]
        public override IHttpActionResult Post(TripDeduction obj)
        {
            return base.Post(obj);
        }

        [ResponseType(typeof(TripDeduction))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}