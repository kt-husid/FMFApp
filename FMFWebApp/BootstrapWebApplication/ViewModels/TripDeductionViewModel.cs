using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripDeductionViewModel : ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        public string DeductionTypeCode { get; set; }

        public decimal? Amount { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedByUserIdCode { get; set; }
    }

    public class TripDeductionCreateOrEditViewModel : ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        [Display(Name = "DeductionType", ResourceType = typeof(BootstrapResources.Resources))]
        public string DeductionTypeCode { get; set; }

        public int? DeductionTypeId { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Amount { get; set; }

        [Display(Name = "UnitPrice", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "TotalPrice", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TotalPrice { get; set; }
    }
}