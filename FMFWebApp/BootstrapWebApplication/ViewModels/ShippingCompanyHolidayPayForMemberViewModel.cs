using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class ShippingCompanyHolidayPayForMemberViewModel : ViewModelBaseWithChangeEvent
    {
        public DateTime? BirthDay { get; set; }

        public string FullName { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TransferDate { get; set; }

        public string ART { get; set; }

        public string TripNumber { get; set; }

        public int? KONTO_FF { get; set; }

        public int? REG_NR_FF { get; set; }
    }
}