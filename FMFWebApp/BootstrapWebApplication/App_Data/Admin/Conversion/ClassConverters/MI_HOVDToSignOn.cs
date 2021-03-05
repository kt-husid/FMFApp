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
    public class MI_HOVDToSignOn
    {
        public MI_HOVDToSignOn(BootstrapContext db, MI_HOVD o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var member = db.Members.Where(s => s.NR == o.MemberNR).FirstOrDefault();
            var shippingCompany = db.ShippingCompanies.Where(s => s.Code == o.ReidariId).FirstOrDefault(); //OK
            // Trip has ship
            //var ship = db.Ships.Where(s => s.Code == o.SkipId).FirstOrDefault(); //OK
            //var trip = db.Trips.Where(s => s.Id == o.TR_HOVD_PostNr).FirstOrDefault(); //OK
            var trip = db.Trips.Where(x => x.TripNumber == o.TR_NR).FirstOrDefault();
            //var job = db.Jobs.Where(s => s.Code == o.StarvId).FirstOrDefault();
            var jobWhileSignedOn = db.Jobs.Where(s => s.Code == o.STARV).FirstOrDefault();
            //var memberType = db.MemberTypes.Where(s => s.Code == o.StatId).FirstOrDefault();
            var memberTypeFromFile = string.IsNullOrEmpty(o.LslagId.Trim()) ? "uk" : o.LslagId.ToLower();
            var memberType = db.MemberTypes.Where(s => s.Code.ToLower().Equals(memberTypeFromFile)).FirstOrDefault();
            //Ship has typa
            //var typa = db.ShipTypes.Where(s => s.TypaCode == o.TypaId).FirstOrDefault();
            //var postal = db.Postals.Where(s => s.PostalCode == o.PostNrPostNr).FirstOrDefault();
            var company = db.Companies.Where(s => s.Code == o.CompanyId).FirstOrDefault();
            


            var signOn = new SignOn()
            {
                ChangeEvent = changeEvent,
                SignOnNumber = o.MI_NR,
                PersonNumber = o.PERS_NR,
                //Code = o.SKIB_ID,
                Year = o.AR,
                From = o.FRA_DATO,
                To = o.TIL_DATO,
                SK_ID = o.SK_ID,
                PART = o.PART,
                KR_IALT = o.KR_IALT,
                //KG_IALT = o.KG_IALT,
                //ID_TUR = o.ID_TUR,
                //TR_NR = o.TR_NR,
                //FRT_PART = o.FRT_PART,
                TIL_PR_DG = o.TIL_PR_DG,
                TIL_PR_TUR = o.TIL_PR_TUR,
                //PART_NETTO = o.PART_NETTO,
                //Birthday = o.FODT,
                EmployerNumber = o.ARB_NR,
                I_PART = o.I_PART,
                FRT_NR = o.FRT_NR,
                TRYG_JN = o.TRYG_JN,
                HasInsurance = o.TRYG_JN.ToLower().Equals("j"),
                LaborInsuranceAmountPerDay = o.TRYG_KR,
                TRYG_KVT = o.TRYG_KVT,
                Member = member,
                ShippingCompany = shippingCompany,
                Trip = trip,
                JobWhileSignedOn = jobWhileSignedOn,
                //Job = job,
                MemberType = memberType,
                //ShipType = typa,              // select TypaCode from TYPA where o.TypaId == 
                //Postal = postal,          // select POSTNR from P100 where o.PostNrPostNr == P100.NR
                Company = company,
                //MemberType = memberType,
                ShipCode = (trip != null && trip.Ship != null) ? trip.Ship.Code : null,
                ShipName = (trip != null && trip.Ship != null) ? trip.Ship.Name : null
            };
            db.SignOns.Add(signOn);
        }
    }
}