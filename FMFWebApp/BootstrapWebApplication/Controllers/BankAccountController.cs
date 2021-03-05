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
    public class BankAccountController : Controller<BankAccount>
    {
        protected override IQueryable<BankAccount> PagedListFilter(IQueryable<BankAccount> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.IsPrimary.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Bank.RegNumber.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Bank.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.AccountNumber.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        protected override void AddViewBag(BankAccount BankAccount)
        {
            ViewBag.BankId = new SelectList(db.BankAccounts.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Bank.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", BankAccount != null ? BankAccount.Id : 0);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = db.Set<Member>().Find(id);
            if (member != null)
            {
                var result = member.BankAccounts.FilterDeleted().Select(m => new MemberBankAccountViewModel()
                {
                    Id = m.Id,
                    MemberId = member.Id,
                    IsPrimary = m.IsPrimary,
                    RegNumber = m.Bank != null ? m.Bank.RegNumber : -1,
                    AccountNumber = m.AccountNumber
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        public ActionResult BankAccounts(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "BankAccounts");
        }

        [HttpGet]
        public ActionResult Create(int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);

            ViewModelBase viewModel = null;
            if (parent != null)
            {
                viewModel = new MemberBankAccountCreateOrEditViewModel()
                {

                };
            }
            ViewBag.BankId = new SelectList(db.Banks.ToList().Select(m => new SelectListItem
            {
                Text = m.RegNumber + " - " + m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text");
            return Create("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberBankAccountCreateOrEditViewModel vmObj)
        {
            return CreateRelatedObjectUsingViewModel<Member, BankAccount>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<BankAccount>(o);
                var dbObj = new BankAccount()
                {
                    AccountNumber = vmObj.AccountNumber,
                    BankId = vmObj.BankId,
                    IsPrimary = vmObj.IsPrimary
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.BankAccounts.Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBase viewModel = null;
            if (obj != null)
            {
                ViewBag.BankId = new SelectList(db.Banks.FilterDeleted().ToList().Select(m => new SelectListItem
                {
                    Text = m.RegNumber + " - " + m.Name,
                    Value = m.Id.ToString()
                }), "Value", "Text", obj != null ? obj.BankId : null);

                viewModel = new MemberBankAccountCreateOrEditViewModel()
                {
                    Id = obj.Id,
                    RegNumber = obj.RegNumber,
                    //BankId = obj.BankId,
                    AccountNumber = obj.AccountNumber,
                    //EntityId = obj.EntityId,
                    IsPrimary = obj.IsPrimary
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberBankAccountCreateOrEditViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Member, BankAccount>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var dbObj = parent.BankAccounts.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                //var bank = db.Banks.Where(x => x.RegNumber == vmObj.RegNumber).FirstOrDefault();
                //dbObj.Bank = bank != null ? bank : dbObj.Bank;
                dbObj.BankId = vmObj.BankId;
                dbObj.AccountNumber = vmObj.AccountNumber;
                dbObj.IsPrimary = vmObj.IsPrimary;
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.BankAccounts.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        //todo: activate ValidateAntiForgeryToken again, but currently this is a workaround, since it's not always attached. The bug is related / caused by modalCRUD in kthusid.js
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(MemberBankAccountViewModel dbObj)
        {
            //var parent = Find(dbObj.MemberId);
            //var obj = dbObj != null ? parent.BankAccounts.Where(x => x.Id == dbObj.Id).FirstOrDefault() : null;
            return DeleteObject<BankAccount, Member>(dbObj.Id, dbObj.MemberId);// DeleteRelatedObject(parent.Id, obj);
        }
    }
}