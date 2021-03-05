using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{    
    /// <summary>
    /// P300
    /// ID,NAVN,ADR1,ADR2,POSTNR,TLF,FAX,KONTAKT,KONTAKT2,IND_REG,UD_REG,STATUS,RET_DATO,RET_HH,RET_MM,RET_ID,NAVN5,ILUL,LN_IALT,TXT_LN,TXT_DATO,AVR_IALT,KG_IALT,LAST_DATO,LAST_ID,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,STUTT,MODEM,DISK_DATO,DISK_NU,DISK_IALT,PREFIX,SKIB_IALT,ALT_ID,ETI_DATO,ETI_HH,ETI_MM,ETI_ID,STAT,ARB_NR,FRT_LON,FRT_LON_DATO,FRT_LON_NU,A_REG,A_KTO
    /// </summary>
    [DataContract]
    public partial class ShippingCompany : IHasStuffWithEntityAndChangeEvent// IHasChangeEvent, IHasEntity
    {
        //[Key]
        //[DataMember]
        //public int Id { get; set; }

        //[ForeignKey("Entity")]
        //[DataMember]
        //public int? EntityId { get; set; }
        //[DataMember]
        //public virtual Entity Entity { get; set; }

        [DataMember]
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public int Code { get; set; }

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; } //NAVN

        //[DataMember]
        //public string AddressLine1 { get; set; } //ADR1

        //[DataMember]
        //public string AddressLine2 { get; set; } //ADR2

        //public Nullable<int> POSTNR { get; set; }
        //public Nullable<int> FK_LIMART_Id { get; set; }
        //public Nullable<int> PhoneNumber { get; set; } //TLF

        [DataMember]
        public Nullable<int> FaxNumber { get; set; } //FAX

        [DataMember]
        [Display(Name = "ContactCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; } //KONTAKT

        [DataMember]
        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; } //KONTAKT2

        //public Nullable<System.DateTime> IND_REG { get; set; }
        //public Nullable<System.DateTime> UD_REG { get; set; }

        //public string STATUS { get; set; }
        //public string NAVN5 { get; set; }

        [NotMapped()]
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
        //public Nullable<int> LN_IALT { get; set; }
        //public Nullable<int> TXT_LN { get; set; }
        //public Nullable<System.DateTime> TXT_DATO { get; set; }
        //public Nullable<int> AVR_IALT { get; set; }

        [DataMember]
        public Nullable<decimal> KG_IALT { get; set; }

        //public Nullable<System.DateTime> LAST_DATO { get; set; }
        //public string LAST_ID { get; set; }
        //public string STUTT { get; set; }
        //public Nullable<int> MODEM { get; set; }
        //public Nullable<System.DateTime> DISK_DATO { get; set; }
        //public Nullable<int> DISK_NU { get; set; }
        //public Nullable<int> DISK_IALT { get; set; }

        [DataMember]
        public string PREFIX { get; set; }

        [DataMember]
        public Nullable<int> SKIB_IALT { get; set; }
        
        [DataMember]
        public Nullable<int> ALT_ID { get; set; }

        public Nullable<System.DateTime> ETI_DATO { get; set; }
        public string ETI_ID { get; set; }
        //public int STAT { get { if (Stat != null) { return Stat.Id; } } }
        //public Nullable<int> FK_TURHEAD_Id { get; set; }

        [DataMember]
        [Display(Name = "EmployerNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        [DataMember]
        public Nullable<decimal> FRT_LON { get; set; }

        [DataMember]
        public Nullable<System.DateTime> FRT_LON_DATO { get; set; }

        [DataMember]
        public Nullable<decimal> FRT_LON_NU { get; set; }

        [DataMember]
        public Nullable<int> A_REG { get; set; }

        [DataMember]
        public Nullable<int> A_KTO { get; set; }

        //[ForeignKey("Status")]
        //[DataMember]
        //public int? StatusId { get; set; }
        //public virtual Status Status { get; set; }

        //[ForeignKey("PostNr")]
        //public Nullable<int> PostNrId { get; set; }
        //public virtual PostNr PostNr { get; set; }

        [ForeignKey("Status")]
        [DataMember]
        public int? StatusId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Status Status { get; set; }


        //public virtual ICollection<LO_HOVD> LO_HOVDs { get; set; }
        public virtual ICollection<Ship> Ships { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }

        public ShippingCompany()
        {
            //this.LO_HOVDs = new HashSet<LO_HOVD>();
            this.Ships = new HashSet<Ship>();
            this.Trips = new HashSet<Trip>();
        }
    }
}