using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class CompanyViewModel : ViewModelBaseWithChangeEvent
    {
        //public String CID { get; set; }

        public string Code { get; set; }

        public String Name { get; set; }

        public String City { get; set; }

        public int Postal { get; set; }

        public String Description { get; set; }
        
        public string ContactCompanyName { get; set; }

        public string ContactPersonName { get; set; }
    }

    public class CompanyCreateOrEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "VAT", ResourceType = typeof(BootstrapResources.Resources))]
        public String CID { get; set; }

        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public String Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public String Description { get; set; }

        [Display(Name = "ContactCompany", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactCompanyName { get; set; }

        [Display(Name = "ContactPerson", ResourceType = typeof(BootstrapResources.Resources))]
        public string ContactPersonName { get; set; }
    }
}