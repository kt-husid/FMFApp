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
    public class TR_LINEToTripLine
    {
        public TR_LINEToTripLine(BootstrapContext db, TR_LINE o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            //var shippingCompany = db.ShippingCompanies.Where(s => s.ShippingCompanyCode == o.ReidariId).FirstOrDefault();
            //var ship = db.Ships.Where(s => s.Code == o.SkipId).FirstOrDefault();
            /***
             * Trip contains a reference to:
                 * company
                 * shippingCompany
                 * ship
                 * typa
                 * postal
             * */
            // var trip = db.Trips.Where(s => s.Id == o.TR_HOVD_PostNR).FirstOrDefault();
            var trip = db.Trips.Where(x => x.TripNumber == o.TR_NR).FirstOrDefault();
            var fishSpecies = db.FishSpecies.Where(s => s.OldCode == o.FiskArtId).FirstOrDefault();
            //var typa = db.ShipTypes.Where(s => s.TypaCode == o.TypaId).FirstOrDefault();
            //var postal = db.Postals.Where(s => s.PostalCode == o.PostNrPostNr).FirstOrDefault();
            //var company = db.Companies.Where(s => s.Code == o.AVR_STAD).FirstOrDefault();

            var amount = o.NOGD.Value;
            var unitPrice = o.PRIS.Value;
            var total = o.KR.Value;
            if (amount > 0 && amount * unitPrice != total)
            {
                unitPrice = total / amount;
            }

            var tripLine = new TripLine()
            {
                // start temp properties
                //ShippingCompanyCode = o.ReidariId,
                //Code = o.SkipId,
                //FishSpeciesCode = o.FiskArtId,
                //TypaCode = o.TypaId,
                //PostalCode = o.PostNrPostNr,
                //CompanyCode = o.AVR_STAD,
                // end temp properties
                ChangeEvent = changeEvent,
                TripNumber = o.TR_NR,
                Amount = amount,
                UnitPrice = unitPrice,
                AlternativePrice = o.ALT_PRIS,
                KR = total,
                PortOfLanding = o.AVR_STAD,
                DateOfLanding = o.AVR_DATO.Value,
                RAD = o.RAD,
                Year = o.AR,
                //SK_ID = o.SK_ID,
                STUD_M = o.STUD_M,
                STUD_S = o.STUD_S,
                //ShippingCompany = shippingCompany,
                //Ship = ship,
                Trip = trip,
                FishSpecies = fishSpecies,
                //Typa = typa,
                //Postal = postal,
                //Company = company
            };
            db.TripLines.Add(tripLine);
        }
    }
}