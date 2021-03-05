using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripLineCreateEditGetRelatedDataViewModel
    {
        public ICollection<Company> Companies { get; set; }

        public ICollection<FishSpecies> FishSpecies { get; set; }
    }
}