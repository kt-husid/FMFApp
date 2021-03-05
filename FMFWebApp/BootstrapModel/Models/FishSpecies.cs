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
    /// Old name: FiskArt
    /// </summary>
    [DataContract]
    public partial class FishSpecies : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string OldCode { get; set; } //ID

        [DataMember]
        public string Name { get; set; } //NAVN

        [DataMember]
        public string OldName { get; set; } //NAVN

        [DataMember]
        public string Description { get; set; } // STUTT
        
        [DataMember]
        public int? FishSpeciesNumber { get; set; } // FiskaslagNr
        
        [DataMember]
        public string ALIAS { get; set; } // ALIAS

        [DataMember]
        public Nullable<int> LINK { get; set; }

        [DataMember]
        public string RAD { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
