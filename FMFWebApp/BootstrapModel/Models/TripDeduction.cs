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
    /// </summary>
    [DataContract]
    public partial class TripDeduction : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        //[Obsolete("Don't use this property!")]
        //public Nullable<decimal> KR { get; set; }

        // todo: remove this (is replaced by Trip.Id)
        [DataMember]
        public Nullable<int> TripNumber { get; set; } //TR_NR

        //public Nullable<int> FK_TR_HOVD_Id { get; set; }

        [DataMember]
        public string ART { get; set; }

        [DataMember]
        public Nullable<decimal> Amount { get; set; } //NOGD

        [DataMember]
        public Nullable<decimal> Price { get; set; } //PRIS

        //public string S_STUD_JN { get; set; }
        //public string M_STUD_JN { get; set; }
        //public string STATUS { get; set; }

        //[DataMember]
        //public Nullable<decimal> AlternativePrice { get; set; } //ALT_PRIS

        [DataMember]
        [NotMapped]
        public Nullable<decimal> TotalPrice { get { return Price * Amount; } } // KR

        //[DataMember]
        //public string PortOfLanding { get; set; } //AVR_STAD

        [DataMember]
        public Nullable<System.DateTime> DateOfLanding { get; set; } //AVR_DATO


        [ForeignKey("PortOfLanding")]
        [DataMember]
        public Nullable<int> PortOfLandingId { get; set; }
        public virtual Company PortOfLanding { get; set; } // P600

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

        // not needed since ship has PrimaryAddress.Postal.PostalCode
        //[ForeignKey("Postal")]
        //[DataMember]
        //public int? PostalId { get; set; }
        //public virtual Postal Postal { get; set; }

        [ForeignKey("Trip")]
        [DataMember]
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; } //TR_HOVD

        [ForeignKey("DeductionType")]
        [DataMember]
        public int? DeductionTypeId { get; set; }
        [DataMember]
        public virtual DeductionType DeductionType { get; set; }

        [NotMapped]
        public int? EntityId
        {
            get
            {
                return Ship.EntityId;
            }
            set
            {
                Ship.EntityId = value;
            }
        }

        [NotMapped]
        public Entity Entity
        {
            get
            {
                return Ship.Entity;
            }
            set
            {
                Ship.Entity = value;
            }
        }
    }
}
