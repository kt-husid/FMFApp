using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class ShipTypeViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool YearList { get; set; }
        public decimal PctToShip { get; set; } // TIL_SKIP
        public decimal PctToCrewMember { get; set; } //TIL_MANN
    }

    public class ShipTypeCreateEditViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public string Description { get; set; }

        [Display(Name = "IsOnYearList", ResourceType = typeof(BootstrapResources.Resources))]
        public bool YearList { get; set; }

        [Display(Name = "PctToShip", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal PctToShip { get; set; } // TIL_SKIP

        [Display(Name = "PctToCrewMember", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal PctToCrewMember { get; set; } //TIL_MANN
    }
}