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
    public class FISKPRISToFishPrice
    {
        public FISKPRISToFishPrice(BootstrapContext db, FISKPRI o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var fishPrice = new FishPrice()
            {
                ChangeEvent = changeEvent,
                FishSpeciesCode = o.ART,
                From = o.FRA_DATO,
                To = o.TIL_DATO,
                Price = o.PRIS
            };
            db.FishPrices.Add(fishPrice);
        }
    }
}