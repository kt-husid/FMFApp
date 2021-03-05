using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// Old name: LGJALD2
    /// </summary>
    [DataContract]
    public partial class MemberPayment2
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> MemberPaymentNR { get; set; } //LG_NR

        [DataMember]
        public Nullable<decimal> U1 { get; set; }

        [DataMember]
        public Nullable<decimal> U2 { get; set; }

        [DataMember]
        public Nullable<decimal> U3 { get; set; }

        [DataMember]
        public Nullable<decimal> U4 { get; set; }

        [DataMember]
        public Nullable<decimal> U5 { get; set; }

        [DataMember]
        public Nullable<decimal> U6 { get; set; }

        [DataMember]
        public Nullable<decimal> U7 { get; set; }

        [ForeignKey("Job")]
        [DataMember]
        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("Postal")]
        [DataMember]
        public int? PostalId { get; set; }
        public virtual Postal Postal { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100
    }
}
