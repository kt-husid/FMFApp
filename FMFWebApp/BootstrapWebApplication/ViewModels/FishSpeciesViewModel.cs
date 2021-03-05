using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class FishSpeciesViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string OldCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OldName { get; set; }
        public string RAD { get; set; }
    }

    public class FishSpeciesCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }

        //[Display(Name = "RAD", ResourceType = typeof(BootstrapResources.Resources))]
        public string RAD { get; set; }

        [Display(Name = "Number", ResourceType = typeof(BootstrapResources.Resources))]
        public int? FishSpeciesNumber { get; set; }
    }
}