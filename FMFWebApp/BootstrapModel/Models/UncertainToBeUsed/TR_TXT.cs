using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;

namespace BootstrapWebApplication.Models
{   
    public partial class TR_TXT : IHasChangeEvent
    {
        [Key()]
        public int Id { get; set; }

        public Nullable<int> TR_NR { get; set; }
        public Nullable<int> LINJE { get; set; }
        public string TEKST { get; set; }
        //public Nullable<System.DateTime> RET_DATO { get; set; }
        //public string RET_HH { get; set; }
        //public string RET_MM { get; set; }
        //public string RET_ID { get; set; }

        [ForeignKey("Trip")]
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
