using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class P400 : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> NR { get; set; }

        [DataMember]
        public string FNAVN { get; set; }

        [DataMember]
        public string ENAVN { get; set; }

        [DataMember]
        public Nullable<int> POST { get; set; }

        [DataMember]
        public Nullable<decimal> KR { get; set; }

        [DataMember]
        public string ART { get; set; }

        [DataMember]
        public Nullable<System.DateTime> UD_DATO { get; set; } // + UD_HH + UD_MM

        [DataMember]
        public string UD_ID { get; set; }

        [ForeignKey("BankAccount")]
        [DataMember]
        public int? BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; } //REG + KONTO

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100
    }
}
