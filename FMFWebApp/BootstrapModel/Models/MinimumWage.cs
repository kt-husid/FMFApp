using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MinimumWage : IHasChangeEvent
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<decimal> PerMonth { get; set; }

        [DataMember]
        public Nullable<decimal> PerDay { get; set; }

        [DataMember]
        [Obsolete("")]
        public decimal? DG_MIN { get; set; }
        
        [DataMember]
        [Obsolete("")]
        public decimal? DG_MAX { get; set; }

        [DataMember]
        [Obsolete("")]
        public decimal? GRAD { get; set; }

        [DataMember]
        [Obsolete("")]
        public decimal? DG_ST { get; set; }
        
        [DataMember]
        public string Code { get; set; }
    }
}
