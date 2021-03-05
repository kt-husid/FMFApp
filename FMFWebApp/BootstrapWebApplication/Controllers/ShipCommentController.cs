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
using BootstrapWebApplication.Interfaces;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AutoMapper;

namespace BootstrapWebApplication.Controllers
{
    public class ShipCommentController : Controller<ShipComment>
    {
        protected override IQueryable<ShipComment> PagedListFilter(IQueryable<ShipComment> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Text.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<ShipComment>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(ShipComment ShipComment)
        {
            //ViewBag.ChangeEventId = new SelectList(db.ShipComments.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "JobTitle");
        }

        public ActionResult Comments(int? id)
        {
            return GetRelatedDataForParent<Ship>(id, "Comments");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Create<Ship>("CreateOrEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipCommentViewModel o)
        {
            return CreateRelatedObjectUsingViewModel<Ship, ShipComment>(o.ShipId, o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new ShipComment()
                {
                    Text = o.Text,
                    ShipId = o.ShipId
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? shipId)
        {
            var parent = db.Set<Ship>().Find(shipId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new ShipCommentViewModel()
                {
                    Text = obj.Text,
                    ChangeEventId = obj.ChangeEventId,
                    ShipId = obj.ShipId
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShipCommentViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Ship, ShipComment>(vmObj.ShipId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.Comments.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Text = vmObj.Text;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? shipId)
        {
            var parent = db.Set<Ship>().Find(shipId);
            var obj = parent != null ? parent.Comments.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        //todo: activate ValidateAntiForgeryToken again, but currently this is a workaround, since it's not always attached. The bug is related / caused by modalCRUD in kthusid.js
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(ShipCommentViewModel dbObj)
        {
            return DeleteObject<ShipComment, Ship>(dbObj.Id, dbObj.ShipId);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var ship = db.Set<Ship>().Find(id);
            if (ship != null)
            {
                var result = ship.Comments.FilterDeleted().Select(m => new ShipCommentViewModel()
                {
                    Id = m.Id,
                    ShipId = m.ShipId,
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