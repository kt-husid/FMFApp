using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MemberLastPaymentViewModel
    {
        [DataMember]
        public decimal? HolidayPay { get; set; }

        [DataMember]
        public decimal? MembershipPayment { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public BankAccount BankAccount { get; set; }
    }
}