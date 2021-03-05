using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberHolidayPay_HOVD_ViewModel : ViewModelBaseWithChangeEvent// ViewModelBaseWithChangeEvent
    {
        public DateTime? TransferDate { get; set; }
        public decimal? Amount { get; set; }
        public int? EmployerNumber { get; set; }
        public int? REG_NR_FF { get; set; }
        public int? KONTO_FF { get; set; }
        public int? REG_NR { get; set; }
        public int? KONTO { get; set; }
        public string ART { get; set; }
        public DateTime? KOYR_DATO { get; set; }
        public int? TR_NR { get; set; }
    }
}