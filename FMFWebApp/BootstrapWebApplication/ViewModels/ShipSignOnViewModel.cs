using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShipSignOnViewModel : ViewModelBase // ViewModelBaseWithChangeEvent
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string ShipCode { get; set; }

        public string JobCode { get; set; }

        public decimal? DG_IALT { get; set; }

        //public Person Person { get; set; }
        public DateTime? Birthday { get; set; }

        public string FullName { get; set; }
    }
}