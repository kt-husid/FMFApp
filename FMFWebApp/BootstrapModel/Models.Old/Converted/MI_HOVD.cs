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
    
    /// <summary>
    /// Converted to: SignOn
    /// </summary>
    public partial class MI_HOVD
    {
        public MI_HOVD()
        {
            //this.MI_HNOT = new HashSet<MI_HNOT>();
            //this.MI_TXT = new HashSet<MI_TXT>();
            //this.TRANS_MI = new HashSet<TRANS_MI>();
        }
    
        public int Id { get; set; }
        public Nullable<int> MI_NR { get; set; }
        public Nullable<int> PERS_NR { get; set; }
        //public Nullable<int> FK_P100_Id { get; set; }
        public string SKIB_ID { get; set; }
        //public Nullable<int> FK_SKIB_Id { get; set; }
        public string AR { get; set; }
        public Nullable<System.DateTime> FRA_DATO { get; set; }
        public Nullable<System.DateTime> TIL_DATO { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public Nullable<int> ANT_LN_TXT { get; set; }
        public string SK_ID { get; set; }
        public Nullable<System.DateTime> OPR_DATO { get; set; }
        public string OPR_HH { get; set; }
        public string OPR_MM { get; set; }
        public string OPR_ID { get; set; }
        public Nullable<decimal> PART { get; set; }
        public Nullable<decimal> KR_IALT { get; set; }
        public Nullable<decimal> KG_IALT { get; set; }
        public string ID_TUR { get; set; }
        public string STARV { get; set; }
        public Nullable<int> TR_NR { get; set; }
        //public Nullable<int> FK_TR_HOVD_Id { get; set; }
        public Nullable<decimal> FRT_PART { get; set; }
        public Nullable<decimal> TIL_PR_DG { get; set; }
        public Nullable<decimal> TIL_PR_TUR { get; set; }
        public Nullable<decimal> PART_NETTO { get; set; }
        public Nullable<System.DateTime> FODT { get; set; }
        public Nullable<int> ARB_NR { get; set; }
        public Nullable<decimal> I_PART { get; set; }
        public Nullable<int> FRT_NR { get; set; }
        public string JN { get; set; }
        public string TRYG_JN { get; set; }
        public Nullable<decimal> TRYG_KR { get; set; }
        public string TRYG_KVT { get; set; }
    
        public int? MemberNR { get; set; }
        public int? ReidariId { get; set; }
        public string SkipId { get; set; }
        public int? TR_HOVD_PostNr { get; set; }
        public string StarvId { get; set; }
        public string StatId { get; set; }
        public int? TypaId { get; set; }
        public int? PostNrPostNr { get; set; }
        public string CompanyId { get; set; }
        public string LslagId { get; set; }

        //public virtual ICollection<MI_HNOT> MI_HNOT { get; set; }
        //public virtual Member P100 { get; set; }
        //public virtual SKIB SKIB { get; set; }
        //public virtual TR_HOVD TR_HOVD { get; set; }
        //public virtual ICollection<MI_TXT> MI_TXT { get; set; }
        //public virtual ICollection<TRANS_MI> TRANS_MI { get; set; }
    }
}
