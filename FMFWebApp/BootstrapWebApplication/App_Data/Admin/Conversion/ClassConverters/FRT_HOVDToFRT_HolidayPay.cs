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
    public class FRT_HOVDToFRT_HolidayPay_HOVD
    {
        public FRT_HOVDToFRT_HolidayPay_HOVD(BootstrapContext db, FRT_HOVD o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            
            var member = db.Members.Where(m => m.NR == o.PERS_NR).FirstOrDefault();
            //var holidayPay = db.HolidayPays.Where(x=>x.)
            var trip = db.Trips.Where(x => x.TripNumber == o.TR_NR).FirstOrDefault();

            // todo: This is disabled as the process takes too long. The account's can be created later on using KONTO & REG_NR or KONTO_FF & REG_NR_FF
            // when they might eventually be accessed by the user.
            //var bankAccount = db.BankAccounts.Where(x => x.Bank.RegNumber == o.REG_NR && x.AccountNumber == o.KONTO).FirstOrDefault();
            //if (bankAccount == null)
            //{
            //    var bank = db.Banks.Where(x => x.RegNumber == o.REG_NR).FirstOrDefault();
            //    bankAccount = new BankAccount()
            //    {
            //        IsPrimary = true,
            //        Bank = bank,
            //        //RegNumber = o.REG_NR.Value,
            //        AccountNumber = o.KONTO.Value
            //    };
            //    db.BankAccounts.Add(bankAccount);
            //}
            
            //var bankAccountFF = db.BankAccounts.Where(x => x.Bank.RegNumber == o.REG_NR_FF && x.AccountNumber == o.KONTO_FF).FirstOrDefault();
            //if (bankAccountFF == null)
            //{
            //    var bank = db.Banks.Where(x => x.RegNumber == o.REG_NR).FirstOrDefault();
            //    bankAccountFF = new BankAccount()
            //    {
            //        IsPrimary = true,
            //        Bank = bank,
            //        //RegNumber = o.REG_NR_FF.Value,
            //        AccountNumber = o.KONTO_FF.Value
            //    };
            //    db.BankAccounts.Add(bankAccountFF);
            //}
            // end of creating bank accounts

            var holidayPay_HOVD = new HolidayPay_HOVD()
            {
                ChangeEvent = changeEvent,
                EmployerNumber = o.ARB_NR,
                TransferDate = o.FLYT_DATO,
                //STATUS = o.STATUS,
                Amount = o.UPPH_ELE,
                //PERS_NR = o.PERS_NR,
                ART = o.ART,
                KOYR_DATO = o.KOYR_DATO,
                FRT_NR = o.FRT_NR,
                //FRT_TOT_NR = o.FRT_TOT_NR, // always 0 in csv file
                PLUS = o.PLUS,
                TR_NR = o.TR_NR,
                X = o.X, 
                Member = member,
                //HolidayPay = null,
                //todo: Later on create bank accounts
                KONTO = o.KONTO,
                REG_NR = o.REG_NR,
                KONTO_FF = o.KONTO_FF,
                REG_NR_FF = o.REG_NR_FF
                //BankAccount = bankAccount,
                //BankAccountFF = bankAccountFF,
                //Trip = trip
            };
            db.HolidayPay_HOVDs.Add(holidayPay_HOVD);
        }
    }
}