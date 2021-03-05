using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShippingCompanyViewModel : ViewModelBaseWithChangeEvent
    {
        public int Code { get; set; }

        public String Name { get; set; }

        public int FaxNumber { get; set; }

        //public String City { get; set; }

        //public int Postal { get; set; }

        //public String Description { get; set; }
        
        public string ContactCompanyName { get; set; }

        public string ContactPersonName { get; set; }
    }

    public class ShippingCompanyCreateOrEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public int Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public String Name { get; set; }

        //[Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        //public String Description { get; set; }

        [Display(Name = "ContactCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; }

        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; }
    }
}