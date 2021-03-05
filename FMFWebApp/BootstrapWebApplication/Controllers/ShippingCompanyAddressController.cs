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
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers
{
    public class ShippingCompanyAddressController : Controller<Address>
    {
        /// <summary>
        /// todo: Filter by Postal CityName & PostalCode/ZipCode
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        protected override IQueryable<Address> PagedListFilter(IQueryable<Address> list, string filterName = null)
        {
            return list.Where(s => s.AddressLine1.ToUpper().Contains(filterName.ToUpper())
                                    || s.AddressLine2.ToUpper().Contains(filterName.ToUpper())
                                    || s.Country.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(Address obj)
        {
            ViewBag.PostalId = new SelectList(db.Postals.ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " - " + m.CityName,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.PostalId : null);
            ViewBag.CountryId = new SelectList(db.Countries.ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " - " + m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? new Nullable<int>(obj.CountryId) : null);
        }

        public ActionResult Addresses(int? id)
        {
            return GetRelatedDataForParent<ShippingCompany>(id, "Addresses");
        }

        private void AddViewBagCreateEdit(Address obj)
        {
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            var parent = db.Set<ShippingCompany>().Find(id);
            if (parent != null)
            {
                var result = parent.Addresses.FilterDeleted().Select(m => new AddressViewModel()
                {
                    Id = m.Id,
                    ParentId = parent.Id,
                    CareOf = m.CareOf,
                    AddressLine1 = m.AddressLine1,
                    AddressLine2 = m.AddressLine2,
                    IsActive = m.IsActive,
                    IsPrimary = m.IsPrimary,
                    PostalCode = m.Postal.Code,
                    City = m.Postal.CityName,
                    StateProvinceRegion = m.StateProvinceRegion,
                    CountryCode = m.Country.Code,
                    CountryName = m.Country.Name
                });
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
                AddViewBag(null);
                viewModel = new AddressCreateOrEditViewModel()
                {
                    IsActive = true
                };
            }
            return Create<ShippingCompany>("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressCreateOrEditViewModel vmObj)
        {
            ShippingCompany shippingCompany = null;
            var result = CreateRelatedObjectUsingViewModel<ShippingCompany, Address>(vmObj.ParentId, vmObj, (parent) =>
            {
                shippingCompany = parent;
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new Address()
                {
                    Id = vmObj.Id,
                    //MemberId = parent.Id,
                    CareOf = vmObj.CareOf,
                    AddressLine1 = vmObj.AddressLine1,
                    AddressLine2 = vmObj.AddressLine2,
                    IsActive = vmObj.IsActive,
                    IsPrimary = vmObj.IsPrimary,
                    PostalId = vmObj.PostalId,
                    StateProvinceRegion = vmObj.StateProvinceRegion,
                    CountryId = vmObj.CountryId
                };


                var primaryAddresses = parent.Addresses.FilterDeleted().Where(x => x.IsPrimary);
                if (primaryAddresses.Count() == 0)
                {
                    // if there's no primary address, then set the new one as primary
                    dbObj.IsPrimary = true;
                }
                else
                {
                    // if there's a primary address and the new one is set to primary, then
                    // set the new one to be primary and old to be not
                    // AND if the new address is set to primary
                    if (vmObj.IsPrimary)
                    {
                        foreach (var primaryAddress in primaryAddresses)
                        {
                            primaryAddress.IsPrimary = false;
                        }
                        dbObj.IsPrimary = true;
                    }
                }


                return dbObj;
            });

            if (shippingCompany != null)
            {
                var address = shippingCompany.PrimaryAddress;
                if (address != null)
                {
                    var country = address.Country;
                    var postal = address.Postal;
                    if (country == null)
                    {
                        country = db.Countries.Where(x => x.Id == address.CountryId).FirstOrDefault();
                    }
                    if (postal == null)
                    {
                        postal = db.Postals.Where(x => x.Id == address.PostalId).FirstOrDefault();
                    }
                    shippingCompany.CountryCode = country != null ? country.Code : "";
                    shippingCompany.CountryName = country != null ? country.Name : "";
                    shippingCompany.AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "";
                    shippingCompany.PostalCode = postal != null ? new Nullable<int>(postal.Code) : null;
                    shippingCompany.CityName = postal != null ? postal.CityName : "";

                    base.Update<ShippingCompany>(shippingCompany);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = parent != null ? parent.Addresses.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBase viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);

                viewModel = new AddressCreateOrEditViewModel()
                {
                    Id = obj.Id,
                    ParentId = obj.Id,
                    CareOf = obj.CareOf,
                    AddressLine1 = obj.AddressLine1,
                    AddressLine2 = obj.AddressLine2,
                    IsActive = obj.IsActive,
                    IsPrimary = obj.IsPrimary,
                    PostalId = obj.PostalId,
                    Postal = obj.Postal,
                    PostalCode = obj.Postal.Code,
                    StateProvinceRegion = obj.StateProvinceRegion,
                    CountryId = obj.CountryId,
                    CountryCode = obj.Country.Code,
                    Country = obj.Country
                };
            }
            return Edit<ShippingCompany>("CreateOrEdit", viewModel);//, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressCreateOrEditViewModel vmObj)
        {
            ShippingCompany shippingCompany = null;
            var result = UpdateRelatedObjectUsingViewModel<ShippingCompany, Address>(vmObj.ParentId, vmObj, (parent) =>
            {
                shippingCompany = parent;

                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.Addresses.Where(x => x.Id == vmObj.Id).FirstOrDefault();


                dbObj.CareOf = vmObj.CareOf;
                dbObj.AddressLine1 = vmObj.AddressLine1;
                dbObj.AddressLine2 = vmObj.AddressLine2;
                dbObj.PostalId = vmObj.PostalId;
                dbObj.StateProvinceRegion = vmObj.StateProvinceRegion;
                dbObj.CountryId = vmObj.CountryId;
                dbObj.IsActive = vmObj.IsActive;
                dbObj.IsPrimary = vmObj.IsPrimary;

                var primaryAddresses = parent.Addresses.FilterDeleted().Where(x => x.IsPrimary);
                if (primaryAddresses.Count() == 0)
                {
                    // if there's no primary address, then set the new one as primary
                    dbObj.IsPrimary = true;
                }
                else
                {
                    // if there's a primary address and the new one is set to primary, then
                    // set the new one to be primary and old to be not
                    // AND the primaryAddress isn't the same object as being edited
                    foreach (var primaryAddress in primaryAddresses)
                    {
                        primaryAddress.IsPrimary = false;
                    }
                    dbObj.IsPrimary = true;
                }
                return dbObj;
            });

            if (shippingCompany != null)
            {
                var address = shippingCompany.PrimaryAddress;
                if (address != null)
                {
                    var country = address.Country;
                    var postal = address.Postal;
                    shippingCompany.CountryCode = country != null ? country.Code : "";
                    shippingCompany.CountryName = country != null ? country.Name : "";
                    shippingCompany.AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "";
                    shippingCompany.PostalCode = postal != null ? new Nullable<int>(postal.Code) : null;
                    shippingCompany.CityName = postal != null ? postal.CityName : "";

                    base.Update<ShippingCompany>(shippingCompany);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? parentId)
        {
            var parent = db.Set<ShippingCompany>().Find(parentId);
            var obj = parent != null ? parent.Addresses.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AddressViewModel vmObj)
        {
            //return DeleteObject<Address, ShippingCompany>(vmObj.Id, v.ParentId);
            var parent = db.Set<ShippingCompany>().Find(vmObj.ParentId);
            if (AddressHandler.Instance.CheckIfCanDelete(parent, vmObj))
            {
                return DeleteObject<Address, ShippingCompany>(vmObj.Id, vmObj.ParentId);
            }
            return Json(HttpStatusCode.BadRequest);
        }
    }
}