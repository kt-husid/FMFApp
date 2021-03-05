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
    public partial class FishSpeciesFromFile
    {	
        [DataMember]
        public int? FiskaslagNr { get; set; }

        [DataMember]
        public string FiskaslagKota { get; set; }

        [DataMember]
        public string Fiskaheiti { get; set; }

        [DataMember]
        public string Fiskaheiti2 { get; set; }
    }
}
