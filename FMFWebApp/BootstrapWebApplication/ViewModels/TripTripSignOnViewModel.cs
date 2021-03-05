using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripTripSignOnViewModel : ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        public int? MemberId { get; set; }

        public int PersonNumber { get; set; }

        public string PersonFullName { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string JobCode { get; set; }

        public decimal? PART { get; set; }

        public decimal? TIL_PR_DG { get; set; }

        public decimal? TIL_PR_TUR { get; set; }

        public decimal? KR_IALT { get; set; }

        public bool HasInsurance { get; set; }

        public decimal? PART_NotAdjusted { get; set; }

        public decimal? MembershipPayment { get; set; }
    }

    public class TripSignOnCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        public int? MemberId { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string JobCode { get; set; }

        public decimal? PART { get; set; }

        public decimal? TIL_PR_DG { get; set; }

        public decimal? TIL_PR_TUR { get; set; }

        public decimal? KR_IALT { get; set; }
    }
}