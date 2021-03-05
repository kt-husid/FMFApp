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
    public class LogController : Controller<ChangeEventItem>
    {
        protected override IQueryable<ChangeEventItem> PagedListFilter(IQueryable<ChangeEventItem> list, string filterName = null)
        {
            return null;
            //return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
            //                    || s.Description.ToString().ToUpper().Contains(filterName.ToUpper()) 
            //                    || s.Code.ToString().ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(ChangeEventItem ChangeEventItem)
        {
            //ViewBag.ChangeEventItemId = new SelectList(db.ChangeEventItems.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Bank.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", ChangeEventItem != null ? ChangeEventItem.Id : 0);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = db.ChangeEventItems.Where(x => x.EntityId == id).Select(m => new LogViewModel()
            {
                Id = m.Id,
                Date = m.Date,
                Type = m.Type,
                UserIdCode = m.UserIdCode,
                //ChangeEvent = m.ChangeEvent,
                TableName = m.TableName,
                ColumnName = m.ColumnName,
                ChangedFrom = m.ChangedFrom,
                ChangedTo = m.ChangedTo,
                ChangeEventId = m.ChangeEvent != null ? m.ChangeEvent.Id : -1
            });
            return result.OrderBy(x => x.Date).ToDataSourceResult(request);
        }

        public ActionResult Logs(int? id)
        {
            return GetRelatedDataForParent<Entity>(id, "Logs");
        }
    }
}