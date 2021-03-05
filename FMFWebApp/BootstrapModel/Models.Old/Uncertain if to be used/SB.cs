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
    
    public partial class SB
    {
        public int SB_Id { get; set; }
        public Nullable<int> NR { get; set; }
        public Nullable<System.DateTime> FODT { get; set; }
        public string FNAVN { get; set; }
        public string ENAVN { get; set; }
        public string ADR1 { get; set; }
        public Nullable<int> POSTNR { get; set; }
        public Nullable<int> FK_LIMART_Id { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string FN5 { get; set; }
        public string STRIKA { get; set; }
        public string KYN { get; set; }
        public Nullable<int> PRI_OWN { get; set; }
        public Nullable<int> BETALER { get; set; }
        public string EN5 { get; set; }
        public Nullable<System.DateTime> OPR_DATO { get; set; }
        public string OPR_HH { get; set; }
        public string OPR_MM { get; set; }
        public string OPR_ID { get; set; }
        public string M_STATUS { get; set; }
        public Nullable<int> TXT_LN { get; set; }
        public Nullable<int> TLF_HEIMA { get; set; }
        public Nullable<int> TYPE { get; set; }
        public string ENFO { get; set; }
        public Nullable<int> CPR { get; set; }
        public string STARV { get; set; }
        public Nullable<int> FK_STARV_Id { get; set; }
        public Nullable<decimal> BURT_DG { get; set; }
        public Nullable<decimal> TUR_IALT { get; set; }
        public Nullable<decimal> INN_IALT { get; set; }
        public string DG { get; set; }
        public string MD { get; set; }
        public string STAT { get; set; }
        public Nullable<int> FK_TURHEAD_Id { get; set; }
        public Nullable<System.DateTime> TXT_DATO { get; set; }
        public Nullable<System.DateTime> LAST_DATO { get; set; }
        public string MNAVN { get; set; }
        public Nullable<int> MI_TOT { get; set; }
        public Nullable<int> LG_TOT { get; set; }
        public string MI_AR { get; set; }
        public string MI_ID { get; set; }
        public string LG_AR { get; set; }
        public Nullable<decimal> LG_KR { get; set; }
        public string LAST_ID { get; set; }
        public Nullable<System.DateTime> ETI_DATO { get; set; }
        public string ETI_HH { get; set; }
        public string ETI_MM { get; set; }
        public string ETI_ID { get; set; }
        public string SLAG { get; set; }
        public Nullable<int> FK_LSLAG_Id { get; set; }
        public Nullable<decimal> LG_IALT { get; set; }
        public Nullable<decimal> TRANSPORT { get; set; }
    
        public virtual LIMART LIMART { get; set; }
        public virtual LSLAG LSLAG { get; set; }
        public virtual STARV STARV1 { get; set; }
        public virtual TURHEAD TURHEAD { get; set; }
    }
}
