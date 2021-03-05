using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AutoMapper;
using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Hubs;
using Microsoft.AspNet.SignalR;

namespace BootstrapWebApplication.Controllers
{
    public class TripController : Controller<Trip>
    {
        protected override Trip DetailsGetHook(Trip obj)
        {
            // load tripdeductions explicitly
            db.Entry(obj).Collection(x => x.TripLines).Load();
            db.Entry(obj).Collection(x => x.TripDeductions).Load();
            db.Entry(obj).Collection(x => x.SignOns).Load();
            return base.DetailsGetHook(obj);
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var ship = db.Ships.Where(x => x.Id == id).FirstOrDefault();// db.Set<Ship>().Find(id);
            if (ship != null)
            {
                //var result = ship.Trips.Select(m => Mapper.CreateMap<Trip, ShipTripViewModel>());
                var user = GetUser();
                var laborInsuranceAmountPerDay = (user != null && user.AppSettings != null) ? user.AppSettings.LaborInsuranceAmountPerDay : 0m;


                var fromDate = new DateTime(DateTime.Now.Year-1, 1, 1);// DateTime.Now.AddYears(-1);
                var toDate = new DateTime(DateTime.Now.Year + 1, 1, 1); // Hans legg 1 ár fram
                
                if (request.Filters.Count == 1)
                {
                    var filter = request.Filters[0] as Kendo.Mvc.CompositeFilterDescriptor;
                    if (filter.FilterDescriptors.Count == 2)
                    {
                        var filter1 = filter.FilterDescriptors[0] as Kendo.Mvc.FilterDescriptor;
                        var filter2 = filter.FilterDescriptors[1] as Kendo.Mvc.FilterDescriptor;
                        if (filter1.Member == "from")
                        {
                            DateTime.TryParseExact(filter1.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDate);
                        }
                        if (filter2.Member == "to")
                        {
                            DateTime.TryParseExact(filter2.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out toDate);
                        }
                    }
                }
                request.Filters.Clear();
                
              //  var tripsOrdered = GetShipTripsOrdered(ship);//, fromDate, toDate);
                var tripsOrdered = ship.Trips
                    .FilterDeleted()
                    .Where(x => DateTime.Compare(fromDate, x.From.Value) <= 0 && DateTime.Compare(toDate, x.To.Value) >= 0) //Hans
                    .OrderByDescending(x => x.From)
                    //.ThenByDescending(x => x.From)
                    .AsQueryable();
                

                //var result = new List<ShipTripViewModel>();
                //foreach (var trip in tripsOrdered)
                //{
                //    db.Entry(trip).Collection(x => x.SignOns).Load();
                //    result.Add(new ShipTripViewModel()
                //    {
                //        Id = trip.Id,
                //        TripId = trip.TripId,
                //        From = trip.From,
                //        To = trip.To,
                //        Days = trip.Days,
                //        LaborInsurance = trip.LaborInsurance,
                //        LaborInsuranceTotal = trip.LaborInsurance.HasValue ? new Nullable<decimal>(trip.LaborInsurance.Value * (decimal)trip.Days) : null,
                //        // todo: how's this calculated?
                //        Paid = null,
                //        Document = "?",
                //        Crew = trip.Crew,
                //        CrewIncludingStayingAtHome = trip.CrewIncludingStayingAtHome,
                //        CrewWithInsurance = trip.CrewWithInsurance,
                //        CrewSharePercentage = trip.CrewSharePercentage,
                //        CrewSharePart = trip.CrewSharePart,
                //        VIDAR = trip.VIDAR,
                //        CrewSharePerCrewMember = trip.MANN_PART,//m.CrewSharePerCrewMember,<-- requires explicit loading of TripLines + TripDeductions (costly). Use mann_part
                //        TotalWeight = trip.TotalWeight,// m.TotalWeightCalculated,// m.TotalWeight,
                //        //todo: til per dg
                //    });
                //}

                var result = tripsOrdered.Select(m => new ShipTripViewModel()
                {
                    Id = m.Id,
                    TripId = m.TripId,
                    TripIdStr = ("" + m.TripId).PadLeft(4).Replace(" ", "0"),
                    From = m.From,
                    To = m.To,
                    Days = m.Days,
                    LaborInsurance = m.LaborInsurance,
                    LaborInsuranceTotal = m.LaborInsuranceTotal,
                    // todo: how's this calculated?
                    //Paid = null,
                    Document = "?",
                    Crew = m.Crew,
                    CrewIncludingStayingAtHome = m.CrewIncludingStayingAtHome,
                    CrewWithInsurance = m.CrewWithInsurance,
                    CrewSharePercentage = m.CrewSharePercentage,
                    CrewSharePart = m.CrewSharePart,
                    VIDAR = m.VIDAR,
                    CrewSharePerCrewMember = m.MANN_PART,//m.CrewSharePerCrewMember,<-- requires explicit loading of TripLines + TripDeductions (costly). Use mann_part
                    TotalWeight = m.TotalWeight,// m.TotalWeightCalculated,// m.TotalWeight,
                    TRYG_BILAG = m.TRYG_BILAG,
                    TRYG_KRHB = m.TRYG_KRHB,
                    TRYG_KRHB_TOTAL = m.LundinAlkaPaymentBookings.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue).Sum(x => x.Amount),
                    LaborInsuranceDifference = m.TRYG_KRHB.HasValue ? m.LaborInsuranceTotal - m.TRYG_KRHB : 0m,
                    MembershipPayment = m.SignOns.FilterDeleted().Sum(x => (x.HasInsurance && x.KR_IALT.HasValue) ? (x.KR_IALT.Value * 0.0125m) : 0m),
                    MembershipPaymentPaid = m.MembershipPaymentPaid.HasValue ? m.MembershipPaymentPaid.Value : 0m,
//               MembershipPaymentTotal = m.LundinMembershipPaymentBookings.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue).Last().Amount, // Hans vís síðsta gjald
//               MembershipPaymentTotal = m.LundinMembershipPaymentBookings.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue).Sum(x => x.Amount), // Hans vís sum av gjøldum
                    MembershipPaymentNr = m.MembershipPaymentNr, // last(m.LundinMembershipPaymentBookings.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue).bokid),  // Hans vís síðsta gjald
                    //todo: til per dg
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        protected override IQueryable<Trip> PagedListFilter(IQueryable<Trip> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.From.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.To.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        protected override void AddViewBag(Trip trip)
        {
            ViewBag.CompanyId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name + " (" + m.Code + ")",
                Value = m.Id.ToString()
            }), "Value", "Text", trip != null ? trip.TripLines.FirstOrDefault().CompanyId : null);

            ViewBag.DeductionTypeId = new SelectList(db.DeductionTypes.ToList().Select(m => new SelectListItem
            {
                Text = m.Name + " (" + m.Code + ")",
                Value = m.Id.ToString()
            }), "Value", "Text", trip != null ? trip.TripDeductions.FirstOrDefault().DeductionTypeId : null);

            ViewBag.PortOfLandingId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text");
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.PostalId = new SelectList(db.Postals, "Id", "CityName");
            //ViewBag.ShipId = new SelectList(db.Ships, "Id", "Code");
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies, "Id", "Name");
            //ViewBag.ShipTypeId = new SelectList(db.ShipTypes, "Id", "TEKSTUR");
        }

