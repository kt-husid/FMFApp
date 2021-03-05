using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class PersonTitleViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PersonTitleCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }
    }
}