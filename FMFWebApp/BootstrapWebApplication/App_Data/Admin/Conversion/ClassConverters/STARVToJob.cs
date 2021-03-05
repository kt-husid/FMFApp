using BootstrapWebApplication.Admin.Conversion;
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
    public class STARVToJob
    {
        public STARVToJob(BootstrapContext db, STARV o, ConverterData data)
        {
            var createdOn = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedOn = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var changeEvent = data.ChangeEventHandler.Create(db, createdOn, updatedOn, null, o.OPR_ID, o.RET_ID, null);
            
            var job = new Job()
            {
                OldId = o.Id,
                Code = o.Code,
                ChangeEvent = changeEvent,
                Description = o.TEKSTUR,
                Description2 = o.TEKSTUR2,
                STUTT = o.STUTT,
                Amount = o.ANTAL,
                TAL_DATO = Helper.ParseDate(o.TAL_DATO, o.TAL_HH, o.TAL_MM),
                TAL_ID = o.TAL_ID,
                PART = o.PART,
                TIL_DG = o.TIL_DG,
                TIL_TUR = o.TIL_TUR,
                Organization = o.FELAG,
                HasInsurance = o.ALKA.ToLower().Equals("j")
            };
            db.Jobs.Add(job);
        }
    }
}