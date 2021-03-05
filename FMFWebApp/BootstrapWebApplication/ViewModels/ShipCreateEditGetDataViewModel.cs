using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShipCreateEditGetDataViewModel
    {
        public ICollection<ShippingCompany> ShippingCompanies { get; set; }

        public ICollection<ShipType> ShipTypes { get; set; }
    }
}