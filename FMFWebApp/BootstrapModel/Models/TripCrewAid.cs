using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// Old name: TR_STUD_M
    /// </summary>
    [DataContract]
    public partial class TripCrewAid : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> TripNumber { get; set; } //TR_NR

        [DataMember]
        public string ART { get; set; } // 

        [DataMember]
        public Nullable<decimal> Amount { get; set; } //NOGD

        [DataMember]
        public Nullable<decimal> Price { get; set; } //PRIS

        //[DataMember]
        //public string S_STUD_JN { get; set; }

        //[DataMember]
        //public string M_STUD_JN { get; set; }

        //[DataMember]
        //public string STATUS { get; set; }

        [DataMember]
        public Nullable<decimal> AlternativePrice { get; set; } //ALT_PRIS

        [DataMember]
        public Nullable<decimal> KR { get; set; }

        [DataMember]
        public string PortOfLanding { get; set; } //AVR_STAD

        [DataMember]
        public Nullable<System.DateTime> DateOfLanding { get; set; } //AVR_DATO

        [ForeignKey("Company")]
        [DataMember]
        public Nullable<int> CompanyId { get; set; }
        public virtual Company Company { get; set; } // P600

        [ForeignKey("ShippingCompany")] //ID_REDERI
        [DataMember]
        public Nullable<int> ShippingCompanyId { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; } //REIDARI

        [ForeignKey("Ship")] //ID_SKIP
        [DataMember]
        public Nullable<int> ShipId { get; set; }
        public virtual Ship Ship { get; set; } // SKIB1

        [ForeignKey("ShipType")]
        [DataMember]
        public Nullable<int> ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [ForeignKey("Postal")]
        [DataMember]
        public int? PostalId { get; set; }
        public virtual Postal Postal { get; set; }

        [ForeignKey("Trip")]
        [DataMember]
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; } //TR_HOVD
    }
}