        [HttpGet]
        public ActionResult Trips(int? id)
        {
            return GetRelatedDataForParent<Ship>(id, "Trips");
        }

        [HttpGet]
        public override ActionResult Details(int? id, string type)
        {
            return DetailsHandler<TripViewModel>(id, (o) =>
            {
                return new TripViewModel()
                {
                    Id = o.Id,
                    TripId = o.TripId,
                    From = o.From,
                    To = o.To,
                    TripNumber = o.TripNumber,
                    PairTrawlerDocumentId = o.PairTrawlerDocumentId,
                    //CrewWithInsurance = o.CrewWithInsurance,
                    ShipTypeCode = o.ShipTypeCode,
                    ShipTypeId = o.ShipTypeId,
                    ShipType = o.ShipType,
                    TotalWeight = o.TotalWeight,
                    //TotalWeightCalculated = o.TotalWeightCalculated,
                    //IsTotalWeightCorrect = o.IsTotalWeightCorrect,
                    TotalAmount = o.TotalAmount,
                    //TotalAmountCalculated = o.TotalAmountCalculated,
                    //IsTotalAmountCorrect = o.IsTotalAmountCorrect,
                    CrewSharePercentage = o.CrewSharePercentage,
                    CrewSharePart = o.CrewSharePart,
                    CrewShareMoney = o.CrewShareMoney,
                    //CrewShareMoneyCalculated = o.CrewShareMoneyCalculated,
                    //CrewShareMoneyPerDay = o.CrewShareMoneyPerDay,
                    VIDAR = o.VIDAR,
                    //Days = o.Days,
                    Crew = o.Crew,
                    CrewIncludingStayingAtHome = o.CrewIncludingStayingAtHome,
                    MANNING_X = o.MANNING_X,
                    SKIP_STUD = o.SKIP_STUD,
                    MAN_STUD = o.MAN_STUD,
                    //DeductionsTotal = o.DeductionsTotal,
                    FRADRAG = o.FRADRAG,
                    TIL_MANN_TOT = o.TIL_MANN_TOT,
                    FRADRAG2 = o.FRADRAG2,
                    MANN_PART = o.MANN_PART,
                    //CrewSharePerCrewMember = o.CrewSharePerCrewMember,
                    //CrewPartPerCrewMember = o.CrewPartPerCrewMember,
                    //PerDay = o.PerDay,
                    PR_DAG = o.PR_DAG,
                    MinimumWage = o.MinimumWage,
                    DAGLON = o.DAGLON,
                    SKIB_TXT = o.SKIB_TXT,
                    TOTAL_KR = o.TOTAL_KR,
                    MANN_PART_IS = o.MANN_PART_IS,
                    MID_AR = o.MID_AR,
                    MANN_PART_I = o.MANN_PART_I,
                    EmployerNumber = o.EmployerNumber,
                    PAR_ART = o.PAR_ART,
                    PAR_TUR_ID = o.PAR_TUR_ID,
                    ALT_MP = o.ALT_MP,
                    ALT_MA = o.ALT_MA,
                    CHECK = o.CHECK,
                    TRYG_ANT = o.TRYG_ANT,
                    LaborInsurance = o.LaborInsurance,
                    //LaborInsuranceTotal = o.LaborInsuranceTotal,
                    TRYG_KRHB = o.TRYG_KRHB,
                    TRYG_KVIT = o.TRYG_KVIT,
                    TRYG_BILAG = o.TRYG_BILAG,
                    TRYG_DATO = o.TRYG_DATO,
                    PortOfLandingId = o.PortOfLandingId,
                    PortOfLanding = o.PortOfLanding,
                    DateOfLanding = o.DateOfLanding,
                    ShipId = o.ShipId,
                    Ship = o.Ship,
                    PairShipId = o.PairShipId,
                    PairShip = o.PairShip,
                    //PrimaryAddress = o.PrimaryAddress,
                    SignOns = o.SignOns,
                    MI2_HOVD = o.MI2_HOVD,
                    ShippingCompanies = o.ShippingCompanies,
                    TripDeductions = o.TripDeductions,
                    TripLines = o.TripLines,
                    TripCrewAid = o.TripCrewAid,
                    TripShipAid = o.TripShipAid
                };
            }, type);
        }

