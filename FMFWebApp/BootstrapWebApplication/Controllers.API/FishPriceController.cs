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
using System.Web.Http.OData;

namespace BootstrapWebApplication.Controllers.API
{
    public class FishPriceController : ApiController<FishPrice>
    {
        protected override IQueryable<FishPrice> PagedListFilter(IQueryable<FishPrice> list, string filterName = null)
        {
            return list.Where(s => ("" + s.FishSpeciesCode).ToString().ToUpper().Contains(filterName.ToUpper())
                                   || ("" + s.Price).ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(FishPrice))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/FishPrice/5
        [ResponseType(typeof(FishPrice))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/FishPrice/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, FishPrice FishPrice)
        {
            return base.Put(id, FishPrice);
        }

        // PUT: api/FishPrice/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string FishPriceObject)
        {
            FishPrice FishPrice = JsonConvert.DeserializeObject<FishPrice>(FishPriceObject);

            return base.Post(FishPrice);
        }

        // POST: api/FishPrice
        [ResponseType(typeof(FishPrice))]
        public override IHttpActionResult Post(FishPrice FishPrice)
        {
            return base.Post(FishPrice);
        }

        [ResponseType(typeof(FishPrice))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        // POST: api/FishPrice/UpdateFishPrice
        [ResponseType(typeof(FishPrice))]
        public HttpResponseMessage UpdateFishPrice(FishPriceTemp fishPriceTemp)
        {
            //if (fishPriceTemp != null)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}
            //var obj = GetFishPrice(fishSpeciesCode, fishSpeciesOldCode);
            var obj = GetFishPrice(fishPriceTemp.FishSpeciesCode, fishPriceTemp.FishSpeciesOldCode);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            obj.Price = fishPriceTemp.NewPrice; //newPrice;
            base.Put(obj.Id, obj);
            return Request.GetResponse(obj, "json");
        }

        public HttpResponseMessage GetFishPrice(string fishSpeciesCode, string fishSpeciesOldCode, string type = null)
        {
            //var fishSpeciesPrice = db.FishPrices.OrderByDescending(x => x.Id).Where(x => x.ChangeEvent.UpdatedOn.Year == DateTime.UtcNow.Year && x.FishSpeciesCode == fishSpeciesCode).FirstOrDefault();
            //if (fishSpeciesPrice == null)
            //{
            //    fishSpeciesPrice = db.FishPrices.OrderByDescending(x => x.Id).Where(x => x.FishSpeciesCode == fishSpeciesCode).FirstOrDefault();
            //}
            //return Request.GetResponse(fishSpeciesPrice, type);
            
            return Request.GetResponse(GetFishPrice(fishSpeciesCode, fishSpeciesOldCode), type);
        }

        private FishPrice GetFishPrice(string fishSpeciesCode, string fishSpeciesOldCode)
        {
            var fishPrice = db.FishPrices.OrderByDescending(x => x.Id).Where(x => x.ChangeEvent.UpdatedOn.Year == DateTime.UtcNow.Year && (x.FishSpeciesCode.ToLower().Equals(fishSpeciesCode) || x.FishSpeciesCode.ToLower().Equals(fishSpeciesOldCode))).FirstOrDefault();
            if (fishPrice == null)
            {
                fishPrice = db.FishPrices.OrderByDescending(x => x.Id).Where(x => x.FishSpeciesCode.ToLower().Equals(fishSpeciesCode) || x.FishSpeciesCode.ToLower().Equals(fishSpeciesOldCode)).FirstOrDefault();
            }
            return fishPrice;
        }

    }
}