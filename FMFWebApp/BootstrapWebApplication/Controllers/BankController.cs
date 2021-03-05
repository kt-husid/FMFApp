using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.DAL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Globalization;

namespace BootstrapWebApplication.Controllers
{
    public class BankController : Controller<Bank>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Banks as IQueryable<Bank>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.RegNumber).ToLower().Contains(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new BankViewModel()
            {
                Id = m.Id,
                Code = m.RegNumber,
                Name = m.Name,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<Bank> PagedListFilter(IQueryable<Bank> list, string filterName = null)
        {
            return list.Where(s => s.RegNumber.ToString().ToLower().Contains(filterName.ToLower()) || s.Name.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(Bank obj)
        {
            
        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var filter = "";
            if (request.Filters.Count > 0)
            {
                filter = (request.Filters[0] as Kendo.Mvc.FilterDescriptor).Value as string;
            }
            request.Filters.Clear();
            return Json(GetFiltered(filter, request));
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        private DataSourceResult GetFiltered(string filter, DataSourceRequest request)
        {
            var listFiltered = db.Set<Bank>() as IQueryable<Bank>;
            if (filter.Length > 3)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.RegNumber.ToString().ToLower().Equals(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<Bank> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new BankViewModel()
            {
                Id = m.Id,
                Code = m.RegNumber,
                Name = m.Name,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<Bank>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Bank()
                {
                    RegNumber = o.Code,
                    Name = o.Name
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.Set<Bank>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new BankCreateEditViewModel()
                {
                    Code = obj.RegNumber,
                    Name = obj.Name
                };
            }
            return Edit<Bank>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<Bank>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.RegNumber = vmObj.Code;
                dbObj.Name = vmObj.Name;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<Bank>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(BankCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Bank>().Find(id);
            return Delete("Delete", obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BankViewModel dbObj)
        {
            return DeleteObject<Bank, Bank>(dbObj.Id);
        }
    }
}