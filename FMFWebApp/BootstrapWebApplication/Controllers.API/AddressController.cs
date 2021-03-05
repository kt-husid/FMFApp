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
    public class AddressController : ApiController<Address>
    {
        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        protected override IQueryable<Address> PagedListFilter(IQueryable<Address> list, string filterName = null)
        {
            return list.Where(s => s.AddressLine1.ToUpper().Contains(filterName.ToUpper())
                                    || s.AddressLine2.ToUpper().Contains(filterName.ToUpper())
                                    || s.Postal.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                    || s.Postal.CityName.ToUpper().Contains(filterName.ToUpper())
                                    || s.Country.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(Address))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Address/5
        /// <summary>
        /// Get a specific Address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">json or xml</param>
        /// <returns></returns>
        [ResponseType(typeof(Address))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Address/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Address Address)
        {
            return base.Put(id, Address);
        }

        // PUT: api/Address/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string AddressObject)
        {
            Address Address = JsonConvert.DeserializeObject<Address>(AddressObject);
            return base.Post(Address);
        }

        // POST: api/Address
        [ResponseType(typeof(Address))]
        public override IHttpActionResult Post(Address Address)
        {
            return base.Post(Address);
        }

        [ResponseType(typeof(Address))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}