using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShipCreateOrEditViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string ShipCode { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }

        [Display(Name = "ContactCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; }

        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; }

        //[Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        //public string ShippingCompanyName { get; set; }

        [Display(Name = "ShippingCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public int? ShippingCompanyCode { get; set; }

        [Display(Name = "ShippingCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public int? ShippingCompanyId { get; set; }

        [Display(Name = "ShippingCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public ShippingCompany ShippingCompany { get; set; }

        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public string ShipTypeCode { get; set; }

        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public int ShipTypeId { get; set; }
        
        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public ShipType ShipType { get; set; }

        [Display(Name = "Tonnage", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Tonnage { get; set; }

        [Display(Name = "HK", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> HK { get; set; }

        [Display(Name = "Status", ResourceType = typeof(BootstrapResources.Resources))]
        public string Status { get; set; }
    }
}