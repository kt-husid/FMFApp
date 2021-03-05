using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class PostalViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
    }

    public class PostalCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public int Code { get; set; }

        [Display(Name = "City", ResourceType = typeof(BootstrapResources.Resources))]
        public string CityName { get; set; }

        [Display(Name = "CountryCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string CountryCode { get; set; }

        public int CountryId { get; set; }
    }
}