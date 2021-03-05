using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// old name: UM_LINE
    /// </summary>
    [DataContract]
    public partial class MemberFinancialAid : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> From { get; set; } //FRA_DATO

        [DataMember]
        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> To { get; set; } //TIL_DATO

        [Obsolete("Use DaysCalculated instead")]
        [DataMember]
        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> Days { get; set; } //DG

        [NotMapped]
        [DataMember]
        public int DaysCalculated
        {
            get
            {
                if (To.HasValue && From.HasValue)
                {
                    return (int)To.Value.Subtract(From.Value).TotalDays + 1;
                }
                return 0;
            }
        } 

        [DataMember]
        [Display(Name = "SocialServicePayment", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> SocialServicePayment { get; set; } //ALM_UPPH

        [DataMember]
        [Display(Name = "PaymentPerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> PaymentPerDay { get; set; } //PR_DG

        [Obsolete("Use instead OurPaymentCalculated")]
        [DataMember]
        [Display(Name = "OurPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> OurPayment { get; set; } //FF_IALT

        [DataMember]
        [NotMapped]
        [Display(Name = "OurPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> OurPaymentCalculated //FF_IALT
        {
            get
            {
                return DaysCalculated * PaymentPerDay - SocialServicePayment;
            }
        }

        [DataMember]
        [Display(Name = "PrintedOn", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> PrintedOn { get; set; } //UD_DATO + UD_HH + UD_MM

        [DataMember]
        [Display(Name = "PrintedBy", ResourceType = typeof(BootstrapResources.Resources))]
        public string PrintedById { get; set; } //UD_ID

        [DataMember]
        [Display(Name = "PrintedAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> PrintedAmount { get; set; } //UD_ANT

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        [DataMember]
        public virtual Member Member { get; set; } //P100

        [ForeignKey("BankAccount")]
        [DataMember]
        public int? BankAccountId { get; set; }
        [DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual BankAccount BankAccount { get; set; } // REG + KONTO
    }
}
