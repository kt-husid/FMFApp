using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class FishPriceViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        public string FishSpeciesCode { get; set; } // ART

        public Nullable<System.DateTime> From { get; set; } // FRA_DATO

        public Nullable<System.DateTime> To { get; set; } // TIL_DATO

        public Nullable<decimal> Price { get; set; } // PRIS

        public String Name { get; set; }
    }

    public class FishPriceCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string FishSpeciesCode { get; set; } // ART

        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> From { get; set; } // FRA_DATO

        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> To { get; set; } // TIL_DATO

        [Display(Name = "Price", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Price { get; set; } // PRIS
    }

    [DataContract]
    public class FishPriceTemp
    {
        [DataMember]
        public string FishSpeciesCode { get; set; }
        
        [DataMember]
        public string FishSpeciesOldCode { get; set; }
        
        [DataMember]
        public decimal NewPrice { get; set; }
    }
}