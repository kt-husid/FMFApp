using BootstrapWebApplication.Controllers;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class TripHandler
    {
        BootstrapContext db = new BootstrapContext();

        public Trip Trip { get; set; }

        public TripHandler(Trip Trip)
        {
            this.Trip = Trip;
        }

        public Trip Create(Trip obj)
        {
            //var controller = new TripController();
            //controller.Create(obj);
            //return obj;
            new NotImplementedException();
            return null;
        }

        public static IEnumerable<SignOn> GetTripSignOnsExcludingStayingAtHome(Trip trip)
        {
            return trip.SignOns.FilterDeleted().Except(trip.SignOns.FilterDeleted().Where(x => !string.IsNullOrEmpty(x.JobCode) && x.JobCode.EndsWith("h")));
        }

        public static bool UpdateTripFromTripLine<T>(Trip trip, BootstrapContext db, Controller<T> controller) where T : class
        {
            db.Entry(trip).Collection(x => x.TripLines).Load();
            db.Entry(trip).Collection(x => x.TripDeductions).Load();
            trip.TotalWeight = trip.TotalWeightCalculated;
            trip.TotalAmount = trip.TotalAmountCalculated;
            trip.FRADRAG = trip.DeductionsTotal;
            //trip.CrewShareMoney = trip.CrewShareMoneyCalculated;
            //trip.TIL_MANN_TOT = trip.CrewShareMoneyCalculated + trip.MAN_STUD;
            //trip.PR_DAG = trip.CrewShareMoneyPerDay;
            return controller.Update<Trip>(trip);
        }

        public static bool UpdateTripFromTripDeduction<T>(Trip trip, BootstrapContext db, Controller<T> controller) where T : class
        {
            return UpdateTripFromTripLine(trip, db, controller);
        }

        public static Trip UpdateTripInfo(Trip trip, decimal? laborInsuranceAmountPerDay, BootstrapContext db)
        {
            var memberSignOnsWithInsuranceCount = 0;
            if (trip.SignOns == null || trip.SignOns.Count == 0)
            {
                db.Entry(trip).Collection(x => x.SignOns).Load();
            }
            if (trip.TripLines == null || trip.TripLines.Count == 0)
            {
                db.Entry(trip).Collection(x => x.TripLines).Load();
            }
            if (trip.TripDeductions == null || trip.TripDeductions.Count == 0)
            {
                db.Entry(trip).Collection(x => x.TripDeductions).Load();
            }
            foreach (var signOn in trip.SignOns)
            {
                //if (signOn.Member != null && signOn.Member.Job != null && signOn.Member.Job.HasInsurance)
                if (signOn.JobWhileSignedOn != null && signOn.JobWhileSignedOn.HasInsurance)
                {
                    memberSignOnsWithInsuranceCount++;
                }
            }
            trip.TRYG_ANT = memberSignOnsWithInsuranceCount;
            trip.LaborInsurance = laborInsuranceAmountPerDay.HasValue ? new Nullable<decimal>(laborInsuranceAmountPerDay.Value * memberSignOnsWithInsuranceCount) : (trip != null ? trip.LaborInsurance : null);
            var crew = TripHandler.GetTripSignOnsExcludingStayingAtHome(trip).Count();

            trip.CrewShareMoney = trip.CrewShareMoneyCalculated;
            trip.TIL_MANN_TOT = trip.CrewShareMoneyCalculated + trip.MAN_STUD;
            trip.PR_DAG = trip.CrewShareMoneyPerDay;

            trip.Crew = crew; //Real amount of crew member signons
            //trip.MANN_PART = trip.CrewSharePerCrewMember;
            //trip.MANN_PART_I = trip.CrewSharePerCrewMember;
            //trip.MANN_PART_IS = trip.CrewSharePerCrewMember;
            //trip.CrewIncludingStayingAtHome = crew; //Amount of crew member signons, which gets paid including those staying at home
            trip.MANNING_X = crew; // todo: figure out what _x is for
            var minWageFrom = db.MinimumWages.FilterDeleted().Where(x => x.Code == trip.From.Value.ToString("yyMM")).FirstOrDefault(); // 1502 4dg
            var minWageTo = db.MinimumWages.FilterDeleted().Where(x => x.Code == trip.To.Value.ToString("yyMM")).FirstOrDefault();     // 1504 6dg
            trip.MinimumWage = CalcMinimumWage(trip.From.Value, trip.To.Value, minWageFrom, minWageTo, trip);
            return trip;
        }

        public static decimal CalcMinimumWage(DateTime dtFrom, DateTime dtTo, MinimumWage minWageFrom, MinimumWage minWageTo, Trip trip)
        {
            var days = dtTo.Subtract(dtFrom).Days + 1;
            var minWageFromDays = dtFrom.Month == dtTo.Month ? dtTo.Subtract(dtFrom).TotalDays + 1 : new DateTime(dtFrom.Year, dtFrom.Month, DateTime.DaysInMonth(dtFrom.Year, dtFrom.Month)).Subtract(dtFrom).TotalDays + 1;
            var minWageToDays = dtFrom.Month == dtTo.Month ? 0 : dtTo.Day;
            var averageMinWage = ((minWageFrom.PerDay.Value * (decimal)minWageFromDays) + (minWageTo.PerDay.Value * (decimal)minWageToDays)) / (decimal)days;
            if (trip.PerDay.Value < averageMinWage)
            {
                return averageMinWage - trip.PerDay.Value;
            }
            return 0m;
        }
    }
}