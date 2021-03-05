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
    public class MemberCommentController : Controller<MemberComment>
    {
        protected override IQueryable<MemberComment> PagedListFilter(IQueryable<MemberComment> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Text.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<MemberComment>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(MemberComment MemberComment)
        {
            //ViewBag.ChangeEventId = new SelectList(db.MemberComments.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "JobTitle");
        }

        public ActionResult Comments(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "Comments");
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;

            //AddViewBag(parent);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new MemberCommentViewModel()
                {
                    Id = obj.Id,
                    MemberId = obj.MemberId,
                    Text = obj.Text,
                    ChangeEvent = obj.ChangeEvent
                    //ChangeEventId = obj.ChangeEventId
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberCommentViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Member, MemberComment>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.Comments.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Text = vmObj.Text;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        //todo: activate ValidateAntiForgeryToken again, but currently this is a workaround, since it's not always attached. The bug is related / caused by modalCRUD in kthusid.js
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(MemberCommentViewModel dbObj)
        {
            return DeleteObject<MemberComment, Member>(dbObj.Id, dbObj.MemberId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Create<Member>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCommentViewModel vmObj)
        {
            return CreateRelatedObjectUsingViewModel<Member, MemberComment>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new MemberComment()
                {
                    Text = vmObj.Text,
                    MemberId = vmObj.MemberId
                };
                return dbObj;
            });
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = db.Set<Member>().Find(id);
            if (member != null)
            {
                var result = member.Comments.FilterDeleted().Select(m => new MemberCommentViewModel()
                {
                    Id = m.Id,
                    MemberId = m.MemberId,
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