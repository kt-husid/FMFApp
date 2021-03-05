using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberPaymentDetailViewModel : ViewModelBaseWithChangeEvent
    {
        public int MemberId { get; set; }

        [Display(Name = "Tax", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Tax { get; set; }
    }

    public class MemberPaymentViewModel : ViewModelBaseWithChangeEvent
    {
        public int MemberId { get; set; }

        [Display(Name = "Year", ResourceType = typeof(BootstrapResources.Resources))]
        public string Year { get; set; }

        [Display(Name = "Bank", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankId { get; set; }

        [Display(Name = "BankCode", ResourceType = typeof(BootstrapResources.Resources))]
        public int BankRegNumber { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int BankAccountNumber { get; set; }

        [Display(Name = "HolidayPay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? HolidayPay { get; set; }

        [Display(Name = "MembershipPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? MembershipPayment { get; set; }

        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Price", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Price { get; set; }

        [Display(Name = "MaternityPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal MaternityPayment { get; set; }

        [Display(Name = "LaborInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal LaborInsurance { get; set; }

        [Display(Name = "Tax", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Tax { get; set; }

        [Display(Name = "PaidOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? PaidOn { get; set; }

        [Display(Name = "PaidBy", ResourceType = typeof(BootstrapResources.Resources))]
        public string PaidById { get; set; }
    }

    public class MemberPaymentCreateViewModel : ViewModelBaseWithChangeEvent
    {
        public int? MemberId { get; set; }

        [Display(Name = "Year", ResourceType = typeof(BootstrapResources.Resources))]
        public string Year { get; set; }

        [Display(Name = "Bank", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankId { get; set; }

        [Display(Name = "RegNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int? RegNumber { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int BankAccountNumber { get; set; }

        [Display(Name = "HolidayPay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? HolidayPay { get; set; }

        [Display(Name = "MembershipPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? MembershipPayment { get; set; }

        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Price", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Price { get; set; }

        [Display(Name = "PaidOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? PaidOn { get; set; }

        [Display(Name = "PaidBy", ResourceType = typeof(BootstrapResources.Resources))]
        public string PaidById { get; set; }
    }
}