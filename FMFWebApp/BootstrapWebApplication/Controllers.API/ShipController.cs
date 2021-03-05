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
    public class ShipController : ApiController<Ship>
    {
        protected override IQueryable<Ship> PagedListFilter(IQueryable<Ship> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(Ship))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Ship/5
        [ResponseType(typeof(Ship))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Ship/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Ship Ship)
        {
            return base.Put(id, Ship);
        }

        // PUT: api/Ship/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string ShipObject)
        {
            Ship Ship = JsonConvert.DeserializeObject<Ship>(ShipObject);

            return base.Post(Ship);
        }

        // POST: api/Ship
        [ResponseType(typeof(Ship))]
        public override IHttpActionResult Post(Ship Ship)
        {
            return base.Post(Ship);
        }

        [ResponseType(typeof(Ship))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpGet]
        [ResponseType(typeof(ICollection<SignOn>))]
        public HttpResponseMessage SignOns(int shipId, string type = "json")
        {
            return Request.GetResponse(db.SignOns.FilterDeleted().Where(x => x.Ship.Id == shipId).ToList(), type);
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
            var handler = new ShipHandler(obj);
            return Request.GetResponse(handler.GetTripInfoForYear(year), type);
        }

        // GET: api/Ship/GetFromCode
        [ResponseType(typeof(Ship))]
        public HttpResponseMessage GetFromCode(string code, string type = null)
        {
            var obj = db.Set<Ship>().Where(x => x.Code == code).FirstOrDefault();
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // GET: api/Ship/GetAll
        [ResponseType(typeof(ICollection<Ship>))]
        public HttpResponseMessage GetAll(string type = null)
        {
            return Request.GetResponse(db.Ships.ToList(), type);
        }

        [ResponseType(typeof(ShipCreateEditGetDataViewModel))]
        public HttpResponseMessage GetShipCreateOrEditDataListViewModel(string type = null)
        {
            var viewModel = new ShipCreateEditGetDataViewModel()
            {
                ShippingCompanies = db.ShippingCompanies.OrderBy(x => x.Name).ToList(),
                ShipTypes = db.ShipTypes.OrderBy(x => x.Code).ToList()
            };
            return Request.GetResponse(viewModel, type);
        }
    }
}