using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.BLL;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class ShippingCompanyPhoneController : Controller<Phone>
    {
        protected override IQueryable<Phone> PagedListFilter(IQueryable<Phone> list, string filterName = null)
        {
            return list.Where(s => s.Number.ToString().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(Phone Phone)
        {
        }

        public ActionResult Phones(int? id)
        {
            return GetRelatedDataForParent<ShippingCompany>(id, "Phones");
        }

        private void AddViewBagCreateEdit(Phone obj)
        {
            //ViewBag.PostalId = new SelectList(db.Postals.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.PostalCode + " - " + m.CityName,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.PostalId : null);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            var parent = db.Set<ShippingCompany>().Find(id);
            if (parent != null)
            {
                var result = PhoneHandler.Instance.GetPhones(parent);
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpGet]
        public ActionResult Create(int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);

            IViewModelBase viewModel = null;
            if (parent != null)
            {
                AddViewBagCreateEdit(null);
                viewModel = new PhoneCreateOrEditViewModel()
                {

                };
            }
            return Create<ShippingCompany>("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhoneCreateOrEditViewModel vmObj)
        {
            ShippingCompany shippingCompany = null;
            var result = CreateRelatedObjectUsingViewModel<ShippingCompany, Phone>(vmObj.ParentId, vmObj, (parent) =>
            {
                shippingCompany = parent;
                return PhoneHandler.Instance.CreatePhone(parent, vmObj);
            });

            if (shippingCompany != null)
            {
                var phone = shippingCompany.PrimaryPhone;
                if (phone != null)
                {
                    shippingCompany.PhoneCountryCode = new Nullable<int>(phone.CountryCode);
                    shippingCompany.PhoneNumber = phone.Number;
                    base.Update<ShippingCompany>(shippingCompany);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = parent != null ? parent.Phones.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBase viewModel = null;
            if (obj != null)
            {
                AddViewBagCreateEdit(obj);

                viewModel = new PhoneCreateOrEditViewModel()
                {
                    Id = obj.Id,
                    ParentId = parentId,
                    IsPrimary = obj.IsPrimary,
                    PhoneNumber = obj.RawNumber
                };
            }
            return Edit<ShippingCompany>("CreateOrEdit", viewModel);//, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhoneCreateOrEditViewModel vmObj)
        {
            ShippingCompany shippingCompany = null;
            var result = UpdateRelatedObjectUsingViewModel<ShippingCompany, Phone>(vmObj.ParentId, vmObj, (parent) =>
            {
                shippingCompany = parent;
                return PhoneHandler.Instance.UpdatePhone(parent, vmObj);
            });

            if (shippingCompany != null)
            {
                var phone = shippingCompany.PrimaryPhone;
                if (phone != null)
                {
                    shippingCompany.PhoneCountryCode = new Nullable<int>(phone.CountryCode);
                    shippingCompany.PhoneNumber = phone.Number;
                    base.Update<ShippingCompany>(shippingCompany);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = parent != null ? parent.Phones.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PhoneViewModel vmObj)
        {
            var parent = db.Set<ShippingCompany>().Find(vmObj.ParentId);
            if (PhoneHandler.Instance.CheckIfCanDelete(parent, vmObj))
            {
                return DeleteObject<Phone, ShippingCompany>(vmObj.Id, vmObj.ParentId);
            }
            return Json(HttpStatusCode.BadRequest);
        }
    }
}