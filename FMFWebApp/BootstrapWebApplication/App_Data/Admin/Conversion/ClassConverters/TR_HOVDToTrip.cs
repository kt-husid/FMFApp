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
    /// </summary>
    public class TR_HOVDToTrip
    {
        public TR_HOVDToTrip(BootstrapContext db, TR_HOVD o, ConverterData data)
        {
            if (!string.IsNullOrEmpty(o.SkipId.Trim()) && o.ID_TUR.HasValue)
            {
                var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
                var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
                var updatedById = o.RET_ID;
                var createdById = o.OPR_ID;
                var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);


                var company = db.Companies.Where(s => s.Code == o.AVR_STAD).FirstOrDefault();
                //var shippingCompany = db.ShippingCompanies.Where(s => s.ShippingCompanyCode == o.ReidariId).FirstOrDefault();
                var ship = db.Ships.Where(s => s.Code == o.SkipId).FirstOrDefault(); // o.SkipId == o.ID_SKIB
                var shipType = db.ShipTypes.Where(x => x.Code == o.SK_ID).FirstOrDefault();
                Ship pairShip = null;
                if (!string.IsNullOrEmpty(o.PAR_SKIP))
                {
                    pairShip = db.Ships.Where(s => s.Code == o.PAR_SKIP).FirstOrDefault();
                }
                //var typa = db.ShipTypes.Where(s => s.Code == o.TypaId).FirstOrDefault();
                //var postal = db.Postals.Where(s => s.PostalCode == o.PostNrId).FirstOrDefault();

                var trip = new Trip()
                {
                    ChangeEvent = changeEvent,
                    TripId = o.ID_TUR.Value,// o.ID_TUR.HasValue ? o.ID_TUR.Value : -1,
                    From = o.FRA_DATO,
                    To = o.TIL_DATO,
                    TripNumber = o.TR_NR,
                    PairShip = pairShip,
                    PairTrawlerDocumentId = (o.PAR_SKJAL.HasValue && o.PAR_SKJAL.Value > 0) ? new Nullable<int>(o.PAR_SKJAL.Value) : null,
                    Crew = o.MANNING,
                    //PortOfLanding = o.AVR_STAD,
                    PortOfLanding = company,
                    DateOfLanding = Helper.ParseDate(o.AVR_DATO, "0", "0"),
                    ShipType= shipType,
                    TotalWeight = o.KG_IALT,
                    TotalAmount = o.UPPH_IALT,
                    //ShipsSharePercentage = o.TIL_SKIP_PCT,
                    //ShipsShareMoney = o.TIL_SKIP_KR,
                    CrewSharePercentage = o.TIL_MANN_PCT,
                    CrewShareMoney = o.TIL_MANN_KR,
                    VIDAR = o.VIDAR,
                    //Days = o.DAGAR,
                    CrewIncludingStayingAtHome = o.MANNING_I,
                    SKIP_STUD = o.SKIP_STUD,
                    MAN_STUD = o.MAN_STUD,
                    //TILSKOT = o.TILSKOT,
                    FRADRAG = o.FRADRAG,
                    TIL_MANN_TOT = o.TIL_MANN_TOT,
                    FRADRAG2 = o.FRADRAG2,
                    MANN_PART = o.MANN_PART,
                    PR_DAG = o.PR_DAG,
                    MinimumWage = o.MINLON,
                    DAGLON = o.DAGLON,
                    SKIB_TXT = o.SKIB_TXT,
                    TOTAL_KR = o.TOTAL_KR,
                    MANN_PART_IS = o.MANN_PART_IS,
                    MID_AR = o.MID_AR,
                    MANN_PART_I = o.MANN_PART_I,
                    EmployerNumber = o.ARB_NR,
                    PAR_ART = o.PAR_ART,
                    PAR_TUR_ID = o.PAR_TUR_ID,
                    MANNING_X = o.MANNING_X,
                    ALT_MP = o.ALT_MP,
                    ALT_MA = o.ALT_MA,
                    CHECK = o.CHECK,
                    TRYG_ANT = o.TRYG_ANT,
                    LaborInsurance = o.TRYG_KR,
                    TRYG_KRHB = o.TRYG_KRHB,
                    TRYG_BILAG = o.TRYG_BILAG,
                    TRYG_DATO = o.TRYG_DATO,
                    //Company = company,
                    //ShippingCompany = shippingCompany,
                    Ship = ship
                    //Typa = typa
                    //Postal = postal
                };
                db.Trips.Add(trip);
            }
        }
    }
}