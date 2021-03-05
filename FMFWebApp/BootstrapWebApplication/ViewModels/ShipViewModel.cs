using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShipViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string ShipType { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }

        public int? ShippingCompanyId { get; set; }

        public string ShippingCompanyName { get; set; }

        public string Description { get; set; }
    }
}