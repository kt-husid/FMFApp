using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class EmailAddressViewModel : ViewModelBase
    {
        public int ParentId { get; set; }

        public string Address { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }

        public bool IsPrimary { get; set; }
    }

    public class EmailAddressCreateOrEditViewModel : ViewModelBase
    {
        public int? ParentId { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(BootstrapResources.Resources))]
        public string Address { get; set; }

        [Display(Name = "IsVerified", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsVerified { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsActive { get; set; }

        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }
    }
}