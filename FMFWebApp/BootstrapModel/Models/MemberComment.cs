using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class MemberComment : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Text", ResourceType = typeof(BootstrapResources.Resources))]
        public string Text { get; set; } //TEKST

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100
    }
}
