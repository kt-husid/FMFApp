using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class PhoneViewModel : ViewModelBase
    {
        public int ParentId { get; set; }

        public Boolean IsPrimary { get; set; }

        public string Extension { get; set; }

        public Country Country { get; set; }

        public int CountryCode { get; set; }

        public int? AreaCode { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class PhoneCreateOrEditViewModel : ViewModelBase
    {
        public int? ParentId { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string PhoneNumber { get; set; }

        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }
    }
}