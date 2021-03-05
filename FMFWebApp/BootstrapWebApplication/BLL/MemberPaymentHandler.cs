using BootstrapWebApplication.Controllers;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapWebApplication.BLL
{
    public class MemberPaymentHandler
    {
        public static MemberPayment Create(Member parent, MemberPaymentCreateViewModel vmObj, MemberPaymentController controller)
        {
            //Convert the ViewModel to DB Object (Model)
            var dbObj = new MemberPayment()
            {
                Year = vmObj.PaidOn.HasValue ? vmObj.PaidOn.Value.ToString("yy") : DateTime.Now.ToString("yy"),// vmObj.Year,
                HolidayPay = vmObj.HolidayPay,
                MembershipPayment = vmObj.MembershipPayment,
                Code = vmObj.Code,
                Price = vmObj.HolidayPay - vmObj.MembershipPayment,// vmObj.Price,
                MemberId = vmObj.MemberId,
                PaidOn = vmObj.PaidOn,
                PaidById = controller.GetUserIdCode()
            };

            BankAccount bankAccount = null;
            if (vmObj.BankId.HasValue)
            {
                bankAccount = controller.Db.BankAccounts.Where(x => x.Bank.Id == vmObj.BankId && x.AccountNumber == vmObj.BankAccountNumber).FirstOrDefault();
            }
            else if (vmObj.RegNumber.HasValue)
            {
                bankAccount = controller.Db.BankAccounts.Where(x => x.Bank.RegNumber == vmObj.RegNumber && x.AccountNumber == vmObj.BankAccountNumber).FirstOrDefault();
            }
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
            dbObj.ChangeEvent = controller.CreateChangeEvent();
            return dbObj;
        }
        
    }
}