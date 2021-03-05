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
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers.API
{
    public class ShippingCompanyController : ApiController<ShippingCompany>
    {
        protected override IQueryable<ShippingCompany> PagedListFilter(IQueryable<ShippingCompany> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                || s.Name.ToUpper().Contains(filterName.ToUpper())
                                );
        }

        [ResponseType(typeof(ShippingCompany))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/ShippingCompany/5
        [ResponseType(typeof(ShippingCompany))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/ShippingCompany/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, ShippingCompany ShippingCompany)
        {
            return base.Put(id, ShippingCompany);
        }

        // PUT: api/ShippingCompany/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string ShippingCompanyObject)
        {
            ShippingCompany ShippingCompany = JsonConvert.DeserializeObject<ShippingCompany>(ShippingCompanyObject);
            return base.Post(ShippingCompany);
        }

        // POST: api/ShippingCompany
        [ResponseType(typeof(ShippingCompany))]
        public override IHttpActionResult Post(ShippingCompany ShippingCompany)
        {
            return base.Post(ShippingCompany);
        }

        [ResponseType(typeof(ShippingCompany))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpGet]
        [ResponseType(typeof(ShipTripYearInfo))]
        public HttpResponseMessage GetTripInfoForYear(int id, int year, string type = "json")
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var handler = new ShippingCompanyHandler(obj);
            return Request.GetResponse(handler.GetTripInfoForYear(year), type);
        }

    }
}