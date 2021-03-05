using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class HolidayPay_HOVD : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "EmployerNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        [DataMember]
        [Display(Name = "TransactionDate", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> TransferDate { get; set; } // FLYT_DATO

        //[DataMember]
        //public string STATUS { get; set; }

        [DataMember]
        [Display(Name = "Amount", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Amount { get; set; } // UPPH_ELE

        //[DataMember]
        //public Nullable<int> PERS_NR { get; set; }

        [DataMember]
        [Display(Name = "Kind", ResourceType = typeof(BootstrapResources.Resources))]
        public string ART { get; set; }

        [DataMember]
        public Nullable<System.DateTime> KOYR_DATO { get; set; }

        [DataMember]
        public Nullable<int> FRT_NR { get; set; }

        [DataMember]
        public Nullable<int> KONTO { get; set; }

        [DataMember]
        public Nullable<int> REG_NR { get; set; }

        [DataMember]
        public Nullable<int> KONTO_FF { get; set; }

        [DataMember]
        public Nullable<int> REG_NR_FF { get; set; }

        //[DataMember]
        //public Nullable<int> FRT_TOT_NR { get; set; }

        [DataMember]
        public string PLUS { get; set; }

        [DataMember]
        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> TR_NR { get; set; }

        [DataMember]
        public Nullable<int> X { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100

        //[ForeignKey("HolidayPay")]
        //[DataMember]
        //public int? HolidayPayId { get; set; }
        //public virtual HolidayPay HolidayPay { get; set; }

        //[ForeignKey("Trip")]
        //[DataMember]
        //public int? TripId { get; set; }
        //public virtual Trip Trip { get; set; }

        [ForeignKey("BankAccount")]
        [DataMember]
        public int? BankAccountId { get; set; }
        [DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public BankAccount BankAccount { get; set; }

        [ForeignKey("BankAccountFF")]
        [DataMember]
        public int? BankAccountFFId { get; set; }
        [DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public BankAccount BankAccountFF { get; set; }
    }
}
