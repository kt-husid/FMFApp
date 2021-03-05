using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// FRAD_ART
    /// </summary>
    [DataContract]
    public partial class DeductionType : IHasChangeEvent
    {
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; } //ID

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; } //NAVN

        //public string STUTT { get; set; }
        //public string ALIAS { get; set; }
        //public Nullable<System.DateTime> RET_DATO { get; set; }
        //public string RET_HH { get; set; }
        //public string RET_MM { get; set; }
        //public string RET_ID { get; set; }
        //public Nullable<int> LINK { get; set; }
        //public string RAD { get; set; }

        [DataMember]
        [Display(Name = "Type", ResourceType = typeof(BootstrapResources.Resources))]
        public int? Type { get; set; } //TYPA
    }
}