        [HttpGet]
        public ActionResult Create(int? shipId)
        {
            var parent = db.Set<Ship>().Find(shipId);

            var lastTrip = GetShipTripsOrdered(parent).FirstOrDefault();

            AddViewBagForCreateEditShipTrip(parent, null);

            ViewModelBaseWithChangeEvent viewModel = new ShipTripCreateViewModel()
            {
                TripId = lastTrip != null ? new Nullable<int>(lastTrip.TripId + 1) : null,
                //From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01),
                //To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                //Crew = 12,
                CrewSharePercentage = (parent != null && parent.ShipType != null) ? parent.ShipType.PctToCrewMember : 0m,
                CrewSharePart = null,//(parent != null && parent.ShipType != null) ? parent.ShipType.PctToCrewMember : 0m,
                PairTrawlerDocumentId = null
                //CompanyCode = "hv",
                //TotalWeight = 0,
                //TotalAmount = 0
            };
            return Create("CreateOrEdit", viewModel, parent);
        }

        /// <summary>
        /// Creates the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipTripCreateViewModel o)// ShipTripCreateViewModel o)
        {
            if (!o.CrewIncludingStayingAtHome.HasValue || o.CrewIncludingStayingAtHome.Value <= 0)
            {
                return GetViewResult(o);
            }
            if (!o.From.HasValue || !o.To.HasValue)
            {
                ModelState.AddModelError("ErrorMessage", "Dato er ikki rætt ásett!");
                return GetViewResult(o);
            }
            var minWageFrom = db.MinimumWages.FilterDeleted().Where(x => x.Code == o.From.Value.ToString("yyMM")).FirstOrDefault(); // 1502 4dg
            var minWageTo = db.MinimumWages.FilterDeleted().Where(x => x.Code == o.To.Value.ToString("yyMM")).FirstOrDefault();     // 1504 6dg
            if (minWageFrom == null || minWageTo == null)
            {
                ModelState.AddModelError("ErrorMessage", "Kanna eftir um minstaløn er løgd inn í skipanina fyri ásetta tíðarskeiðið!");
                return GetViewResult(o);
            }
            return CreateRelatedObjectUsingViewModel<Ship, Trip>(o.ShipId, o, (parent) =>
            {
                // Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<Trip>(obj);

                var lastTripInDb = db.Trips.OrderByDescending(x => x.TripNumber).FirstOrDefault();
                var user = GetUser();
                var laborInsuranceAmountPerDay = (user != null && user.AppSettings != null) ? user.AppSettings.LaborInsuranceAmountPerDay : 0m;

                var dbObj = new Trip()
                {
                    TripId = o.TripId.Value,
                    From = o.From,
                    To = o.To,
                    TripNumber = lastTripInDb != null ? lastTripInDb.TripNumber + 1 : int.Parse(DateTime.Now.Year.ToString("yyMM")), // Deprecated (Trip.Id replaces this)
                    PairShip = o.PairShipId.HasValue ? db.Set<Ship>().Find(o.PairShipId) : null,
                    PairTrawlerDocumentId = o.PairTrawlerDocumentId,
                    Crew = o.CrewIncludingStayingAtHome,
                    CrewIncludingStayingAtHome = o.CrewIncludingStayingAtHome,
                    MANNING_X = o.CrewIncludingStayingAtHome,
                    PortOfLandingId = o.PortOfLandingId,
                    //PortOfLanding = select from db,
                    DateOfLanding = o.To,// o.DateOfLanding,//Helper.ParseDate(o.DateOfLanding, "0", "0"),
                    //ShipTypeCode = parent.ShipType.TypaCode,
                    TotalWeight = o.TotalWeight,
                    TotalAmount = o.TotalAmount,
                    CrewSharePercentage = o.CrewSharePercentage,
                    CrewSharePart = o.CrewSharePart,
                    //CrewShareMoney = ,
                    VIDAR = "J",// o.VIDAR,
                    //Days = o.DAGAR,
                    SKIP_STUD = 0,
                    MAN_STUD = 0,
                    //TILSKOT = o.TILSKOT,
                    //FRADRAG = o.FRADRAG,
                    //TIL_MANN_TOT = CrewShareMoney + MAN_STUD, //deprecated
                    FRADRAG2 = 0m,
                    MANN_PART = o.MANN_PART,
                    //PR_DAG = o.PR_DAG,
                    DAGLON = null,                  //?
                    SKIB_TXT = parent.Name,
                    TOTAL_KR = null,                //?
                    MANN_PART_IS = null,            //?
                    MID_AR = null,                  //?
                    MANN_PART_I = null,             //?
                    EmployerNumber = parent.ShippingCompany.EmployerNumber,
                    PAR_ART = null,                 //?
                    PAR_TUR_ID = null,              //?
                    ALT_MP = null,                  //?
                    ALT_MA = null,                  //?
                    CHECK = null,                   //?
                    TRYG_ANT = null,                //?
                    //LaborInsurance = null,
                    //TRYG_KRHB = null,
                    TRYG_BILAG = null,              //?
                    TRYG_DATO = null,               //?
                    //Company = company,
                    //ShippingCompany = shippingCompany,
                    Ship = parent,
                    ShipType = parent.ShipType,
                    ShipTypeCode = parent.ShipType != null ? parent.ShipType.Code : null
                    //Postal = postal 
                };
                var laborInsurance = (laborInsuranceAmountPerDay.HasValue && o.CrewIncludingStayingAtHome.HasValue) ? laborInsuranceAmountPerDay.Value * o.CrewIncludingStayingAtHome.Value : 0m;
                //var laborInsurance = (laborInsuranceAmountPerDay.HasValue && o.CrewIncludingStayingAtHome.HasValue) ? laborInsuranceAmountPerDay.Value *  : 0m;
                dbObj.LaborInsurance = new Nullable<decimal>(laborInsurance);
                dbObj.MinimumWage = TripHandler.CalcMinimumWage(o.From.Value, o.To.Value, minWageFrom, minWageTo, dbObj);
                //dbObj.MANN_PART = dbObj.CrewSharePerCrewMember;
                dbObj.MANN_PART_I = dbObj.CrewSharePerCrewMember;
                dbObj.MANN_PART_IS = dbObj.CrewSharePerCrewMember;
                //dbObj.TotalWeight = dbObj.TotalWeightCalculated;
                //dbObj.TRYG_KRHB = (decimal)dbObj.Days * laborInsurance;
                return dbObj;
            });
        }

