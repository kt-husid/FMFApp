using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class DeductionTypeViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }
    }

    public class DeductionTypeCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }

        [Display(Name = "Type", ResourceType = typeof(BootstrapResources.Resources))]
        public int? Type { get; set; }
    }
}