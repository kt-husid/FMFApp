using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class EmailAddress : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Local { get; set; }

        [DataMember]
        public String Domain { get; set; }

        [DataMember]
        public Boolean IsVerified { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }

        [DataMember]
        [ForeignKey("Entity")]
        public int? EntityId { get; set; }
        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        public virtual Entity Entity { get; set; }

        [DataMember]
        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }
    }
}