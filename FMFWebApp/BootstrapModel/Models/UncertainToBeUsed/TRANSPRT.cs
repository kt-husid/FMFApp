using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.Models.Old;

namespace BootstrapWebApplication.Models
{
    public partial class TRANSPRT : IHasChangeEvent
    {
        [Key()]
        public int Id { get; set; }

        public Nullable<int> PERS_NR { get; set; }
        public Nullable<int> TRP_NR { get; set; }
        public string AR { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public Nullable<int> SJ_NR { get; set; }
        public Nullable<int> ANT_LN_TXT { get; set; }
        public Nullable<int> REG_NR { get; set; }
        public Nullable<int> KONTO { get; set; }
        public Nullable<decimal> UPPH_IALT { get; set; }
        public Nullable<System.DateTime> UTG_DATO { get; set; }
        public Nullable<System.DateTime> UD_DATO { get; set; }
        public string UD_HH { get; set; }
        public string UD_MM { get; set; }
        public string UD_ID { get; set; }
        public Nullable<int> UD_NR { get; set; }
        //public Nullable<System.DateTime> OPR_DATO { get; set; }
        //public string OPR_HH { get; set; }
        //public string OPR_MM { get; set; }
        //public string OPR_ID { get; set; }
        public string HEINTA { get; set; }
        public Nullable<int> FAX_NR { get; set; }
        public Nullable<int> REG_FRA { get; set; }
        public Nullable<int> KONTO_FRA { get; set; }
        public string EN { get; set; }
        public string FN { get; set; }

        [ForeignKey("Member")]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100

        public virtual ICollection<TRANS_TP> TRANS_TP { get; set; }

        public TRANSPRT()
        {
            this.TRANS_TP = new HashSet<TRANS_TP>();
        }
    }
}
