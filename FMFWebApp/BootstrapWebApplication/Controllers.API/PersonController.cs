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
using System.Web.Mvc;
using Kthusid.Web;

namespace BootstrapWebApplication.Controllers.API
{
    public class PersonController : ApiController<Person>
    {
        protected override IQueryable<Person> PagedListFilter(IQueryable<Person> list, string filterName = null)
        {
            return list.Where(s => s.FirstName.ToLower().Contains(filterName.ToLower())
                                   || s.MiddleName.ToLower().Contains(filterName.ToLower())
                                   || s.LastName.ToLower().Contains(filterName.ToLower())
                                   || s.FullName.ToLower().Contains(filterName.ToLower()));
        }

        [ResponseType(typeof(Person))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Person/5
        [ResponseType(typeof(Person))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Person Person)
        {
            return base.Put(id, Person);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string PersonObject)
        {
            Person Person = JsonConvert.DeserializeObject<Person>(PersonObject);
            return base.Post(Person);
        }

        // POST: api/Person
        [ResponseType(typeof(Person))]
        public override IHttpActionResult Post([Bind(Include = "Id,SSN,FirstName,MiddleName,LastName,Birthday,GenderId,TitleId")] Person Person)
        {
            return base.Post(Person);
        }

        [ResponseType(typeof(Person))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [ResponseType(typeof(ICollection<Website>))]
        public HttpResponseMessage Websites(int? id, string type = null)
        {
            return GetRelatedData(id, "Websites", type);
        }

        [ResponseType(typeof(ICollection<Phone>))]
        public HttpResponseMessage Phones(int? id, string type = null)
        {
            return GetRelatedData(id, "Phones", type);
        }

        [ResponseType(typeof(ICollection<Address>))]
        public HttpResponseMessage Addresses(int? id, string type = null)
        {
            return GetRelatedData(id, "Addresses", type);
        }
    }
}