//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootstrapWebApplication.Models.Old
{
    using System;
    using System.Collections.Generic;
    
    public partial class LGJALD
    {
        public LGJALD()
        {
            this.LG_TXT = new HashSet<LG_TXT>();
            this.MANNINGs = new HashSet<MANNING>();
            this.TRANS_LG = new HashSet<TRANS_LG>();
        }
    
        public int LGJALD_Id { get; set; }
        public Nullable<int> LG_NR { get; set; }
        public Nullable<int> NR { get; set; }
        public Nullable<int> FK_P100_Id { get; set; }
        public string AR { get; set; }
        public string STARV { get; set; }
        public Nullable<decimal> PRIS { get; set; }
        public Nullable<System.DateTime> ING_DATO { get; set; }
        public Nullable<int> ING_NR { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string ING_HH { get; set; }
        public string ING_MM { get; set; }
        public string ING_ID { get; set; }
        public Nullable<int> SJ_NR { get; set; }
        public Nullable<int> ANT_LN_TXT { get; set; }
        public Nullable<int> REG_NR { get; set; }
        public Nullable<int> KONTO { get; set; }
        public Nullable<decimal> FRT_LON { get; set; }
        public Nullable<decimal> LGJ { get; set; }
        public string KOTA { get; set; }
        public Nullable<decimal> TRANSP_IALT { get; set; }
        public string SLAG { get; set; }
        public string K1 { get; set; }
        public string UDS { get; set; }
    
        public virtual ICollection<LG_TXT> LG_TXT { get; set; }
        public virtual Member P100 { get; set; }
        public virtual ICollection<MANNING> MANNINGs { get; set; }
        public virtual ICollection<TRANS_LG> TRANS_LG { get; set; }
    }
}