using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripCreateEditGetRelatedDataViewModel
    {
        public ICollection<Company> Companies { get; set; }

        public ICollection<Ship> Ships { get; set; }
    }
}