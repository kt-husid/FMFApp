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
using System.Collections;

namespace BootstrapWebApplication.Controllers
{
    public class MemberFinancialAidController : Controller<MemberFinancialAid>
    {
        protected override IQueryable<MemberFinancialAid> PagedListFilter(IQueryable<MemberFinancialAid> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.PaymentPerDay.ToString().ToUpper().Contains(filterName.ToUpper()));
            try
            {
                filteredList = filteredList.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<MemberFinancialAid>();
            }
            catch (Exception ex) { }
            return filteredList;
        }

        protected override void AddViewBag(MemberFinancialAid obj)
        {
            //ViewBag.ChangeEventId = new SelectList(db.MemberFinancialAids.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", person != null ? person.TitleId : null);
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "JobTitle");


        }

        public ActionResult MemberFinancialAidReport(bool isPartialView = false)
        {
            var id = -1;
            var idString = Request.QueryString["MemberFinancialAidId"] as string;
            int.TryParse(idString, out id);
            var financialAid = db.FinancialAids.Where(x => !x.ChangeEvent.DeletedOn.HasValue && x.Id == id).FirstOrDefault();
            var user = GetUser();
            if (financialAid != null && user != null)
            {
                financialAid.PrintedById = user.UserIdCode;
                financialAid.PrintedOn = DateTime.Now;
                Update(financialAid);
            }

            if (isPartialView)
                return PartialView();
            return View();
        }


        public ActionResult MemberFinancialAidReport2()
        {
            return View();
        }

        public ActionResult MemberFinancialAids(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "MemberFinancialAids");
        }

        private void AddViewBag(Member parent, MemberFinancialAid obj)
        {
            ViewBag.MemberBankAccountId = new SelectList(parent.BankAccounts.ToList().Select(m => new SelectListItem
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

        [HttpGet]
        public ActionResult Create(int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            if (parent != null)
            {
                AddViewBag(parent, null);
            }
            var user = GetUser();
            return Create("CreateOrEdit", new MemberFinancialAidCreateViewModel()
            {
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                PaymentPerDay = user != null && user.AppSettings != null ? user.AppSettings.MemberFinancialAidPaymentPerDay : null
            }, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberFinancialAidCreateViewModel vmObj)
        {
            return CreateRelatedObjectUsingViewModel<Member, MemberFinancialAid>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<MemberFinancialAidCreateViewModel>(o);
                //var handler = new MemberFinancialAidHandler(o);
                var dbObj = new MemberFinancialAid()
                {
                    From = vmObj.From,
                    To = vmObj.To,
                    Days = vmObj.Days,
                    SocialServicePayment = vmObj.SocialServicePayment,
                    PaymentPerDay = vmObj.PaymentPerDay,
                    PrintedOn = null,
                    PrintedById = null,
                    PrintedAmount = 0,
                    MemberId = vmObj.MemberId
                };
                dbObj.OurPayment = null;//vmObj.Days * vmObj.PaymentPerDay - vmObj.SocialServicePayment;

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

                // Create a changeEvent object
                dbObj.ChangeEvent = CreateChangeEvent();
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? memberId)
        {
            var parent = db.Set<Member>().Find(memberId);
            var obj = parent != null ? parent.FinancialAids.Where(x => x.Id == id).FirstOrDefault() : null;

            if (parent != null)
            {
                AddViewBag(parent, obj);
            }

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new MemberFinancialAidCreateViewModel()
                {
                    From = obj.From,
                    To = obj.To,
                    Days = obj.DaysCalculated,// obj.Days,
                    SocialServicePayment = obj.SocialServicePayment,
                    PaymentPerDay = obj.PaymentPerDay,
                    PrintedOn = obj.PrintedOn,
                    MemberId = obj.MemberId,
                    ChangeEventId = obj.ChangeEventId,
                    BankId = obj.BankAccount != null ? obj.BankAccount.BankId : null,
                    BankAccountNumber = obj.BankAccount != null ? obj.BankAccount.AccountNumber : -1
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberFinancialAidCreateViewModel vmObj)
        {
            return UpdateRelatedObjectUsingViewModel<Member, MemberFinancialAid>(vmObj.MemberId, vmObj, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<MemberFinancialAidCreateViewModel>(o);
                var dbObj = parent.FinancialAids.Where(x => x.Id == vmObj.Id).FirstOrDefault();
                dbObj.From = vmObj.From;
                dbObj.To = vmObj.To;
                // todo: this is a fix, because the FinancialAid Report currently makes use of this property.
                dbObj.Days = dbObj.DaysCalculated;
                dbObj.SocialServicePayment = vmObj.SocialServicePayment;
                dbObj.PaymentPerDay = vmObj.PaymentPerDay;

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
            var obj = parent != null ? parent.FinancialAids.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MemberFinancialAid dbObj)
        {
            //var parent = Find(dbObj.MemberId);
            //var obj = dbObj != null ? parent.FinancialAids.Where(x => x.Id == dbObj.Id).FirstOrDefault() : null;
            return DeleteObject<MemberFinancialAid, Member>(dbObj.Id, dbObj.MemberId);// DeleteRelatedObject(parent.Id, obj);
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

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = db.Set<Member>().Find(id);
            if (member != null)
            {
                var result = member.FinancialAids.FilterDeleted().Select(m => new MemberFinancialAidViewModel()
                {
                    Id = m.Id,
                    From = m.From,
                    To = m.To,
                    Days = m.DaysCalculated,
                    SocialServicePayment = m.SocialServicePayment,
                    PaymentPerDay = m.PaymentPerDay,
                    OurPayment = m.OurPaymentCalculated,
                    BankAccountId = m.BankAccountId,
                    BankAccountRegNumber = m.BankAccount != null ? m.BankAccount.RegNumber : -1,
                    BankAccountAccountNumber = m.BankAccount != null ? m.BankAccount.AccountNumber : -1,
                    PrintedOn = m.PrintedOn,
                    PrintedBy = m.PrintedById
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpPost]
        public bool PrintReport(int? id)
        {
            var obj = db.Set<MemberFinancialAid>().Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                obj.PrintedById = GetUserIdCode();
                obj.PrintedOn = DateTime.UtcNow;
                return Update(obj);
            }
            return false;
        }
    }
}