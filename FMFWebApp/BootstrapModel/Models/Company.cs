using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// Converted from P600
    /// </summary>
    [DataContract]
    public partial class Company : IHasStuffWithEntityAndChangeEvent
    {
        [DataMember]
        [Display(Name = "VAT", ResourceType = typeof(BootstrapResources.Resources))]
        public String CID { get; set; }

        [DataMember]
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public String Name { get; set; }

        [DataMember]
        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public String Description { get; set; }

        [DataMember]
        [Display(Name = "ContactCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; } //KONTAKT

        [DataMember]
        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; } //KONTAKT2
        //public Nullable<System.DateTime> IND_REG { get; set; }
        //public Nullable<System.DateTime> UD_REG { get; set; }
        //public string STATUS { get; set; }

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

        [DataMember]
        public Nullable<int> AVR_IALT { get; set; }

        [DataMember]
        public Nullable<decimal> KG_IALT { get; set; }

        [DataMember]
        public Nullable<System.DateTime> LAST_DATO { get; set; }

        [DataMember]
        public string LAST_ID { get; set; }

        //public int? PersonId { get; set; }
        //[ForeignKey("PersonId")]
        //public virtual Person Person { get; set; }
        
        //public string STUTT { get; set; }
        //public Nullable<int> MODEM { get; set; }
        //public Nullable<System.DateTime> DISK_DATO { get; set; }
        //public Nullable<int> DISK_NU { get; set; }
        //public Nullable<int> DISK_IALT { get; set; }
    
        //public virtual LIMART LIMART { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CompanyComment> Comments { get; set; }
        
        public Company()
        {
            this.Trips = new HashSet<Trip>();
            this.Comments = new HashSet<CompanyComment>();
        }
    }
}