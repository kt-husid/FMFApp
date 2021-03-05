using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// LSLAG - P019.FRM
    /// </summary>
    [DataContract]
    public partial class MemberType : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        //[DataMember]
        //public string STUTT { get; set; }

        [DataMember]
        public string BREV_UT { get; set; }

        //[DataMember]
        //public string FLYTAST { get; set; }

        //[DataMember]
        //public string DLISTA { get; set; }

        //[DataMember]
        //public string BLISTA { get; set; }

        //[DataMember]
        //public string ARS_LISTI { get; set; }
    
        //public virtual ICollection<G100> G100 { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        //public virtual ICollection<SB> SBs { get; set; }

        public MemberType()
        {
            //this.G100 = new HashSet<G100>();
            this.Members = new HashSet<Member>();
            //this.SBs = new HashSet<SB>();
        }
    }
}
