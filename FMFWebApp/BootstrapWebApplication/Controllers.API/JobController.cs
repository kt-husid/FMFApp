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
    public class JobController : ApiController<Job>
    {
        protected override IQueryable<Job> PagedListFilter(IQueryable<Job> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToString().ToUpper().Contains(filterName.ToUpper()) || s.Description.ToString().ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(Job))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Job/5
        [ResponseType(typeof(Job))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Job/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Job Job)
        {
            return base.Put(id, Job);
        }

        // PUT: api/Job/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string JobObject)
        {
            Job Job = JsonConvert.DeserializeObject<Job>(JobObject);
            
            return base.Post(Job);
        }

        // POST: api/Job
        [ResponseType(typeof(Job))]
        public override IHttpActionResult Post(Job Job)
        {
            return base.Post(Job);
        }

        [ResponseType(typeof(Job))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        // GET: api/Job/GetFromCode
        [ResponseType(typeof(Job))]
        public HttpResponseMessage GetFromCode(string code, string type = null)
        {
            var obj = db.Set<Job>().FilterDeleted().Where(x => x.Code == code).FirstOrDefault();
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // GET: api/Job/GetAll
        [ResponseType(typeof(ICollection<Job>))]
        public HttpResponseMessage GetAll(string type = null)
        {
            return Request.GetResponse(db.Jobs.ToList(), type);
        }

    }
}