        private decimal CalcMinimumWage(DateTime dtFrom, DateTime dtTo, MinimumWage minWageFrom, MinimumWage minWageTo)
        {
            var minWage = 0m;
            var days = dtTo.Subtract(dtFrom).Days + 1;
            //var minWageFrom = db.MinimumWages.FilterDeleted().Where(x => x.Code == dtFrom.ToString("yyMM")).FirstOrDefault(); // 1502 4dg
            //var minWageTo = db.MinimumWages.FilterDeleted().Where(x => x.Code == dtTo.ToString("yyMM")).FirstOrDefault();     // 1504 6dg
            var minWageFromDays = dtFrom.Month == dtTo.Month ? dtTo.Subtract(dtFrom).TotalDays + 1 : new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month)).Subtract(dtFrom).TotalDays + 1;
            var minWageToDays = dtFrom.Month == dtTo.Month ? 0 : dtTo.Day;
            var minWageBetweenVal = 0m;
            // 1504 - 1502 = 2. 1503 - 1502 = 1
            var monthDiff = int.Parse(dtTo.ToString("yyMM")) - int.Parse(dtFrom.ToString("yyMM"));
            if (monthDiff > 1)
            {
                for (int i = 1; i < monthDiff; i++)
                {
                    var nextMonth = (int.Parse(dtFrom.ToString("MM")) + i) % 12;
                    var code = int.Parse(dtFrom.ToString("yyMM")) + i;
                    var minWageBetween = db.MinimumWages.FilterDeleted().Where(x => x.Code == ("" + code)).FirstOrDefault();
                    var daysInMonth = (DateTime.DaysInMonth(dtFrom.Year, nextMonth));
                    minWageBetweenVal += (decimal)(daysInMonth / days) * (minWageBetween.PerDay.HasValue ? minWageBetween.PerDay.Value : 0m);
                }
            }
            minWage = minWageBetweenVal + ((decimal)(minWageFromDays / days) * (minWageFrom.PerDay.HasValue ? minWageFrom.PerDay.Value : 0m)) + ((decimal)(minWageToDays / days) * (minWageTo.PerDay.HasValue ? minWageTo.PerDay.Value : 0m));
            return minWage;
        }

        private ActionResult GetViewResult(ShipTripCreateViewModel o)
        {
            AddViewBagForCreateEditShipTrip(db.Ships.FilterDeleted().Where(x => x.Id == o.ShipId).FirstOrDefault(), null);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView("CreateOrEdit", o);
            }
            return View("CreateOrEdit", o);
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? shipId)
        {
            var parent = db.Set<Ship>().Find(shipId);
            var obj = parent != null ? parent.Trips.Where(x => x.Id == id).FirstOrDefault() : null;

            AddViewBagForCreateEditShipTrip(parent, obj);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                viewModel = new ShipTripCreateViewModel()
                {
                    TripId = obj.TripId,
                    From = obj.From,
                    To = obj.To,
                    Days = obj.Days,
                    CrewIncludingStayingAtHome = obj.CrewIncludingStayingAtHome, // MANNING_I
                    CrewSharePercentage = obj.CrewSharePercentage.HasValue ? obj.CrewSharePercentage.Value : 0m,
                    CrewSharePart = obj.CrewSharePart,
                    CompanyCode = obj.PortOfLanding != null ? obj.PortOfLanding.Code : null,
                    PortOfLandingId = obj.PortOfLandingId.Value,
                    PortOfLanding = obj.PortOfLanding,
                    VIDAR = obj.VIDAR,
                    CrewSharePerCrewMember = obj.CrewSharePerCrewMember,
                    TotalWeight = obj.TotalWeight,
                    MANN_PART = obj.MANN_PART,
                    TotalAmount = obj.TotalAmount,
                    DateOfLanding = obj.DateOfLanding,
                    PairShip_ShipCode = obj.PairShip != null ? obj.PairShip.Code : null,
                    PairShipId = obj.PairShipId,
                    PairShip = obj.PairShip,
                    PairTrawlerDocumentId = obj.PairTrawlerDocumentId,
                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShipTripCreateViewModel vmObj)
        {
            if (!vmObj.CrewIncludingStayingAtHome.HasValue || vmObj.CrewIncludingStayingAtHome.Value <= 0)
            {
                if (Request != null && Request.IsAjaxRequest())
                {
                    return PartialView(vmObj);
                }
                return View(vmObj);
            }
            return UpdateRelatedObjectUsingViewModel<Ship, Trip>(vmObj.ShipId, vmObj, (parent) =>
            {
                var user = GetUser();
                var laborInsuranceAmountPerDay = (user != null && user.AppSettings != null) ? user.AppSettings.LaborInsuranceAmountPerDay : 0m;

                //Convert the ViewModel to DB Object (Model)
                //var dbObj = Mapper.Map<Trip>(o);
                var dbObj = parent.Trips.Where(x => x.Id == vmObj.Id).FirstOrDefault();

                //var crew = TripHandler.GetTripSignOnsExcludingStayingAtHome(dbObj).Count();
                //var tripSignOnCount = dbObj.SignOns.FilterDeleted().Count();
                //vmObj.CrewIncludingStayingAtHome = vmObj.CrewIncludingStayingAtHome <= tripSignOnCount ? vmObj.CrewIncludingStayingAtHome : tripSignOnCount;

                var laborInsurance = (laborInsuranceAmountPerDay.HasValue && dbObj.CrewIncludingStayingAtHome.HasValue) ? laborInsuranceAmountPerDay.Value * dbObj.CrewIncludingStayingAtHome.Value : 0m;

                dbObj.TripId = vmObj.TripId.HasValue ? vmObj.TripId.Value : dbObj.TripId;
                dbObj.From = vmObj.From;
                dbObj.To = vmObj.To;
                //dbObj.Crew = vmObj.CrewIncludingStayingAtHome;
                dbObj.CrewIncludingStayingAtHome = vmObj.CrewIncludingStayingAtHome; // MANNING_I
                dbObj.MANNING_X = vmObj.CrewIncludingStayingAtHome;
                dbObj.CrewSharePercentage = vmObj.CrewSharePercentage;
                dbObj.CrewSharePart = vmObj.CrewSharePart;
                dbObj.PortOfLandingId = vmObj.PortOfLandingId;
                dbObj.VIDAR = vmObj.VIDAR;
                dbObj.MANN_PART = vmObj.MANN_PART.HasValue ? vmObj.MANN_PART.Value : dbObj.MANN_PART;
                //dbObj.MANN_PART = vmObj.CrewSharePerCrewMember;
                dbObj.MANN_PART_I = vmObj.CrewSharePerCrewMember.HasValue ? vmObj.CrewSharePerCrewMember.Value : dbObj.MANN_PART_I;
                dbObj.MANN_PART_IS = vmObj.CrewSharePerCrewMember.HasValue ? vmObj.CrewSharePerCrewMember.Value : dbObj.MANN_PART_IS;
                dbObj.TotalWeight = vmObj.TotalWeight.HasValue ? vmObj.TotalWeight.Value : dbObj.TotalWeight;
                dbObj.TotalAmount = vmObj.TotalAmount.HasValue ? vmObj.TotalAmount.Value : dbObj.TotalAmount;
                dbObj.DateOfLanding = vmObj.To.HasValue ? vmObj.To : dbObj.DateOfLanding;// vmObj.DateOfLanding;
                dbObj.PairShipId = vmObj.PairShipId;
                dbObj.PairTrawlerDocumentId = vmObj.PairTrawlerDocumentId.HasValue ? vmObj.PairTrawlerDocumentId.Value : dbObj.PairTrawlerDocumentId;
                dbObj.LaborInsurance = laborInsurance;
                //dbObj.TRYG_KRHB = (decimal)dbObj.Days * laborInsurance;

                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? shipId)
        {
            var parent = db.Set<Ship>().Find(shipId);
            var obj = parent != null ? parent.Trips.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipTripViewModel dbObj)
        {
            //var parent = Find(dbObj.ShipId);
            //var obj = dbObj != null ? parent.Trips.Where(x => x.Id == dbObj.Id).FirstOrDefault() : null;
            return DeleteObject<Trip, Ship>(dbObj.Id, dbObj.ShipId);// DeleteRelatedObject(parent.Id, obj);
        }

        public ActionResult ReadTripSignOns([DataSourceRequest] DataSourceRequest request, int tripId)
        {
            var trip = db.Set<Trip>().Find(tripId);
            if (trip != null)
            {
                var result = trip.SignOns.FilterDeleted().Select(m => new ShipSignOnViewModel()
                {
                    Id = m.Id,
                    From = m.From,
                    To = m.To,
                    ShipCode = m.ShipCode,
                    JobCode = m.JobCode,
                    DG_IALT = ((m.To.HasValue && m.From.HasValue) ? m.To.Value.Subtract(m.From.Value).Days + 1 : 0),
                    Birthday = ((m.Member != null && m.Member.Person != null) ? m.Member.Person.Birthday : null),
                    FullName = ((m.Member != null && m.Member.Person != null) ? m.Member.Person.FullName : "")
                    //ChangeEvent = m.ChangeEvent
                });
                return Json(result.ToDataSourceResult(request));
            }
            return Json(null);
        }

        private IQueryable<Trip> GetShipTripsOrdered(Ship ship, DateTime? from = null, DateTime? to = null)
        {
            //db.Configuration.LazyLoadingEnabled = false;
            //var result = ship.Trips.FilterDeleted().OrderByDescending(x => x.Id).ThenByDescending(x => x.From).AsQueryable();
            //db.Configuration.LazyLoadingEnabled = true;
            //return result;
            if (from.HasValue && to.HasValue)
            {
                ship.Trips.FilterDeleted().Where(x => DateTime.Compare(from.Value, x.From.Value) <= 0 && DateTime.Compare(to.Value, x.To.Value) >= 0).OrderByDescending(x => x.From).AsQueryable();
            }
            return ship.Trips.FilterDeleted().OrderByDescending(x => x.From).AsQueryable();
        }

        private void AddViewBagForCreateEditShipTrip(Ship ship, Trip trip)
        {
            ViewBag.HasPairShip = (ship != null && ship.ShipType != null) ? ship.ShipType.Code == "3" : false;

            ViewBag.PortOfLandingId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", trip != null ? trip.PortOfLandingId : null);

            ViewBag.PairShipId = new SelectList(db.Ships.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", trip != null ? trip.ShipId : null);
        }

        [HttpPost]
        public ActionResult CopyLastSignOns(int? tripId, int? shipId)
        {
            if (tripId.HasValue && shipId.HasValue)
            {
                var ship = db.Ships.Where(x => x.Id == shipId).FirstOrDefault();
                if (ship != null)
                {
                    var trip = ship.Trips.Where(x => x.Id == tripId).FirstOrDefault();
                    var lastTrip = ship.Trips.FilterDeleted().Where(x => x.Id != tripId).OrderByDescending(x => x.Id).FirstOrDefault();
                    if (trip != null && lastTrip != null)
                    {
                        var hubCtx = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
                        var hub = new LundinBookingsHubHelper(hubCtx);

                        // Load the triplines & deductions for the current trip
                        db.Entry(trip).Collection(x => x.TripLines).Load();
                        db.Entry(trip).Collection(x => x.TripDeductions).Load();
                        // Load the signons for the last trip
                        db.Entry(lastTrip).Collection(x => x.SignOns).Load();

                        var signOnsCopied = 0;
                        var totalSignOns = lastTrip.SignOns.FilterDeleted().ToList().Count;
                        foreach (var signOn in lastTrip.SignOns.FilterDeleted())
                        {
                            var progress = Math.Round((((signOnsCopied + 1) * 100.0) / (double)totalSignOns), 2);
                            hub.UpdateProgress(progress);
                            if (trip.SignOns.FilterDeleted().Where(x => x.MemberId == signOn.MemberId).FirstOrDefault() == null)
                            {
                                var lastSignOn = db.SignOns.OrderByDescending(x => x.SignOnNumber).FirstOrDefault();
                                var member = signOn.Member;
                                var KR_IALT = SignOnHandler.CalcKR_IALT(trip, new TripSignOnCreateEditViewModel()
                                {
                                    From = trip.From,
                                    To = trip.To,
                                    TIL_PR_DG = signOn.TIL_PR_DG,
                                    TIL_PR_TUR = signOn.TIL_PR_TUR
                                });
                                trip.SignOns.Add(new SignOn()
                                {
                                    SignOnNumber = lastSignOn != null ? lastSignOn.SignOnNumber + 1 : null,
                                    PersonNumber = member.NR,
                                    Year = signOn.To.HasValue ? signOn.To.Value.ToString("yy") : DateTime.Now.ToString("yy"),
                                    //From = signOn.From, Hans broytt til trip dato
                                    //To = signOn.To,
                                    From = trip.From,
                                    To = trip.To,
                                    SK_ID = trip.ShipTypeCode, //Hans
                                    ShipCode = trip.Ship.Code, //Hans
                                    ShipName = trip.Ship.Name, //Hans
                                    PART = signOn.PART,
                                    KR_IALT = KR_IALT,
                                    //KG_IALT = null,
                                    //ID_TUR = trip.TripId,
                                    //TR_NR = trip.TripNumber,
                                    //FRT_PART = 0m,
                                    TIL_PR_DG = signOn.TIL_PR_DG,
                                    TIL_PR_TUR = signOn.TIL_PR_TUR,
                                    //PART_NETTO = 0m,
                                    EmployerNumber = trip.EmployerNumber,
                                    I_PART = (KR_IALT.HasValue ? KR_IALT : 1m) / ((signOn.PART.HasValue || signOn.PART.Value != 0) ? signOn.PART : 1m), // correctly calculated?
                                    FRT_NR = 0,
                                    // !!! NOTE!!!
                                    // if TRYG_JN == 'J', then 'samlagstrygging' has to be calculated
                                    TRYG_JN = signOn.JobWhileSignedOn.HasInsurance ? "J" : "N",
                                    HasInsurance = signOn.JobWhileSignedOn.HasInsurance,
                                    // todo: calculate samlagstrygging !!!
                                    LaborInsuranceAmountPerDay = signOn.JobWhileSignedOn.HasInsurance ? signOn.LaborInsuranceAmountPerDay  : 0m, //Hans: bara trygging um starvið hevur trygging
                                    //TRYG_KR = job.HasInsurance ? new Nullable<decimal>(0m) : null,// trygJN.ToLower().Equals("j") ? new Nullable<decimal>(0m) : null,
                                    TRYG_KVT = String.Empty, // todo: what's this for?
                                    Member = member,
                                    ShippingCompanyId = (trip != null && trip.Ship != null) ? trip.Ship.ShippingCompanyId : null,
                                    Trip = trip,
                                    TripId = tripId,
                                    JobWhileSignedOn = signOn.JobWhileSignedOn,
                                    MemberType = member != null ? member.MemberType : null,
                                    Company = trip.PortOfLanding,
                                    ChangeEvent = CreateChangeEvent()
                                });
                                signOnsCopied++;
                            }
                        }
                        hub.UpdateProgress(100.0);
                        var result = Update(trip);
                        return Json(new
                        {
                            SignOnsCopied = signOnsCopied,
                            Result = result
                        });
                    }
                }
            }
            return new EmptyResult();
        }

        //[HttpGet]
        //public ActionResult GetCrewSharePerCrewMember(int? tripId)
        //{
        //    var trip = db.Trips.FilterDeleted().Where(x => x.Id == tripId).FirstOrDefault();
        //    if (trip != null)
        //    {
        //        db.Entry(trip).Collection(x => x.TripLines).Load();
        //        db.Entry(trip).Collection(x => x.TripDeductions).Load();
        //        db.Entry(trip).Collection(x => x.SignOns).Load();

        //        var tripLinesTotalCost = 0m;
        //        var tripDeductionsTotalCost = 0m;
        //        foreach (var tripLine in trip.TripLines.FilterDeleted())
        //        {
        //            tripLinesTotalCost += tripLine.TotalPrice;
        //        }
        //        foreach (var deduction in trip.TripDeductions)
        //        {
        //            if (deduction.TotalPrice.HasValue)
        //            {
        //                tripDeductionsTotalCost += deduction.TotalPrice.Value;
        //            }
        //        }
        //        var crewIncludingStayingAtHome = (trip.CrewIncludingStayingAtHome.HasValue && trip.CrewIncludingStayingAtHome.Value > 0) ? trip.CrewIncludingStayingAtHome.Value : 1m;
        //        var pct = trip.CrewSharePercentage.HasValue ? trip.CrewSharePercentage.Value / 100m : 0m;
        //        var crewShareMoneyCalculated = (tripLinesTotalCost - tripDeductionsTotalCost) * pct - (trip.FRADRAG2.HasValue ? trip.FRADRAG2.Value : 0m);
        //        var crewSharePerCrewMember = Math.Round(crewShareMoneyCalculated / crewIncludingStayingAtHome, 4);

        //        return Json(crewSharePerCrewMember, JsonRequestBehavior.AllowGet);
        //    }
        //    return new EmptyResult();
        //}
    }
}