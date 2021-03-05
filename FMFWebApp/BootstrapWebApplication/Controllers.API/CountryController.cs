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
    public class CountryController : ApiController<Country>
    {
        protected override IQueryable<Country> PagedListFilter(IQueryable<Country> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(Country))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Country/5
        [ResponseType(typeof(Country))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Country/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Country Country)
        {
            return base.Put(id, Country);
        }

        // PUT: api/Country/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string CountryObject)
        {
            Country Country = JsonConvert.DeserializeObject<Country>(CountryObject);
            return base.Post(Country);
        }

        // POST: api/Country
        [ResponseType(typeof(Country))]
        public override IHttpActionResult Post(Country Country)
        {
            return base.Post(Country);
        }

        [ResponseType(typeof(Country))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }


        // GET: api/Country/GetFromCode
        [ResponseType(typeof(Country))]
        public HttpResponseMessage GetFromCode(string code, string type = null)
        {
            var obj = db.Countries.Where(x => x.Code == code).FirstOrDefault();
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // GET: api/Country/GetAll
        [ResponseType(typeof(ICollection<Country>))]
        public HttpResponseMessage GetAll(string type = null)
        {
            return Request.GetResponse(db.Countries.ToList(), type);
        }

    }
}