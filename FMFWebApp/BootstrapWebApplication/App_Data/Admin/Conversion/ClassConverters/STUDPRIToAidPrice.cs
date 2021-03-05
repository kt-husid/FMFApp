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
    public class STUDPRIToAidPrice
    {
        public STUDPRIToAidPrice(BootstrapContext db, STUDPRI o, ConverterData data)
        {
            //var db = new BootstrapContext();

            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var fishSpecies = db.FishSpecies.Where(s => s.OldCode == o.FiskArtId).FirstOrDefault();
            var typa = db.ShipTypes.Where(s => s.Code == o.TypaId).FirstOrDefault();
            var aidType = db.AidTypes.Where(s => s.AidTypeCode == o.StudArtId).FirstOrDefault();

            var aidPrice = new AidPrice()
            {
                ChangeEvent = changeEvent,
                From = o.FRA_DATO,
                To = o.TIL_DATO,
                Price = o.PRIS,
                Code = o.KOTA,
                SKIP = o.SKIP, 
                FID = o.FID,
                FishSpecies = fishSpecies,
                ShipType = typa,
                AidType = aidType
            };
            db.AidPrices.Add(aidPrice);
            //db.SaveChanges();
            //db.Dispose();
        }
    }
}