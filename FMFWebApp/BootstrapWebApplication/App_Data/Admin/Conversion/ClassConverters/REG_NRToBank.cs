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
    public class REG_NRToBank
    {
        /// <summary>
        /// </summary>
        /// <param name="db"></param>
        /// <param name="o"></param>
        /// <param name="data"></param>
        public REG_NRToBank(BootstrapContext db, REG_NR o, ConverterData data)
        {
            var changeEvent = data.ChangeEventHandler.Create(db);

            var bank = new Bank()
            {
                ChangeEvent = changeEvent,
                Name = o.NAVN,
                RegNumber = o.NR
            };
            db.Banks.Add(bank);
        }
    }
}