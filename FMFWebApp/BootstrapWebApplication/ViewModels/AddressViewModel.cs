//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace BootstrapWebApplication.Models
//{
//    public class AddressViewModel : ViewModelBaseWithChangeEvent
//    {
//        [Display(Name = "CareOf", ResourceType = typeof(BootstrapResources.Resources))]
//        public string CareOf { get; set; }

//        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
//        [Display(Name = "AddressLine1", ResourceType = typeof(BootstrapResources.Resources))]
//        public string AddressLine1 { get; set; }

//        [Display(Name = "AddressLine2", ResourceType = typeof(BootstrapResources.Resources))]
//        public string AddressLine2 { get; set; }

//        [Display(Name = "IsActive", ResourceType = typeof(BootstrapResources.Resources))]
//        public bool IsActive { get; set; }

//        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
//        public bool IsPrimary { get; set; }

//        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
//        public int? PostalId { get; set; }
//        //public virtual Postal Postal { get; set; }

//        [Display(Name = "StateProvinceRegion", ResourceType = typeof(BootstrapResources.Resources))]
//        public string StateProvinceRegion { get; set; }

//        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
//        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
//        //[ForeignKey("Country")]
//        public int CountryId { get; set; }
//        //public virtual Country Country { get; set; }

//        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
//        //[ForeignKey("Entity")]
//        public int? EntityId { get; set; }
//        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
//        //public virtual Entity Entity { get; set; }

//    }
//}