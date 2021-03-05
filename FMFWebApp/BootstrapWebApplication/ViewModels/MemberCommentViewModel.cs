using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberCommentViewModel : ViewModelBaseWithChangeEvent// ViewModelBaseWithChangeEvent
    {
        [Display(Name = "Comment", ResourceType = typeof(BootstrapResources.Resources))]
        public string Text { get; set; }

        public int? MemberId { get; set; }
    }
}