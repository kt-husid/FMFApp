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
    public class SignOnController : ApiController<SignOn>
    {
        protected override IQueryable<SignOn> PagedListFilter(IQueryable<SignOn> list, string filterName = null)
        {
            return list.Where(s => s.ShipCode.ToString().Contains(filterName));
        }

        [ResponseType(typeof(SignOn))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/SignOn/5
        [ResponseType(typeof(SignOn))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/SignOn/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, SignOn SignOn)
        {
            return base.Put(id, SignOn);
        }

        // PUT: api/SignOn/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string SignOnObject)
        {
            SignOn SignOn = JsonConvert.DeserializeObject<SignOn>(SignOnObject);
            return base.Post(SignOn);
        }

        // POST: api/SignOn
        [ResponseType(typeof(SignOn))]
        public override IHttpActionResult Post(SignOn SignOn)
        {
            return base.Post(SignOn);
        }

        [ResponseType(typeof(SignOn))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}