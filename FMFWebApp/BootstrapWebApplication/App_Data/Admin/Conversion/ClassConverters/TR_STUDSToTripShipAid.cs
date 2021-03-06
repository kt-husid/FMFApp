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
    public class TR_STUDSToTripShipAid
    {
        public TR_STUDSToTripShipAid(BootstrapContext db, TR_STUDS o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var shippingCompany = db.ShippingCompanies.Where(s => s.Code == o.ReidariId).FirstOrDefault();
            var ship = db.Ships.Where(s => s.Code == o.SkipId).FirstOrDefault();
            var trip = db.Trips.Where(s => s.Id == o.TR_HOVD_PostNr).FirstOrDefault();
            var typa = db.ShipTypes.Where(s => s.Code == o.TypaId).FirstOrDefault();
            var postal = db.Postals.Where(s => s.Code == o.PostNrPostNr).FirstOrDefault();
            var company = db.Companies.Where(s => s.Code == o.CompanyId).FirstOrDefault();

            var tripShipAid = new TripShipAid()
            {
                ChangeEvent = changeEvent,
                TripNumber = o.TR_NR,
                ART = o.ART,
                Amount = o.NOGD,
                Price = o.PRIS,
                AlternativePrice = o.ALT_PRIS,
                KR = o.KR,
                PortOfLanding = o.AVR_STAD,
                DateOfLanding = o.AVR_DATO,
                Company = company,
                ShippingCompany = shippingCompany,
                Ship = ship,
                ShipType = typa,
                Postal = postal,
                Trip = trip
            };
            //db.TripShipAids.Add(tripShipAid);
            throw new Exception("This converter should not be used");
        }
    }
}