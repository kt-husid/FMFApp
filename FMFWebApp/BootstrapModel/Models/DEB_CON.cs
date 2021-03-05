using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class DEB_CON : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> NR { get; set; }

        [DataMember]
        public Nullable<int> NR_DEB { get; set; }

        [DataMember]
        public Nullable<decimal> SALDO { get; set; }

        [ForeignKey("Job")]
        [DataMember]
        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("Postal")]
        [DataMember]
        public int? PostalId { get; set; }
        public virtual Postal Postal { get; set; }

        [ForeignKey("MemberType")]
        [DataMember]
        public int? MemberTypeId { get; set; }
        public virtual MemberType MemberType { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100
    }
}
