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
using BootstrapWebApplication.Controllers;
using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Interfaces;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class ShippingCompanyCommentController : Controller<Comment>
    {
        protected override IQueryable<Comment> PagedListFilter(IQueryable<Comment> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Text.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<Comment>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(Comment Comment)
        {
            //ViewBag.ChangeEventId = new SelectList(db.Comments.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanys, "Id", "JobTitle");
        }

        public ActionResult Comments(int? id)
        {
            return GetRelatedDataForParent<ShippingCompany>(id, "Comments");
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = (parent != null && parent.Entity != null) ? parent.Entity.Comments.Where(x => x.Id == id).FirstOrDefault() : null;

            //AddViewBag(parent);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new CommentViewModel()
                {
                    Id = obj.Id,
                    ParentId = parent.Id,
                    EntityId = parent.EntityId,
                    Text = obj.Text,
                    ChangeEvent = obj.ChangeEvent,
                    ChangeEventId = obj.ChangeEventId
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<ShippingCompany, Comment>(vmObj.ParentId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.Entity.Comments.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Text = vmObj.Text;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = (parent != null && parent.Entity != null) ? parent.Entity.Comments.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        //todo: activate ValidateAntiForgeryToken again, but currently this is a workaround, since it's not always attached. The bug is related / caused by modalCRUD in kthusid.js
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(CommentViewModel dbObj)
        {
            return DeleteObject<Comment, ShippingCompany>(dbObj.Id, dbObj.ParentId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Create<ShippingCompany>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel vmObj)
        {
            return CreateRelatedObjectUsingViewModel<ShippingCompany, Comment>(vmObj.ParentId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Comment()
                {
                    Text = vmObj.Text
                };
                return dbObj;
            });
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var ShippingCompany = db.Set<ShippingCompany>().Find(id);
            if (ShippingCompany != null)
            {
                var result = ShippingCompany.Entity.Comments.FilterDeleted().Select(m => new CommentViewModel()
                {
                    Id = m.Id,
                    ParentId = ShippingCompany.Id,
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