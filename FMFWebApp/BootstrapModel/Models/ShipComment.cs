using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class ShipComment : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        [ForeignKey("Ship")]
        [DataMember]
        public int? ShipId { get; set; }
        public virtual Ship Ship { get; set; }
    }
}
