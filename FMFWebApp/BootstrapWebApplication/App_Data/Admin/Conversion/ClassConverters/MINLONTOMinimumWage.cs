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
    public class MINLONTOMinimumWage
    {
        public MINLONTOMinimumWage(BootstrapContext db, MINLON o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;// o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var minimumWage = new MinimumWage()
            {
                ChangeEvent = changeEvent,
                //From = o.FRA_DATO,
                //To = o.TIL_DATO,
                PerMonth = o.PR_MD,
                PerDay = o.PR_DG,
                DG_MIN = o.DG_MIN,
                DG_MAX = o.DG_MAX,
                GRAD = o.GRAD,
                DG_ST = o.DG_ST,
                Code = o.KOTA
            };
            db.MinimumWages.Add(minimumWage);
        }
    }
}