using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberFinancialAidViewModel : ViewModelBaseWithChangeEvent
    {
        public int? MemberId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Days { get; set; }
        public decimal? SocialServicePayment { get; set; }
        public decimal? PaymentPerDay { get; set; }
        public decimal? OurPayment { get; set; }
        public DateTime? PrintedOn { get; set; }
        public string PrintedBy { get; set; }
        public int? BankAccountId { get; set; }
        public int BankAccountRegNumber { get; set; }
        public int BankAccountAccountNumber { get; set; }
    }
}