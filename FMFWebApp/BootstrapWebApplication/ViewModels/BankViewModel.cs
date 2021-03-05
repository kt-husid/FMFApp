using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class BankViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class BankCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "BankCode", ResourceType = typeof(BootstrapResources.Resources))]
        public int Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }
    }
}