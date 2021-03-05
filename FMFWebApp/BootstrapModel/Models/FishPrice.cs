using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// Old name: Fiskpri
    /// </summary>
    [DataContract]
    public partial class FishPrice : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FishSpeciesCode { get; set; } // ART

        [DataMember]
        public Nullable<System.DateTime> From { get; set; } // FRA_DATO
        
        [DataMember]
        public Nullable<System.DateTime> To { get; set; } // TIL_DATO

        [DataMember]
        public Nullable<decimal> Price { get; set; } // PRIS
    }
}
