using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Currency
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Symbol { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public int Decimals { get; set; }
    }
}
