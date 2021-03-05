using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberBankAccountViewModel : ViewModelBase
    {
        //public int? BankId { get; set; }
        //public Bank Bank { get; set; }

        public int RegNumber { get; set; }

        public int AccountNumber { get; set; }

        public bool IsPrimary { get; set; }

        //public int? EntityId { get; set; }
        //public Entity Entity { get; set; }

        public int MemberId { get; set; }
    }

    public class MemberBankAccountCreateOrEditViewModel : ViewModelBase
    {
        [Display(Name = "RegNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int RegNumber { get; set; }

        [Display(Name = "Bank", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankId { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int AccountNumber { get; set; }

        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }

        public int? MemberId { get; set; }
    }
}