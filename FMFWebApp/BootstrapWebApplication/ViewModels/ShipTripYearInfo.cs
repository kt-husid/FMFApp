using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class ShipTripYearInfo
    {
        [DataMember]
        public double DayCount { get; set; }

        [DataMember]
        public int TripCount { get; set; }

        [DataMember]
        public DateTime? LastTripDate { get; set; }
    }
}