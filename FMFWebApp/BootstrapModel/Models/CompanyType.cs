using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class CompanyType
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Description { get; set; }
    }
}