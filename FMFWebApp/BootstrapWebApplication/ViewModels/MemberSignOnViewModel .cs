using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberSignOnViewModel : ViewModelBaseWithChangeEvent// ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        public Trip Trip { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int Days { get; set; }

        public int ShipId { get; set; }

        public string ShipCode { get; set; }

        public string ShipName { get; set; }

        public string JobCode { get; set; }

        public decimal? PART { get; set; }

        public decimal? TIL_PR_DG { get; set; }

        public int? EmployerNumber { get; set; }

        public decimal? KR_IALT { get; set; }

        public decimal KR_I { get; set; }

        public decimal KR_FF { get; set; }

        public decimal? MembershipPayment { get; set; }

        //public decimal FinancialAid { get; set; }

        public string TripNumber { get; set; }
    }
}