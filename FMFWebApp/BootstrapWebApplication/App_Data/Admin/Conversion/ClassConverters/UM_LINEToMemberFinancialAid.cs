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
    public class UM_LINEToMemberFinancialAid
    {
        public UM_LINEToMemberFinancialAid(BootstrapContext db, UM_LINE o, ConverterData data)
        {
            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault();
            if (member == null)
            {
                return;
            }
            
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var bankAccountChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            //todo: isPrimary? Lookup from db or create new (current solution)?

            var bankAccount = db.BankAccounts.Where(x=>x.Bank.RegNumber == o.REG && x.AccountNumber == o.KONTO).FirstOrDefault();
            if (bankAccount == null)
            {
                var bank = db.Banks.Where(x => x.RegNumber == o.REG).FirstOrDefault();
                bankAccount = new BankAccount()
                {
                    IsPrimary = true,
                    Bank = bank,
                    AccountNumber = o.KONTO.Value,
                    ChangeEvent = bankAccountChangeEvent
                };
                bankAccount.Entity = member.Person.Entity;
                db.BankAccounts.Add(bankAccount);
            }

            var memberFinancialAid = new MemberFinancialAid()
            {
                ChangeEvent = changeEvent,
                From = o.FRA_DATO,
                To = o.TIL_DATO,
                Days = o.DG,
                SocialServicePayment = o.ALM_UPPH,
                PaymentPerDay = o.PR_DG,
                OurPayment = o.FF_IALT,
                PrintedOn = Helper.ParseDate(o.UD_DATO, o.UD_HH, o.UD_MM),
                PrintedById = o.UD_ID,
                PrintedAmount = o.UD_ANT,
                Member = member,
                BankAccount = bankAccount
            };
            db.FinancialAids.Add(memberFinancialAid);
        }
    }
}