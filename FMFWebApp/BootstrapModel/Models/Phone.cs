using BootstrapWebApplication.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Phone : IHasChangeEvent, IHasEntity
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public Boolean IsPrimary { get; set; }

        [DataMember]
        public string Extension { get; set; }

        [DataMember]
        public int CountryCode { get; set; }

        [DataMember]
        public int? AreaCode { get; set; }

        [DataMember]
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string Number { get; set; }

        /// <summary>
        /// The number will be parsed using the 'libphonenumber' parser
        /// https://code.google.com/p/libphonenumber/
        /// </summary>
        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string RawNumber { get; set; }

        public override string ToString()
        {
            return CountryCode + " " + Number;
        }

        [DataMember]
        [ForeignKey("Entity")]
        public int? EntityId { get; set; }
        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        public virtual Entity Entity { get; set; }

        //[DataMember]
        //[ForeignKey("EntityBase")]
        //public int? EntityBaseId { get; set; }
        ////[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        //public virtual EntityBase EntityBase { get; set; }
    }
}