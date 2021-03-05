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
    public class CompanyController : ApiController<Company>
    {
        protected override IQueryable<Company> PagedListFilter(IQueryable<Company> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(Company))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Company/5
        [ResponseType(typeof(Company))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Company/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Company Company)
        {
            return base.Put(id, Company);
        }

        // PUT: api/Company/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string CompanyObject)
        {
            Company Company = JsonConvert.DeserializeObject<Company>(CompanyObject);
            return base.Post(Company);
        }

        // POST: api/Company
        [ResponseType(typeof(Company))]
        public override IHttpActionResult Post(Company Company)
        {
            return base.Post(Company);
        }

        [ResponseType(typeof(Company))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        // GET: api/Company/GetFromCode
        [ResponseType(typeof(Company))]
        public HttpResponseMessage GetFromCode(string code, string type = null)
        {
            var obj = db.Companies.Where(x => x.Code == code).FirstOrDefault();
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // GET: api/Company/GetAll
        [ResponseType(typeof(ICollection<Company>))]
        public HttpResponseMessage GetAll(string type = null)
        {
            return Request.GetResponse(db.Companies.ToList(), type);
        }

        //[ResponseType(typeof(ICollection<Company>))]
        public HttpResponseMessage GetInfoForAllTrips(int companyId, string type = null)
        {
            var company = db.Companies.Where(x => x.Id == companyId).FirstOrDefault();
            if (company == null)
            {
                return null;
            }
            var tripLineWeightCount = 0m;
            var tripLineCount = 0;
            foreach (var trip in company.Trips.FilterDeleted())
            {
                tripLineCount++;
                tripLineWeightCount += trip.TripLines.Sum(x => x.Amount);
            }
            return Request.GetResponse(new
            {
                TripLineCount = tripLineCount,//company.Trips.Count,
                TripLineWeightCount = tripLineWeightCount
            }, type);
        }
    }
}