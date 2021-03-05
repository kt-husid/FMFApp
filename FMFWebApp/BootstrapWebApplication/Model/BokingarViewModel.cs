using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.ViewModels
{
    [DataContract]
    public class BokingarViewModel
    {
        [DataMember]
        [Display(Name = "Id", ResourceType = typeof(BootstrapResources.Resources))]
        public int BokID { get; set; }

        [DataMember]
        [Display(Name = "Date", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime Date { get; set; }
        
        [DataMember]
        [Display(Name = "Document", ResourceType = typeof(BootstrapResources.Resources))]
        public string JournalNumber { get; set; }
        
        [DataMember]
        [Display(Name = "Amount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal Amount { get; set; }

        [DataMember]
        [Display(Name = "ShipCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string ShipCode { get; set; }

        [DataMember]
        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int? TripNumber { get; set; }
    }
}