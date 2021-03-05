using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class MemberTypeViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BREV_UT { get; set; }
    }

    public class MemberTypeCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }

        public string BREV_UT { get; set; }
    }
}