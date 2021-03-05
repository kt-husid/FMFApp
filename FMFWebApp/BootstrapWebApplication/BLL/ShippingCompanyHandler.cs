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
    public class ShippingCompanyHandler
    {
        public ShippingCompany ShippingCompany { get; set; }

        public ShippingCompanyHandler()
        {

        }

        public ShippingCompanyHandler(ShippingCompany shippingCompany)
            : this()
        {
            this.ShippingCompany = shippingCompany;
        }

        internal ShipTripYearInfo GetTripInfoForYear(int year)
        {
            var ships = ShippingCompany.Ships;
            var info = new ShipTripYearInfo()
            {
                DayCount = 0,
                TripCount = 0,
                LastTripDate = null
            };

            foreach (var ship in ships)
            {
                var trips = ship.Trips.FilterDeleted().Where(x => x.From.HasValue && x.From.Value.Year == DateTime.UtcNow.Year).ToList();
                info.TripCount += trips.Count;
                foreach (var trip in trips)
                {
                    if (info.LastTripDate == null || trip.From.Value >= info.LastTripDate.Value)
                    {
                        info.DayCount += trip.Days;
                        info.LastTripDate = trip.From;
                    }
                }
            }
            return info;
        }

        public ShippingCompany Create(BootstrapContext db, int code, string name, string contactCompanyName, string contactPersonName, ChangeEvent ce)
        {
            var entity = new Entity();
            db.Entities.Add(entity);
            // Convert the ViewModel to DB Object (Model)
            var dbObj = new ShippingCompany()
            {
                //CID = o.CID,
                Code = code,
                EmployerNumber = code,
                Name = name,
                //Description = o.Description,
                ContactCompanyName = contactCompanyName,
                ContactPersonName = contactPersonName,
                Entity = entity,
                ChangeEvent = ce
            };
            return dbObj;
        }
    }
}