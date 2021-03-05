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
    /// <summary>
    /// TODO: Check if this converter is correct!
    /// </summary>
    public class MI2_HOVDConverter
    {
        public MI2_HOVDConverter(BootstrapContext db, BootstrapWebApplication.Models.Old.MI2_HOVD o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var member = db.Members.Where(s => s.NR == o.MemberNR).FirstOrDefault();
            var shippingCompany = db.ShippingCompanies.Where(s => s.Code == o.ReidariId).FirstOrDefault();
            var ship = db.Ships.Where(s => s.Code == o.SkipId).FirstOrDefault();
            var trip = db.Trips.Where(s => s.Id == o.TR_HOVD_PostNr).FirstOrDefault();
            var job = db.Jobs.Where(j => j.Id == o.StarvId).FirstOrDefault();
            var status = db.Statuses.Where(s => s.Id == o.StatId).FirstOrDefault();
            var typa = db.ShipTypes.Where(s => s.Code == o.TypaId).FirstOrDefault();
            var postal = db.Postals.Where(s => s.Code == o.PostNrPostNr).FirstOrDefault();
            var company = db.Companies.Where(s => s.Code == o.CompanyId).FirstOrDefault();
            var memberType = db.MemberTypes.Where(m => m.Code == o.MemberTypeCode).FirstOrDefault();

            var mi2_hovd = new BootstrapWebApplication.Models.MI2_HOVD()
            {
                ChangeEvent = changeEvent,
                PERS_NR = o.PERS_NR,
                SKIB_ID = o.SKIB_ID,
                AR = o.AR,
                From = o.FRA_DATO,
                To = o.TIL_DATO,
                MI_NRGR = o.MI_NRGR.Value,
                PERS_NRGR = o.PERS_NR.Value,
                SK_NRGR = o.SK_NRGR,
                STARV20 = o.STARV20,
                DAGAR_GR = o.DAGAR_GR,
                Member = member,
                ShippingCompany = shippingCompany,
                Ship = ship,
                TR_HOVD = trip,
                Job = job,
                Status = status,
                ShipType = typa,
                Postal = postal,
                Company = company,
                MemberType = memberType
            };
            db.MI2_HOVDs.Add(mi2_hovd);
        }
    }
}