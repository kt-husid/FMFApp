using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class HolidayPay
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        //public Nullable<int> NR { get; set; }

        [DataMember]
        public string Year { get; set; } //AR

        [DataMember]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        public Nullable<decimal> TRSP { get; set; }

        [DataMember]
        public Nullable<System.DateTime> FRT_DATO { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TRS_DATO { get; set; }

        [DataMember]
        public Nullable<decimal> LO_KR_A { get; set; }

        [DataMember]
        public Nullable<decimal> LO_KR_F { get; set; }

        [DataMember]
        public Nullable<decimal> LO_KR_FRT { get; set; }

        [DataMember]
        public Nullable<System.DateTime> LO_DATO { get; set; }

        [DataMember]
        public Nullable<int> DG_AR { get; set; }

        [DataMember]
        public Nullable<int> TU_AR { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DATO_AR { get; set; }

        [DataMember]
        public string ID_AR { get; set; }

        [DataMember]
        public Nullable<decimal> SAML_KR { get; set; }

        [DataMember]
        public Nullable<decimal> SAML_KR2 { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}
