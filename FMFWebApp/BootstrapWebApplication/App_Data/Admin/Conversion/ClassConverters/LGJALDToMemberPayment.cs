using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.Models.Old;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public class LGJALDToMemberPayment
    {
        //ID,LG_NR,NR,AR,PRIS,ING_DATO,RET_DATO,RET_HH,RET_MM,RET_ID,ING_HH,ING_MM,ING_ID,REG_NR,KONTO,FRT_LON,LGJ,KOTA,TRANSP_IALT,SLAG,K1
        public LGJALDToMemberPayment(BootstrapContext db, LGJALD o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            
            ////Exception: NR is referring to PostNr in P100
            ////var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault(); //<-- WRONG!
            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault();
            
            BankAccount bankAccount = null;
            int regNr = o.REG_NR.Value;
            int accNr = o.KONTO.Value;
            if (regNr != 0 && accNr != 0)
            {
                bankAccount = db.BankAccounts.Where(b => b.Bank.RegNumber == regNr && b.AccountNumber == accNr).FirstOrDefault();
                //if (bankAccount != null)
                //    bankAccountId = bankAccount.Id;
            }
            //if (bankAccount == null)
            //{
            //    bankAccount = new BankAccount()
            //    {
            //        IsPrimary = false,
            //        RegNumber = o.REG_NR.Value,
            //        AccountNumber = o.KONTO.Value
            //    };
            //    db.BankAccounts.Add(bankAccount);
            //    //db.SaveChanges();
            //    member.BankAccounts.Add(bankAccount);
            //}

            var memberPayment = new MemberPayment()
            {
                ChangeEvent = changeEvent,
                MemberPaymentNR = o.LG_NR,
                //todo: remove MemberNR? (low priority)
                MemberNR = o.NR,
                Year = o.AR,
                Price = o.PRIS,
                PaidOn = Helper.ParseDate(o.ING_DATO, o.ING_HH, o.ING_MM),
                PaidById = o.ING_ID,
                BankAccount = bankAccount,
                HolidayPay = o.FRT_LON,
                MembershipPayment = o.LGJ,
                Code = o.KOTA,
                TRANSP_IALT = o.TRANSP_IALT,
                K1 = o.K1,
                Member = member
            };
            db.MemberPayments.Add(memberPayment);
        }
    }
}