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
    public partial class AidType : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string AidTypeCode { get; set; } // ID

        [DataMember]
        public string Name { get; set; } // NAVN

        [DataMember]
        public string Description { get; set; } // STUTT

        //public string ALIAS { get; set; }

        [DataMember]
        public int? LINK { get; set; }
        //public string RAD { get; set; }

        [DataMember]
        public string Type { get; set; } // TYPA

        public virtual ICollection<AidPrice> AidPrices { get; set; }
        //public virtual ICollection<STXFI> STXFIs { get; set; }

        public AidType()
        {
            this.AidPrices = new HashSet<AidPrice>();
            //this.STXFIs = new HashSet<STXFI>();
        }
    }
}