using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Status : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string STUTT { get; set; }

        [DataMember]
        public string BREV_UT { get; set; }

        [DataMember]
        public string FLYTAST { get; set; }

        [DataMember]
        public string DLISTA { get; set; }

        [DataMember]
        public string BLISTA { get; set; }

        [DataMember]
        public string YearList { get; set; }

        [DataMember]
        public bool IsOnYearList { get; set; }
    }
}