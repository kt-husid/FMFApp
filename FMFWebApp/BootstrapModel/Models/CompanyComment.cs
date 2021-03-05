using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class CompanyComment : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        [ForeignKey("Company")]
        [DataMember]
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
