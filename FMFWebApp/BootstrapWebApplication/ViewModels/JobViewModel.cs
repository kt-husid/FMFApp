using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class JobViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Organization { get; set; }

        public decimal? PART { get; set; }

        public decimal? TIL_DG { get; set; }

        public decimal? TIL_TUR { get; set; }

        public bool HasInsurance { get; set; }
    }

    public class JobCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }

        //[Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        //public string Description2 { get; set; }

        [Display(Name = "Organization", ResourceType = typeof(BootstrapResources.Resources))]
        public string Organization { get; set; }

        //[Display(Name = "PerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TIL_DG { get; set; }

        //[Display(Name = "PerTrip", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TIL_TUR { get; set; }

        [Display(Name = "HasInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public bool HasInsurance { get; set; }
    }
}