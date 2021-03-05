using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class AddressViewModel : ViewModelBaseWithChangeEvent
    {
        public int ParentId { get; set; }

        [Display(Name = "CareOf", ResourceType = typeof(BootstrapResources.Resources))]
        public string CareOf { get; set; }

        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "AddressLine1", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine2 { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsActive { get; set; }

        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }

        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        public int PostalCode { get; set; }

        [Display(Name = "City", ResourceType = typeof(BootstrapResources.Resources))]
        public string City { get; set; }

        [Display(Name = "StateProvinceRegion", ResourceType = typeof(BootstrapResources.Resources))]
        public string StateProvinceRegion { get; set; }

        [Display(Name = "CountryCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string CountryCode { get; set; }

        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        public string CountryName { get; set; }
    }

    public class AddressCreateOrEditViewModel : ViewModelBase
    {
        public int? ParentId { get; set; }

        [Display(Name = "CareOf", ResourceType = typeof(BootstrapResources.Resources))]
        public string CareOf { get; set; }

        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "AddressLine1", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine2 { get; set; }

        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PostalId { get; set; }

        [Display(Name = "StateProvinceRegion", ResourceType = typeof(BootstrapResources.Resources))]
        public string StateProvinceRegion { get; set; }

        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        public int CountryId { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsActive { get; set; }

        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }
        
        public int PostalCode { get; set; }

        public string CountryCode { get; set; }

        public Postal Postal { get; set; }

        public Country Country { get; set; }
    }
}