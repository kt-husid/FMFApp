using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MemberTripInfoForYearOverviewViewModel
    {
        [DataMember]
        public Member Member { get; set; }

        [DataMember]
        public ICollection<MemberTripInfoForYearTotal> MemberTripInfoForYearTotals { get; set; }

        public MemberTripInfoForYearOverviewViewModel(Member member)
        {
            this.Member = member;
            MemberTripInfoForYearTotals = new List<MemberTripInfoForYearTotal>();
        }
    }

    public class MemberTripInfoForYearTotal
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Trips { get; set; }
        public DateTime? LastDate { get; set; }
        public string ShipCode { get; set; }
        public decimal Days { get; set; }
    }
}