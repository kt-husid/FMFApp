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
    public class FishSpeciesController : ApiController<FishSpecies>
    {
        protected override IQueryable<FishSpecies> PagedListFilter(IQueryable<FishSpecies> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || ("" + s.OldCode).ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || ("" + s.OldName).ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(FishSpecies))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/FishSpecies/5
        [ResponseType(typeof(FishSpecies))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/FishSpecies/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, FishSpecies FishSpecies)
        {
            return base.Put(id, FishSpecies);
        }

        // PUT: api/FishSpecies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string FishSpeciesObject)
        {
            FishSpecies FishSpecies = JsonConvert.DeserializeObject<FishSpecies>(FishSpeciesObject);

            return base.Post(FishSpecies);
        }

        // POST: api/FishSpecies
        [ResponseType(typeof(FishSpecies))]
        public override IHttpActionResult Post(FishSpecies FishSpecies)
        {
            return base.Post(FishSpecies);
        }

        [ResponseType(typeof(FishSpecies))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        // GET: api/FishSpecies/GetFromFishSpeciesCode
        [ResponseType(typeof(FishSpecies))]
        public HttpResponseMessage GetFromCode(string code, string type = null)
        {
            //var obj = db.FishSpecies.Where(x => x.Code.Equals(code) || x.OldCode.Equals(code)).FirstOrDefault();
            var obj = db.FishSpecies.Where(s => s.Code.ToLower().Contains(code.ToLower()) || (""+s.OldCode).ToLower().Contains(code.ToLower())).FirstOrDefault();
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // GET: api/FishSpecies/GetAll
        [ResponseType(typeof(ICollection<FishSpecies>))]
        public HttpResponseMessage GetAll(string type = null)
        {
            return Request.GetResponse(db.FishSpecies.ToList(), type);
        }

        [ResponseType(typeof(ICollection<FishSpecies>))]
        [HttpGet]
        [EnableQuery]
        public HttpResponseMessage Find(string filter, string type = null)
        {
            var filtered = new FishSpeciesHandler().GetFiltered(db.FishSpecies, filter, 3);
            return Request.GetResponse(filtered, type);
        }
    }
}