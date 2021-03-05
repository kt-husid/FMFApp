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
    public class WebsiteController : ApiController<Website>
    {
        protected override IQueryable<Website> PagedListFilter(IQueryable<Website> list, string filterName = null)
        {
            return list.Where(s => s.Name.ToUpper().Contains(filterName.ToUpper())
                                || s.Uri.ToUpper().Contains(filterName.ToUpper())
                                || s.Description.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(Website))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Website/5
        [ResponseType(typeof(Website))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Website/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Website Website)
        {
            return base.Put(id, Website);
        }

        // PUT: api/Website/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string WebsiteObject)
        {
            Website Website = JsonConvert.DeserializeObject<Website>(WebsiteObject);
            return base.Post(Website);
        }

        // POST: api/Website
        [ResponseType(typeof(Website))]
        public override IHttpActionResult Post(Website Website)
        {
            return base.Post(Website);
        }

        [ResponseType(typeof(Website))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}