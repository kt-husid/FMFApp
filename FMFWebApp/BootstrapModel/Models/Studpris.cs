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
    [DataContract]
    public partial class AidPrice : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<System.DateTime> From { get; set; } // FRA_DATO

        [DataMember]
        public Nullable<System.DateTime> To { get; set; } // TIL_DATO

        [DataMember]
        public Nullable<decimal> Price { get; set; } // PRIS

        [DataMember]
        public string Code { get; set; } // KOTA

        // todo: rename
        [DataMember]
        public string SKIP { get; set; }

        [DataMember]
        public string FID { get; set; }
        
        //[ForeignKey("Ship")]
        //public Nullable<int> ShipId { get; set; }
        //public virtual Ship Ship { get; set; } //SKIP1

        [ForeignKey("FishSpecies")]
        [DataMember]
        public int? FishSpeciesId { get; set; }
        public virtual FishSpecies FishSpecies { get; set; }

        [ForeignKey("ShipType")]
        [DataMember]
        public int? ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [ForeignKey("AidType")]
        [DataMember]
        public int? AidTypeId { get; set; } //FK_P100_Id
        public virtual AidType AidType { get; set; } //STUDART
    }
}
