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
    public class MemberPaymentController : Controller<MemberPayment>
    {
        protected override IQueryable<MemberPayment> PagedListFilter(IQueryable<MemberPayment> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Price.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<MemberPayment>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(MemberPayment obj)
        {
            //ViewBag.ChangeEventId = new SelectList(db.MemberPayments.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "JobTitle");
        }

        public ActionResult MemberPayments(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "MemberPayments");
        }

        private void AddViewBag(Member parent, MemberPayment obj)
        {
            ViewBag.MemberBankAccountId = new SelectList(parent.BankAccounts.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Bank.RegNumber + " - " + m.AccountNumber,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.BankAccountId : null);

            ViewBag.BankId = new SelectList(db.Banks.ToList().Select(m => new SelectListItem
            {
                Text = m.RegNumber + " - " + m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", (obj != null && obj.BankAccount != null) ? obj.BankAccount.BankId : null);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult MemberPayment_Create([DataSourceRequest] DataSourceRequest request, Member obj)
        //{
        //    if (obj != null && ModelState.IsValid)
        //    {
        //        //todo: implement Create method in BLL
        //        //handler.Create(obj);                                 
        //    }

        //    return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        //}

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = db.Set<Member>().Find(id);
            if (member != null)
            {
                var user = GetUser();
                var laborInsurancePercentage = user.AppSettings.LaborInsurancePercentage.HasValue ? user.AppSettings.LaborInsurancePercentage.Value : 0m;

                var result = member.Payments.FilterDeleted().OrderByDescending(x => x.PaidOn).Select(m => new MemberPaymentViewModel()
                {
                    Id = m.Id,
                    Year = m.Year,
                    BankRegNumber = m.BankAccount != null ? m.BankAccount.RegNumber : -1,
                    BankAccountNumber = m.BankAccount != null ? m.BankAccount.AccountNumber : -1,
                    HolidayPay = m.HolidayPay,
                    MembershipPayment = m.MembershipPayment,
                    Code = m.Code,
                    Price = m.Price,
                    PaidOn = m.PaidOn,
                    PaidById = m.PaidById,
                    ChangeEvent = m.ChangeEvent,
                    ChangeEventId = m.ChangeEventId,
                    LaborInsurance = m.LaborInsurance * laborInsurancePercentage,
                    MaternityPayment = m.MaternityPayment,
                    Tax = m.Tax
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult MemberPayment_Update([DataSourceRequest] DataSourceRequest request, MemberPaymentViewModel vmObj)
        //{
        //    return base.UpdateRelatedObjectUsingViewModel(vmObj.MemberId, vmObj, (parent) =>
        //    {
        //        //Convert the ViewModel to DB Object (Model)
        //        //var dbObj = Mapper.Map<MemberPayment>(vmObj);
        //        var bank = db.Banks.Where(x => x.RegNumber == vmObj.BankRegNumber).FirstOrDefault();

        //        var dbObj = new MemberPayment()
        //        {
        //            Id = vmObj.Id,
        //            ChangeEventId = vmObj.ChangeEventId,
        //            MemberId = vmObj.MemberId

        //        };
        //        return dbObj;
        //        //return Json(new[] { dbObj }.ToDataSourceResult(request, ModelState));
        //    });
        //}


        [HttpGet]
        public ActionResult Create(int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);

            if (parent != null)
            {

                AddViewBag(parent, null);
            }
            return Create("CreateOrEdit", new MemberPaymentCreateViewModel() { PaidById = GetUserIdCode() }, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberPaymentCreateViewModel vmObj)
        {
            return CreateRelatedObjectUsingViewModel<Member, MemberPayment>(vmObj.MemberId, vmObj, (parent) =>
            {
                return MemberPaymentHandler.Create(parent, vmObj, this);
            });
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.Payments.Where(x => x.Id == id).FirstOrDefault() : null;

            if (parent != null)
            {
                AddViewBag(parent, obj);
            }

            MemberPaymentCreateViewModel viewModel = null;
            if (obj != null)
            {
                viewModel = new MemberPaymentCreateViewModel()
                {
                    Year = obj.Year,
                    HolidayPay = obj.HolidayPay,
                    MembershipPayment = obj.MembershipPayment,
                    Code = obj.Code,
                    Price = obj.Price,
                    MemberId = obj.MemberId,
                    PaidOn = obj.PaidOn,
                    PaidById = obj.PaidById,
                    ChangeEventId = obj.ChangeEventId,
                    BankId = obj.BankAccount != null ? obj.BankAccount.BankId : null,
                    BankAccountNumber = obj.BankAccount != null ? obj.BankAccount.AccountNumber : -1
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberPaymentCreateViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Member, MemberPayment>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<MemberFinancialAidCreateViewModel>(o);
                var dbObj = parent.Payments.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.Year = vmObj.Year;
                dbObj.HolidayPay = vmObj.HolidayPay;
                dbObj.MembershipPayment = vmObj.MembershipPayment;
                dbObj.Code = vmObj.Code;
                dbObj.Price = vmObj.Price;
                dbObj.PaidOn = vmObj.PaidOn;
                //dbObj.PaidById = vmObj.PaidById;
                //dbObj.MemberId = vmObj.MemberId;
                //dbObj.ChangeEventId = vmObj.ChangeEventId;

                var bankAccount = db.BankAccounts.Where(x => x.Bank.Id == vmObj.BankId && x.AccountNumber == vmObj.BankAccountNumber).FirstOrDefault();
                if (bankAccount == null)
                {
                    bankAccount = new BankAccount()
                    {
                        AccountNumber = vmObj.BankAccountNumber,
                        BankId = vmObj.BankId,
                        Entity = parent.Person.Entity,
                        IsPrimary = true
                    };
                }
                dbObj.BankAccount = bankAccount;

                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.Payments.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MemberPayment dbObj)
        {
            //var parent = Find(dbObj.MemberId);
            //var obj = dbObj != null ? parent.Payments.Where(x => x.Id == dbObj.Id).FirstOrDefault() : null;
            return DeleteObject<MemberPayment, Member>(dbObj.Id, dbObj.MemberId);// DeleteRelatedObject(parent.Id, obj);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult MemberFinancialAid_Update([DataSourceRequest] DataSourceRequest request, MemberFinancialAidViewModel vmObj)
        //{
        //    return base.UpdateRelatedObjectUsingViewModel(vmObj.MemberId, vmObj, (vm) =>
        //    {
        //        //Convert the ViewModel to DB Object (Model)
        //        //var dbObj = Mapper.Map<BankAccount>(vm);
        //        //var bankAccount = db.BankAccounts.Where(x => x.Id == vmObj.BankAccountId).FirstOrDefault();
        //        var dbObj = new MemberFinancialAid()
        //        {
        //            Id = vmObj.Id,
        //            From = vmObj.From,
        //            To = vmObj.To,
        //            //Days = null,
        //            SocialServicePayment = vmObj.SocialServicePayment,
        //            PaymentPerDay = vmObj.PaymentPerDay,
        //            PrintedOn = null,
        //            PrintedById = null,
        //            PrintedAmount = 0,
        //            MemberId = vmObj.MemberId,
        //            BankAccountId = vmObj.BankAccountId,
        //            ChangeEventId = vmObj.ChangeEventId
        //        };
        //        return dbObj;
        //        //return Json(new[] { dbObj }.ToDataSourceResult(request, ModelState));
        //    });
        //}

    }
}