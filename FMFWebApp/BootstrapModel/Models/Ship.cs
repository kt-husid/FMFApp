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
    //ID,Code,NAVN,ADR1,POSTNR,TLF,FAX,KONTAKT,KONTAKT2,IND_REG,UD_REG,STATUS,RET_DATO,RET_HH,RET_MM,RET_ID,NAVN5,ILUL,LN_IALT,SK_TYPA,REDERI,TONNAGE,HK,TXT_LN,TXT_DATO,BURT_DG,TUR_IALT,LAST_DATO,TYPA,STRIKA,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,STAT,AV_IALT,KG_IALT,DG_IALT,ALT_ID,GRUPPE,ETI_DATO,ETI_HH,ETI_MM,ETI_ID,ARB_NR
    [DataContract]
    public partial class Ship : IHasStuffWithEntityAndChangeEvent //, IHasEntityWithMore
    {
        [DataMember]
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; } //NAVN
        
        //public string AddressLine1 { get; set; } //ADR1
        //public Nullable<int> POSTNR { get; set; } //POSTNR
        //public Nullable<int> PhoneNumber { get; set; } //TLF
        //public Nullable<int> FAX { get; set; }

        [DataMember]
        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; } //KONTAKT

        [DataMember]
        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; } //KONTAKT2

        [DataMember]
        public Nullable<System.DateTime> IND_REG { get; set; }

        [DataMember]
        public Nullable<System.DateTime> UD_REG { get; set; }

        [DataMember]
        [Display(Name = "Status", ResourceType = typeof(BootstrapResources.Resources))]
        public string Status { get; set; }

        //[DataMember]
        //[NotMapped]
        //public string StatusText
        //{
        //    get
        //    {
        //        return null;
        //        //if (String.IsNullOrEmpty(Status))
        //        //{
        //        //    //return @BootstrapResources.Resources.EverythingOK;
        //        //}
        //        //else
        //        //{
        //        //    return @BootstrapResources.Resources.EverythingNOTOK;
        //        //}
        //    }
        //}
        
        [NotMapped]
        [DataMember]
        public string NAVN5
        {
            get
            {
                if (!String.IsNullOrEmpty(Name) && Name.Length >= 5)
                {
                    return Name.Substring(0, 5).ToUpperFirst();
                }
                return null;
            }
        }

        //public string ILUL { get; set; }

        //[DataMember]
        //public Nullable<int> LN_IALT { get; set; }

        [DataMember]
        public string SK_TYPA { get; set; }

        //public Nullable<int> FK_REIDARI_Id { get; set; }

        [DataMember]
        [Display(Name = "Tonnage", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Tonnage { get; set; }

        [DataMember]
        [Display(Name = "HK", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> HK { get; set; }

        //[DataMember]
        //public Nullable<int> TXT_LN { get; set; } // amount of comments
        //[DataMember]
        //public Nullable<System.DateTime> TXT_DATO { get; set; } // comment date

        //[DataMember]
        //public Nullable<int> BURT_DG { get; set; }

        //[DataMember]
        //public Nullable<int> TUR_IALT { get; set; }

        //[DataMember]
        //public Nullable<System.DateTime> LAST_DATO { get; set; }

        //public string TYPA { get; set; }
        //public Nullable<int> FK_TYPA_Id { get; set; }
        //public string STRIKA { get; set; }
        //public string STAT { get; set; }
        //public Nullable<int> FK_TURHEAD_Id { get; set; }

        [DataMember]
        public Nullable<decimal> KG_IALT { get; set; }

        // use GET: /api/Ship/GetTripInfoForYear -> json.DayCount
        //[DataMember]
        //public Nullable<decimal> DG_IALT { get; set; }
        // use GET: /api/Ship/GetTripInfoForYear -> json.TripCount
        //[DataMember]
        //public Nullable<int> AV_IALT { get; set; }

        [DataMember]
        public string ALT_ID { get; set; }

        [DataMember]
        public Nullable<int> Group { get; set; } //GRUPPE

        [DataMember]
        public Nullable<System.DateTime> ETI_DATO { get; set; } // + ETI_HH + ETI_MM

        [DataMember]
        public string ETI_ID { get; set; }

        [Obsolete("Use instead ShippingCompany.EmployerNumber !!!")]
        [DataMember]
        [Display(Name = "ShippingCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        //public virtual LIMART LIMART { get; set; }
        //public virtual TURHEAD TURHEAD { get; set; }
        [ForeignKey("ShippingCompany")]
        [DataMember]
        public int? ShippingCompanyId { get; set; }
        [DataMember]
        [Display(Name = "ShippingCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual ShippingCompany ShippingCompany { get; set; }

        [ForeignKey("ShipType")]
        [DataMember]
        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public int? ShipTypeId { get; set; }
        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual ShipType ShipType { get; set; } //TYPA1

        public virtual ICollection<ShipComment> Comments { get; set; }

        [InverseProperty("Ship")] 
        public virtual ICollection<Trip> Trips { get; set; }
    }
}