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
    public class EmailAddressController : Controller<EmailAddress>
    {
        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        protected override IQueryable<EmailAddress> PagedListFilter(IQueryable<EmailAddress> list, string filterName = null)
        {
            return list.Where(s => s.Address.ToUpper().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(EmailAddress EmailAddress)
        {
            //ViewBag.CountryId = new SelectList(db.Countries.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", EmailAddress == null ? 1 : EmailAddress.CountryId);
        }

        protected override void CreateGetHook()
        {

        }

        protected override void EditGetHook()
        {

        }

        protected override void DeleteGetHook()
        {

        }

        public ActionResult EmailAddresses(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "EmailAddresses");
        }

        private void AddViewBagCreateEdit(EmailAddress obj)
        {
            //ViewBag.PostalId = new SelectList(db.Postals.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Code + " - " + m.CityName,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.PostalId : null);
            //ViewBag.CountryId = new SelectList(db.Countries.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Code + " - " + m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? new Nullable<int>(obj.CountryId) : null);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            var parent = db.Set<Member>().Find(id);
            if (parent != null)
            {
                var result = parent.EmailAddresses.FilterDeleted().Select(m => new EmailAddressViewModel()
                {
                    Id = m.Id,
                    ParentId = parent.Id,
                    Address = m.Address,
                    IsVerified = m.IsVerified,
                    IsActive = m.IsActive,
                    IsPrimary = m.IsPrimary
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpGet]
        public ActionResult Create(int? parentId)
        {
            var parent = db.Set<Member>().Find(parentId);

            IViewModelBase viewModel = null;
            if (parent != null)
            {
                AddViewBagCreateEdit(null);
                viewModel = new EmailAddressCreateOrEditViewModel()
                {
                    IsActive = true
                };
            }
            return Create<Member>("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmailAddressCreateOrEditViewModel vmObj)
        {
            Member member = null;
            var result = CreateRelatedObjectUsingViewModel<Member, EmailAddress>(vmObj.ParentId, vmObj, (parent) =>
            {
                member = parent;
                //Convert the ViewModel to DB Object (Model)
                var dbObj = new EmailAddress()
                {
                    Id = vmObj.Id,
                    Address = vmObj.Address,
                    IsVerified = true, // TODO: should be verified by sending a link
                    IsActive = vmObj.IsActive,
                    IsPrimary = vmObj.IsPrimary
                };


                var primaryEmailAddresses = parent.EmailAddresses.FilterDeleted().Where(x => x.IsPrimary);
                if (primaryEmailAddresses.Count() == 0)
                {
                    // if there's no primary EmailAddress, then set the new one as primary
                    dbObj.IsPrimary = true;
                }
                else
                {
                    // if there's a primary EmailAddress and the new one is set to primary, then
                    // set the new one to be primary and old to be not
                    // AND if the new EmailAddress is set to primary
                    if (vmObj.IsPrimary)
                    {
                        foreach (var primaryEmailAddress in primaryEmailAddresses)
                        {
                            primaryEmailAddress.IsPrimary = false;
                        }
                        dbObj.IsPrimary = true;
                    }
                }


                return dbObj;
            });

            if (member != null)
            {
                var emailAddress = member.PrimaryEmailAddress;
                if (emailAddress != null)
                {
                    // Update (if property exists) in Member 
                    base.Update<Member>(member);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? parentId)
        {
            var parent = db.Set<Member>().Find(parentId);
            var obj = parent != null ? parent.EmailAddresses.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBase viewModel = null;
            if (obj != null)
            {
                AddViewBagCreateEdit(obj);

                viewModel = new EmailAddressCreateOrEditViewModel()
                {
                    Id = obj.Id,
                    ParentId = obj.Id,
                    Address = obj.Address,
                    //IsVerified = obj.IsVerified,
                    IsActive = obj.IsActive,
                    IsPrimary = obj.IsPrimary
                };
            }
            return Edit<Member>("CreateOrEdit", viewModel);//, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmailAddressCreateOrEditViewModel vmObj)
        {
            Member member = null;
            var result = UpdateRelatedObjectUsingViewModel<Member, EmailAddress>(vmObj.ParentId, vmObj, (parent) =>
            {
                member = parent;

                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.EmailAddresses.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                                
                dbObj.Address = vmObj.Address;
                //dbObj.IsVerified = true;
                dbObj.IsActive = vmObj.IsActive;
                dbObj.IsPrimary = vmObj.IsPrimary;

                var primaryEmailAddresses = parent.EmailAddresses.FilterDeleted().Where(x => x.IsPrimary);
                if (primaryEmailAddresses.Count() == 0)
                {
                    // if there's no primary EmailAddress, then set the new one as primary
                    dbObj.IsPrimary = true;
                }
                else
                {
                    // if there's a primary EmailAddress and the new one is set to primary, then
                    // set the new one to be primary and old to be not
                    // AND the primaryEmailAddress isn't the same object as being edited
                    foreach (var primaryEmailAddress in primaryEmailAddresses)
                    {
                        primaryEmailAddress.IsPrimary = false;
                    }
                    dbObj.IsPrimary = true;
                }
                return dbObj;
            });

            if (member != null)
            {
                var emailAddress = member.PrimaryEmailAddress;
                if (emailAddress != null)
                {
                    // Update (if property exists) in Member 
                    base.Update<Member>(member);
                }
            }

            return result;
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? parentId)
        {
            var parent = db.Set<Member>().Find(parentId);
            var obj = parent != null ? parent.EmailAddresses.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EmailAddressViewModel vmObj)
        {
            //return DeleteObject<EmailAddress, Member>(vmObj.Id, vmObj.ParentId);
            var parent = db.Set<Member>().Find(vmObj.ParentId);
            if (EmailAddressHandler.Instance.CheckIfCanDelete(parent, vmObj))
            {
                return DeleteObject<EmailAddress, Member>(vmObj.Id, vmObj.ParentId);
            }
            return Json(HttpStatusCode.BadRequest);
        }
    }
}