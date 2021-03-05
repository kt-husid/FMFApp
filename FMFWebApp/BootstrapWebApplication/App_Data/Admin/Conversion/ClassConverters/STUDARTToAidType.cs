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
    public class STUDARTToAidType
    {
        public STUDARTToAidType(BootstrapContext db, STUDART o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var aidType = new AidType()
            {
                ChangeEvent = changeEvent,
                AidTypeCode = o.StudArtCode,
                Name = o.NAVN,
                Description = o.STUTT,
                LINK = o.LINK,
                Type = o.TYPA,
            };
            db.AidTypes.Add(aidType);
        }
    }
}