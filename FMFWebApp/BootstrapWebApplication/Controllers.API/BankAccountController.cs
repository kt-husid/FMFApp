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
    public class BankAccountController : ApiController<BankAccount>
    {
        protected override IQueryable<BankAccount> PagedListFilter(IQueryable<BankAccount> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.IsPrimary.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Bank.RegNumber.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Bank.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.AccountNumber.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [ResponseType(typeof(BankAccount))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/BankAccount/5
        [ResponseType(typeof(BankAccount))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/BankAccount/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, BankAccount BankAccount)
        {
            return base.Put(id, BankAccount);
        }

        // PUT: api/BankAccount/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string BankAccountObject)
        {
            BankAccount BankAccount = JsonConvert.DeserializeObject<BankAccount>(BankAccountObject);
            
            return base.Post(BankAccount);
        }

        // POST: api/BankAccount
        [ResponseType(typeof(BankAccount))]
        public override IHttpActionResult Post(BankAccount BankAccount)
        {
            return base.Post(BankAccount);
        }

        [ResponseType(typeof(BankAccount))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

    }
}