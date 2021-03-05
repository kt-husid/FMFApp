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
    public class PersonTitleController : ApiController<PersonTitle>
    {
        protected override IQueryable<PersonTitle> PagedListFilter(IQueryable<PersonTitle> list, string filterName = null)
        {
            return list.Where(s => s.Description.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(PersonTitle))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/PersonTitle/5
        [ResponseType(typeof(PersonTitle))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/PersonTitle/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, PersonTitle personTitle)
        {
            return base.Put(id, personTitle);
        }

        // PUT: api/PersonTitle/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string personTitleObject)
        {
            PersonTitle personTitle = JsonConvert.DeserializeObject<PersonTitle>(personTitleObject);
            return base.Post(personTitle);
        }

        // POST: api/PersonTitle
        [ResponseType(typeof(PersonTitle))]
        public override IHttpActionResult Post(PersonTitle personTitle)
        {
            return base.Post(personTitle);
        }

        [ResponseType(typeof(PersonTitle))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}