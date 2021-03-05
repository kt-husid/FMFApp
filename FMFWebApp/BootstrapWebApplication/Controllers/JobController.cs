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
using BootstrapWebApplication.BLL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class JobController : Controller<Job>
    {
        public JsonResult GetAll()
        {
            var result = db.Jobs.FilterDeleted().Select(m => new JobViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Description = m.Description,
                Organization = m.Organization,
                PART = m.PART,
                TIL_DG = m.TIL_DG,
                TIL_TUR = m.TIL_TUR,
                HasInsurance = m.HasInsurance
                //ChangeEvent = m.ChangeEvent
            });
            return Json(new DataSourceResult() { Data = result, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Jobs as IQueryable<Job>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Contains(filter.ToLower()) || s.Description.ToLower().Contains(filter.ToLower()));
            }
            //var result = listFiltered.Select(m => ToViewModel(m));
            var result = listFiltered.Select(job => new JobViewModel()
            {
                Id = job.Id,
                Code = job.Code,
                Description = job.Description,
                Organization = job.Organization,
                PART = job.PART,
                TIL_DG = job.TIL_DG,
                TIL_TUR = job.TIL_TUR,
                HasInsurance = job.HasInsurance,
                ChangeEvent = job.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<Job> PagedListFilter(IQueryable<Job> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                || s.Description.ToString().ToUpper().Contains(filterName.ToUpper())
                                || s.Code.ToString().ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(Job Job)
        {
            //ViewBag.JobId = new SelectList(db.Jobs.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Bank.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", Job != null ? Job.Id : 0);
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

        private DataSourceResult GetFiltered(string filter, DataSourceRequest request)
        {
            //var listFiltered = db.Set<FishSpecies>() as IQueryable<FishSpecies>;
            //if (filter.Length > 1)
            //{
            //    //DateTime dt = DateTime.Now;
            //    //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            //    listFiltered = listFiltered.Where(s => s.Code.ToLower().Equals(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower())
            //    );
            //}
            //return GetFiltered(request, listFiltered);
            var filtered = new JobHandler().GetFiltered(db.Jobs, filter, 2);
            return GetFiltered(request, filtered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IQueryable<Job> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(job => new JobViewModel()
            {
                Id = job.Id,
                Code = job.Code,
                Description = job.Description,
                Organization = job.Organization,
                PART = job.PART,
                TIL_DG = job.TIL_DG,
                TIL_TUR = job.TIL_TUR,
                HasInsurance = job.HasInsurance,
                ChangeEvent = job.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = db.Jobs.FilterDeleted().Select(job => new JobViewModel()
            {
                Id = job.Id,
                Code = job.Code,
                Description = job.Description,
                Organization = job.Organization,
                PART = job.PART,
                TIL_DG = job.TIL_DG,
                TIL_TUR = job.TIL_TUR,
                HasInsurance = job.HasInsurance,
                ChangeEvent = job.ChangeEvent
            });
            return result.ToDataSourceResult(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.BankId = new SelectList(db.Banks.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.RegNumber + " - " + m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text");
            AddViewBag(null);
            //return Create<Job>("Create");
            var viewModel = new JobCreateEditViewModel()
            {

            };
            return Create<Job>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCreateEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Job()
                {
                    Code = o.Code,
                    Description = o.Description,
                    Description2 = o.Description,
                    STUTT = o.Description,
                    Organization = o.Organization,
                    TIL_DG = o.TIL_DG,
                    TIL_TUR = o.TIL_TUR,
                    HasInsurance = o.HasInsurance
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.Jobs.Where(x => x.Id == id).FirstOrDefault();

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                //ViewBag.BankId = new SelectList(db.Banks.ToList().Select(m => new SelectListItem
                //{
                //    Text = m.RegNumber + " - " + m.Name,
                //    Value = m.Id.ToString()
                //}), "Value", "Text", obj != null ? obj.BankId : null);

                viewModel = new JobCreateEditViewModel()
                {
                    Code = obj.Code,
                    Description = obj.Description,
                    //Description2 = obj.Description2,
                    Organization = obj.Organization,
                    TIL_DG = obj.TIL_DG,
                    TIL_TUR = obj.TIL_TUR,
                    HasInsurance = obj.HasInsurance
                };
            }
            return Edit<Job>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobCreateEditViewModel vmObj)
        {
            return UpdateUsingViewModel(vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = db.Jobs.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Code = vmObj.Code;
                dbObj.Description = vmObj.Description;
                dbObj.Description2 = vmObj.Description;
                dbObj.STUTT = vmObj.Description;
                dbObj.Organization = vmObj.Organization;
                dbObj.TIL_DG = vmObj.TIL_DG;
                dbObj.TIL_TUR = vmObj.TIL_TUR;
                dbObj.HasInsurance = vmObj.HasInsurance;
                return dbObj;
            });
        }

        [HttpGet]
        public JsonResult GetFromCode(string code)
        {
            var obj = db.Set<Job>().FilterDeleted().Where(x => x.Code == code).FirstOrDefault();
            if (obj != null)
            {
                return Json(new JobViewModel()
                {
                    Id = obj.Id,
                    Code = obj.Code,
                    Description = obj.Description,
                    TIL_DG = obj.TIL_DG,
                    TIL_TUR = obj.TIL_TUR,
                    Organization = obj.Organization,
                    PART = obj.PART,
                    HasInsurance = obj.HasInsurance
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(HttpStatusCode.BadRequest, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Job>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(JobViewModel dbObj)
        {
            return DeleteObject<Job, Job>(dbObj.Id);
        }

        //[HttpGet]
        //public ActionResult Delete(int? id, int? memberId)
        //{
        //    var obj = db.Jobs.Where(x => x.Id == id).FirstOrDefault();
        //    return Delete("Delete", obj);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(JobCreateEditViewModel dbObj)
        //{
        //    return DeleteObject(dbObj.Id);
        //}
    }
}