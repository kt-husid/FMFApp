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
    [DataContract]
    public partial class TripLine : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        //[Obsolete("Don't use this property!")]
        //public decimal Price { get; set; } //PRIS
        //[Obsolete("Don't use this property!")]
        //public string SK_ID { get; set; } 

        [Obsolete("Use Trip.TripId instead")]
        // todo: remove this (is replaced by Trip.TripId)
        [DataMember]
        public Nullable<int> TripNumber { get; set; } //TR_NR

        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        // Amount is Weight in KG
        public decimal Amount { get; set; } //NOGD

        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public decimal UnitPrice { get; set; } //PRIS

        //public string S_STUD_JN { get; set; } //EMPTY col in DB
        //public string M_STUD_JN { get; set; } //EMPTY col in DB
        //public string STATUS { get; set; } //EMPTY col in DB

        [DataMember]
        //todo: alternative price used?
        public Nullable<decimal> AlternativePrice { get; set; } //ALT_PRIS

        // todo: Remove this property (KR) but make sure TotalPrice is right!
        [DataMember]
        [Obsolete("Use TotalPrice instead")]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public decimal KR { get; set; }

        [DataMember]
        [NotMapped]
        public decimal TotalPrice // KR
        {
            get
            {
                return UnitPrice * Amount;
            }
        }

        [DataMember]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public string PortOfLanding { get; set; } //AVR_STAD

        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "DateOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        [DataType(DataType.DateTime)]
        public System.DateTime DateOfLanding { get; set; } //AVR_DATO 

        [DataMember]
        public string RAD { get; set; }

        [DataMember]
        public string Year { get; set; } //AR

        [DataMember]
        [NotMapped]
        public string ShipTypeCode // SK_ID
        {
            get
            {
                if (Ship != null && Ship.Code != null)
                {
                    return Ship.ShipType.Code;
                }
                return null;
            }
        }

        [DataMember]
        public Nullable<decimal> STUD_M { get; set; }

        [DataMember]
        public Nullable<decimal> STUD_S { get; set; }

        [ForeignKey("ShippingCompany")]
        [DataMember]
        public int? ShippingCompanyId { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }

        //[ForeignKey("Ship")]
        //[DataMember]
        //public int? ShipId { get; set; }
        //public virtual Ship Ship { get; set; } // SKIP

        [NotMapped]
        [DataMember]
        public Ship Ship
        {
            get
            {
                if (Trip != null)
                {
                    return Trip.Ship;
                }
                return null;
            }
        }

        [ForeignKey("Trip")]
        [DataMember]
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }

        [ForeignKey("FishSpecies")]
        [DataMember]
        public int? FishSpeciesId { get; set; }
        [DataMember]
        [Display(Name = "FishSpecies", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual FishSpecies FishSpecies { get; set; }

        [ForeignKey("ShipType")]
        [DataMember]
        public int? ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [ForeignKey("Postal")]
        [DataMember]
        public int? PostalId { get; set; }
        public virtual Postal Postal { get; set; }

        [ForeignKey("Company")]
        [DataMember]
        public int? CompanyId { get; set; }
        [Display(Name = "Company", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public virtual Company Company { get; set; }

        //public int ShippingCompanyCode { get; set; }

        //public string Code { get; set; }

        //public string FishSpeciesCode { get; set; }

        //public int? PostalCode { get; set; }

        //public string CompanyCode { get; set; }

        //public int? TypaCode { get; set; }

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
