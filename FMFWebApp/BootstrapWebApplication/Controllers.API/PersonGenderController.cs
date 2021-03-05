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
    public class PersonGenderController : ApiController<PersonGender>
    {
        protected override IQueryable<PersonGender> PagedListFilter(IQueryable<PersonGender> list, string filterName = null)
        {
            return list.Where(s => s.Description.ToUpper().Contains(filterName.ToUpper())
                                || s.Description.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(PersonGender))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/PersonGender/5
        [ResponseType(typeof(PersonGender))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/PersonGender/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, PersonGender PersonGender)
        {
            return base.Put(id, PersonGender);
        }

        // PUT: api/PersonGender/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string PersonGenderObject)
        {
            PersonGender PersonGender = JsonConvert.DeserializeObject<PersonGender>(PersonGenderObject);
            return base.Post(PersonGender);
        }

        // POST: api/PersonGender
        [ResponseType(typeof(PersonGender))]
        public override IHttpActionResult Post(PersonGender PersonGender)
        {
            return base.Post(PersonGender);
        }

        [ResponseType(typeof(PersonGender))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}