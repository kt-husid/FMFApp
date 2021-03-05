using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class StatusViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string BREV_UT { get; set; }
        public string FLYTAST { get; set; }
        public string DLISTA { get; set; }
        public string BLISTA { get; set; }
        public string YearList { get; set; }
        public bool IsOnYearList { get; set; }
    }

    public class StatusCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }

        public string BREV_UT { get; set; }
        public string FLYTAST { get; set; }
        public string DLISTA { get; set; }
        public string BLISTA { get; set; }
        public string YearList { get; set; }

        [Display(Name = "IsOnYearList", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsOnYearList { get; set; }
    }
}