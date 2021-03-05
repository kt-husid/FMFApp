using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripTripLineCreateViewModel : ViewModelBaseWithChangeEvent
    {
        [DataMember]
        [Display(Name = "Trip", ResourceType = typeof(BootstrapResources.Resources))]
        public int? TripId { get; set; }

        [Display(Name = "Trip", ResourceType = typeof(BootstrapResources.Resources))]
        public Trip Trip { get; set; }

        [Display(Name = "FishSpecies", ResourceType = typeof(BootstrapResources.Resources))]
        public string FishSpeciesCode { get; set; }

        [DataMember]
        [Display(Name = "FishSpecies", ResourceType = typeof(BootstrapResources.Resources))]
        public int? FishSpeciesId { get; set; }

        [DataMember]
        [Display(Name = "FishSpecies", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual FishSpecies FishSpecies { get; set; }

        [DataMember]
        [Display(Name = "Amount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal Amount { get; set; } //NOGD

        [DataMember]
        [Display(Name = "UnitPrice", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal UnitPrice { get; set; } //PRIS

        [DataMember]
        [Display(Name = "TotalPrice", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal TotalPrice { get; set; }

        //[DataMember]
        //[Display(Name = "CompanyCode", ResourceType = typeof(BootstrapResources.Resources))]
        //public string CompanyCode { get; set; }

        //[DataMember]
        //[Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        //public string PortOfLanding { get; set; } //AVR_STAD

        //[DataMember]
        //[Display(Name = "DateOfLanding", ResourceType = typeof(BootstrapResources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //public DateTime? DateOfLanding { get; set; } //AVR_DATO


        //[DataMember]
        //[Display(Name = "Company", ResourceType = typeof(BootstrapResources.Resources))]
        //public int? CompanyId { get; set; }
        //[DataMember]
        //[Display(Name = "Company", ResourceType = typeof(BootstrapResources.Resources))]
        //public virtual Company Company { get; set; }
    }
}