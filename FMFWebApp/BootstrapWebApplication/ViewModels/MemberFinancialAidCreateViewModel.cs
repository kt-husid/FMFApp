using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberFinancialAidCreateViewModel : ViewModelBaseWithChangeEvent
    {
        public int? MemberId { get; set; }
        
        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? From { get; set; }

        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? To { get; set; }

        //[Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        //public int? Days { get; set; }

        [Display(Name = "SocialServicePayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? SocialServicePayment { get; set; }

        [Display(Name = "PaymentPerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? PaymentPerDay { get; set; }

        //[Display(Name = "OurPayment", ResourceType = typeof(BootstrapResources.Resources))]
        //public decimal? OurPayment { get; set; }

        [Display(Name = "BankCode", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankId { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int BankAccountNumber { get; set; }

        [Display(Name = "PrintedOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? PrintedOn { get; set; }

        [Display(Name = "PrintedBy", ResourceType = typeof(BootstrapResources.Resources))]
        public string PrintedBy { get; set; }

        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public int? Days { get; set; }
    }
}