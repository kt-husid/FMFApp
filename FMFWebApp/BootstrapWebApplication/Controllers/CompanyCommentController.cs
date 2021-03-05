using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BootstrapWebApplication.Interfaces;

namespace BootstrapWebApplication.Controllers
{
    public class CompanyCommentController : Controller<CompanyComment>
    {
        protected override IQueryable<CompanyComment> PagedListFilter(IQueryable<CompanyComment> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Text.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<CompanyComment>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(CompanyComment CompanyComment)
        {
            //ViewBag.ChangeEventId = new SelectList(db.CompanyComments.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "JobTitle");
        }

        public ActionResult Comments(int? id)
        {
            return GetRelatedDataForParent<Company>(id, "Comments");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Create<Company>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyCommentViewModel o)
        {
            return CreateRelatedObjectUsingViewModel<Company, CompanyComment>(o.CompanyId, o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new CompanyComment()
                {
                    Text = o.Text,
                    CompanyId = o.CompanyId
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? companyId)
        {
            var parent = db.Set<Company>().Find(companyId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new CompanyCommentViewModel()
                {
                    Text = obj.Text,
                    //ChangeEventId = obj.ChangeEventId,
                    //CompanyId = obj.CompanyId
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyCommentViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Company, CompanyComment>(vmObj.CompanyId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.Comments.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Text = vmObj.Text;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? companyId)
        {
            var parent = db.Set<Company>().Find(companyId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        //todo: activate ValidateAntiForgeryToken again, but currently this is a workaround, since it's not always attached. The bug is related / caused by modalCRUD in kthusid.js
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(CompanyCommentViewModel dbObj)
        {
            return DeleteObject<CompanyComment, Company>(dbObj.Id, dbObj.CompanyId);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var company = db.Set<Company>().Find(id);
            if (company != null)
            {
                var result = company.Comments.FilterDeleted().Select(m => new CompanyCommentViewModel()
                {
                    Id = m.Id,
                    CompanyId = m.CompanyId,
                    Text = m.Text,
                    ChangeEventId = m.ChangeEventId,
                    ChangeEvent = m.ChangeEvent
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }
    }
}