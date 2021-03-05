using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MemberStatisticsViewModel
    {
        [DataMember]
        public SignOn SignOn { get; set; }

        [DataMember]
        public decimal TripsThisYear { get; set; }

        [DataMember]
        public decimal DaysThisYear { get; set; }

        [DataMember]
        public int SignOnsThisYear { get; set; }

        [DataMember]
        public int PaymentsTotal { get; set; }

        [DataMember]
        public DateTime? PaymentsYear { get; set; }

        [DataMember]
        public decimal PaymentsSum { get; set; }
    }
}