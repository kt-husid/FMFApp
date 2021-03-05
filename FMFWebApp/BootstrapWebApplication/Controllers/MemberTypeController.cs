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
using PagedList;
using System.Linq.Expressions;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class MemberTypeController : Controller<MemberType>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.MemberTypes as IQueryable<MemberType>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Contains(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new MemberTypeViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<MemberType> PagedListFilter(IQueryable<MemberType> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToLower().Contains(filterName.ToLower()) || s.Description.ToLower().Contains(filterName.ToLower()));
        }

        protected override void AddViewBag(MemberType obj)
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
            var listFiltered = db.Set<MemberType>() as IQueryable<MemberType>;
            if (filter.Length > 1)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Equals(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower())
                );
            }
            return GetFiltered(request, listFiltered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<MemberType> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new MemberTypeViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                BREV_UT = m.BREV_UT,
                ChangeEvent = m.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBag(null);
            return Create<MemberType>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberTypeCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new MemberType()
                {
                    Code = o.Code,
                    Description = o.Description,
                    BREV_UT = o.BREV_UT
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var o = db.Set<MemberType>().Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (o != null)
            {
                AddViewBag(o);
                viewModel = new MemberTypeCreateEditViewModel()
                {
                    Code = o.Code,
                    Description = o.Description,
                    BREV_UT = o.BREV_UT
                };
            }
            return Edit<MemberType>("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberTypeCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Set<MemberType>().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //dbObj.Code = vmObj.Code;
                dbObj.Description = vmObj.Description;
                dbObj.BREV_UT = vmObj.BREV_UT;
                return dbObj;
            });
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    var obj = db.Set<MemberType>().Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(MemberTypeCreateEditViewModel dbObj)
        //{
        //    return Delete(dbObj);
        //}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<MemberType>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MemberTypeViewModel dbObj)
        {
            return DeleteObject<MemberType, MemberType>(dbObj.Id);
        }
    }
}