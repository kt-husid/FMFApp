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
    public partial class ShipType : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        //public string STUTT { get; set; }
        //public string BREV_UT { get; set; }
        //public string FLYTAST { get; set; }
        //public string DLISTA { get; set; }
        //public string BLISTA { get; set; }

        [DataMember]
        public bool YearList { get; set; } // ARS_LISTI

        [DataMember]
        public decimal PctToShip { get; set; } // TIL_SKIP

        [DataMember]
        public decimal PctToCrewMember { get; set; } //TIL_MANN
    
        public virtual ICollection<Ship> Ships { get; set; }
        public virtual ICollection<AidPrice> AidPrices { get; set; } // STUDPRIS

        public ShipType()
        {
            this.Ships = new HashSet<Ship>();
            this.AidPrices = new HashSet<AidPrice>();
        }
    }
}
