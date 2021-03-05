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
    public class CompanyCommentController : ApiController<CompanyComment>
    {
        protected override IQueryable<CompanyComment> PagedListFilter(IQueryable<CompanyComment> list, string filterName = null)
        {
            return list.Where(s => s.Text.ToUpper().Contains(filterName.ToUpper()));
        }

        [ResponseType(typeof(CompanyComment))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/CompanyComment/5
        [ResponseType(typeof(CompanyComment))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/CompanyComment/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, CompanyComment CompanyComment)
        {
            return base.Put(id, CompanyComment);
        }

        // PUT: api/CompanyComment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string CompanyCommentObject)
        {
            CompanyComment CompanyComment = JsonConvert.DeserializeObject<CompanyComment>(CompanyCommentObject);
            return base.Post(CompanyComment);
        }

        // POST: api/CompanyComment
        [ResponseType(typeof(CompanyComment))]
        public override IHttpActionResult Post(CompanyComment CompanyComment)
        {
            return base.Post(CompanyComment);
        }

        [ResponseType(typeof(CompanyComment))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}