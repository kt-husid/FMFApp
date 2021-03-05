using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class TLF_IN
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> NR { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DATO { get; set; }
        //public string HH { get; set; }
        //public string MM { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
