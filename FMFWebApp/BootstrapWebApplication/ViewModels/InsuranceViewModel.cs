using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class InsuranceYearsViewModel : ViewModelBase
    {
        public string Year { get; set; }

        public Decimal Total { get; set; }
    }

    public class InsuranceViewModel : ViewModelBase
    {
        public int TripId { get; set; }

        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int TripNumber { get; set; }

        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string TripNumberStr { get; set; }

        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? From { get; set; }

        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? To { get; set; }

        [Display(Name = "Ship", ResourceType = typeof(BootstrapResources.Resources))]
        public string ShipCode { get; set; }

        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public string JobCode { get; set; }

        [Display(Name = "Share", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal PART { get; set; }

        [Display(Name = "PerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal PerDay { get; set; }

        [Display(Name = "EmployerNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int EmployerNumber { get; set; }

        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public double Days { get; set; }

        [Display(Name = "Total", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal Total
        {
            get
            {
                return PerDay * (decimal)Days;
            }
        }

        [Display(Name = "MembershipPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal MembershipPayment { get; set; }
        
    }
}