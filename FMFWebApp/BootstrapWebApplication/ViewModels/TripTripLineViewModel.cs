using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripTripLineViewModel : ViewModelBaseWithChangeEvent
    {
        public int? TripId { get; set; }

        public int? FishSpeciesId { get; set; }

        public string FishSpeciesCode { get; set; }

        public string FishSpeciesName { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Price { get; set; }

        public decimal? KR { get; set; }

        public string CompanyName { get; set; }

        public string PortOfLanding { get; set; }

        public DateTime? DateOfLanding { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedByUserIdCode { get; set; }
    }
}