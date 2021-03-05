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
    public class ShipHandler
    {
        //BootstrapContext db = new BootstrapContext();

        public Ship Ship { get; set; }

        public ShipHandler(Ship ship)
        {
            this.Ship = ship;
        }

        public Ship Create(Ship obj)
        {
            //var controller = new ShipController();
            //controller.Create(obj);
            //return obj;
            new NotImplementedException();
            return null;
        }

        internal ShipTripYearInfo GetTripInfoForYear(int year)
        {
            var trips = Ship.Trips.FilterDeleted().Where(x=>x.From.HasValue && x.From.Value.Year == DateTime.UtcNow.Year).ToList();
            var info = new ShipTripYearInfo()
            {
                DayCount = 0,
                TripCount = trips.Count,
                LastTripDate = null
            };
            
            foreach (var trip in trips)
            {
                if (info.LastTripDate == null || trip.From.Value >= info.LastTripDate.Value)
                {
                    info.DayCount += trip.Days;
                    info.LastTripDate = trip.From;
                }
            }
            return info;
        }

    }
}