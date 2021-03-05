using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShipTripViewModel : ViewModelBaseWithChangeEvent
    {
        public int? ShipId { get; set; }

        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int TripId { get; set; }

        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string TripIdStr { get; set; }

        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? From { get; set; }

        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? To { get; set; }

        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public double Days { get; set; }

        [Display(Name = "LaborInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? LaborInsurance { get; set; }

        [Display(Name = "LaborInsuranceTotal", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? LaborInsuranceTotal { get; set; }

        //[Display(Name = "Paid", ResourceType = typeof(BootstrapResources.Resources))]
        //public decimal? Paid { get; set; }

        [Display(Name = "Document", ResourceType = typeof(BootstrapResources.Resources))]
        public string Document { get; set; }
        
        [Display(Name = "Crew", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Crew { get; set; }

        [Display(Name = "CrewIncludingStayingAtHome", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewIncludingStayingAtHome { get; set; }

        [Display(Name = "CrewWithInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewWithInsurance { get; set; }

        [Display(Name = "CrewSharePercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePercentage { get; set; }

        [Display(Name = "CrewSharePart", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePart { get; set; }

        [Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public int PortOfLandingId { get; set; }

        //[Display(Name = "", ResourceType = typeof(BootstrapResources.Resources))]
        public string VIDAR { get; set; }

        //[Display(Name = "", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePerCrewMember { get; set; }

        [Display(Name = "TotalWeight", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TotalWeight { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "DateOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? DateOfLanding { get; set; }

        [Display(Name = "PairShip", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PairShipId { get; set; }

        [Display(Name = "PairShipDocument", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PairTrawlerDocumentId { get; set; }

        public int? TRYG_BILAG { get; set; }

        public decimal? TRYG_KRHB { get; set; }

        public decimal? TRYG_KRHB_TOTAL { get; set; }

        public decimal? LaborInsuranceDifference { get; set; }

        public decimal? MembershipPaymentTotal { get; set; } //Hans

        public decimal? MembershipPaymentNr { get; set; } //Hans

        [Display(Name = "MembershipPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal MembershipPayment { get; set; }

        [Display(Name = "MembershipPaymentPaid", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal MembershipPaymentPaid { get; set; }

    }

    public class ShipTripCreateViewModel : ViewModelBaseWithChangeEvent
    {
        public int? ShipId { get; set; }

        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int? TripId { get; set; }

        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? From { get; set; }

        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources)), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? To { get; set; }
        
        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public double Days { get; set; }

        [Display(Name = "Crew", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewIncludingStayingAtHome { get; set; } // MANNING_I

        [Display(Name = "CrewSharePercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal CrewSharePercentage { get; set; }

        [Display(Name = "CrewSharePart", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePart { get; set; }

        public string CompanyCode { get; set; }

        [Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public int PortOfLandingId { get; set; }

        [Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public Company PortOfLanding { get; set; }

        //[Display(Name = "", ResourceType = typeof(BootstrapResources.Resources))]
        public string VIDAR { get; set; }

        //[Display(Name = "", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePerCrewMember { get; set; }

        [Display(Name = "TotalWeight", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TotalWeight { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? TotalAmount { get; set; }

        //[Display(Name = "TotalAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? MANN_PART { get; set; }

        [Display(Name = "DateOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? DateOfLanding { get; set; }

        public string PairShip_ShipCode { get; set; }

        [Display(Name = "PairShip", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PairShipId { get; set; }

        [Display(Name = "PairShip", ResourceType = typeof(BootstrapResources.Resources))]
        public Ship PairShip { get; set; }

        [Display(Name = "PairShipDocument", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PairTrawlerDocumentId { get; set; }

        public string ErrorMessage { get; set; }
    }
}