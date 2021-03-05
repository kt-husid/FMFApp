using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class CountryViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CountryCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "CountryCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }
    }
}