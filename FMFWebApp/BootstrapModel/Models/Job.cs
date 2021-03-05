using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// old name: STARV
    /// </summary>
    [DataContract]
    public partial class Job : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int OldId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Description2 { get; set; }

        [DataMember]
        public string STUTT { get; set; }

        [DataMember]
        [NotMapped]
        public string TXT5
        {
            get
            {
                if (Description.Length >= 4)
                {
                    return Description.Substring(0, 4).ToUpperFirst();
                }
                return null;
            }
        }

        [DataMember]
        public Nullable<int> Amount { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TAL_DATO { get; set; }

        //public string TAL_HH { get; set; }
        //public string TAL_MM { get; set; }

        [DataMember]
        public string TAL_ID { get; set; }

        [DataMember]
        public Nullable<decimal> PART { get; set; }

        [DataMember]
        public Nullable<decimal> TIL_DG { get; set; }

        [DataMember]
        public Nullable<decimal> TIL_TUR { get; set; }

        [DataMember]
        public string Organization { get; set; } // FELAG
    
        //public virtual ICollection<G100> G100 { get; set; } // OLD DATA
        public virtual ICollection<Member> Members { get; set; }
        //public virtual ICollection<SB> SBs { get; set; }
        //public virtual ICollection<STXSK> STXSKs { get; set; }
        //public virtual ICollection<TURHEAD> TURHEADs { get; set; }

        public Job()
        {
            //this.G100 = new HashSet<G100>();
            this.Members = new HashSet<Member>();
            //this.SBs = new HashSet<SB>();
            //this.STXSKs = new HashSet<STXSK>();
            //this.TURHEADs = new HashSet<TURHEAD>();
        }

        public bool HasInsurance { get; set; } // ALKA (J ella N)
    }
}
