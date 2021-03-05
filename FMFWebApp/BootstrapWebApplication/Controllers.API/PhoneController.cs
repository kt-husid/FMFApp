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
    public class PhoneController : ApiController<Phone>
    {
        protected override IQueryable<Phone> PagedListFilter(IQueryable<Phone> list, string filterName = null)
        {
            return list.Where(s => s.RawNumber.Contains2(filterName));
        }

        [ResponseType(typeof(Phone))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Phone/5
        [ResponseType(typeof(Phone))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Phone/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Phone Phone)
        {
            return base.Put(id, Phone);
        }

        // PUT: api/Phone/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string PhoneObject)
        {
            Phone Phone = JsonConvert.DeserializeObject<Phone>(PhoneObject);
            return base.Post(Phone);
        }

        // POST: api/Phone
        [ResponseType(typeof(Phone))]
        public override IHttpActionResult Post(Phone Phone)
        {
            return base.Post(Phone);
        }

        [ResponseType(typeof(Phone))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}