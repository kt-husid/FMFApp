using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberEmploymentViewModel
    {
        public int Id { get; set; }

        public int ShipId { get; set; }

        public string ShipCode { get; set; }

        public string ShipName { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string JobCode { get; set; }

        public decimal HolidayPay
        {
            get
            {
                return Math.Round(CrewSharePerCrewMember * 0.1025m, 2);
            }
        }

        public decimal CrewSharePerCrewMember { get; set; }

        public decimal? FinancialAid { get; set; }
    }
}