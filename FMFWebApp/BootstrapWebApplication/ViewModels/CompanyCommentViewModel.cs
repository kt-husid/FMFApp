using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class CompanyCommentViewModel : ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Comment", ResourceType = typeof(BootstrapResources.Resources))]
        public string Text { get; set; }

        public int? CompanyId { get; set; }
    }
}