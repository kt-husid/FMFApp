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
    public class SocialNetworkController : ApiController<SocialNetwork>
    {
        protected override IQueryable<SocialNetwork> PagedListFilter(IQueryable<SocialNetwork> list, string filterName = null)
        {
            return list.Where(s => s.UserName.Contains2(filterName)
                                || s.Uri.Contains2(filterName));
        }

        [ResponseType(typeof(SocialNetwork))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/SocialNetwork/5
        [ResponseType(typeof(SocialNetwork))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/SocialNetwork/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, SocialNetwork SocialNetwork)
        {
            return base.Put(id, SocialNetwork);
        }

        // PUT: api/SocialNetwork/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string SocialNetworkObject)
        {
            SocialNetwork SocialNetwork = JsonConvert.DeserializeObject<SocialNetwork>(SocialNetworkObject);
            return base.Post(SocialNetwork);
        }

        // POST: api/SocialNetwork
        [ResponseType(typeof(SocialNetwork))]
        public override IHttpActionResult Post(SocialNetwork SocialNetwork)
        {
            return base.Post(SocialNetwork);
        }

        [ResponseType(typeof(SocialNetwork))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}