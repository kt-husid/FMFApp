using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;

namespace BootstrapWebApplication.Models
{
    public partial class LO_LINE : IHasChangeEvent
    {
        [Key()]
        public int Id { get; set; }

        public Nullable<int> LO_NR { get; set; }
        public Nullable<int> PERS_NR { get; set; }
        public string STATUS { get; set; }
        //public Nullable<System.DateTime> RET_DATO { get; set; }
        //public string RET_HH { get; set; }
        //public string RET_MM { get; set; }
        //public string RET_ID { get; set; }
        public Nullable<int> ANT_LN_TXT { get; set; }
        //public Nullable<System.DateTime> OPR_DATO { get; set; }
        //public string OPR_HH { get; set; }
        //public string OPR_MM { get; set; }
        //public string OPR_ID { get; set; }
        public Nullable<decimal> KR_A { get; set; }
        public Nullable<decimal> KR_F { get; set; }
        public Nullable<int> SK_P { get; set; }
        public Nullable<int> KO_P { get; set; }
        public Nullable<decimal> KR_FRT { get; set; }
        public Nullable<int> SK_F { get; set; }
        public Nullable<int> KO_F { get; set; }
        public Nullable<System.DateTime> FODT { get; set; }
        public Nullable<int> CPR { get; set; }
        public Nullable<int> ARB_NR { get; set; }

        [ForeignKey("Member")]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100
    }
}
