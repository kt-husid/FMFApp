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
    
    public partial class P300
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string NAVN { get; set; }
        public string ADR1 { get; set; }
        public string ADR2 { get; set; }
        public Nullable<int> POSTNR { get; set; }
        public Nullable<int> FK_LIMART_Id { get; set; }
        public Nullable<int> TLF { get; set; }
        public Nullable<int> FAX { get; set; }
        public string KONTAKT { get; set; }
        public string KONTAKT2 { get; set; }
        public Nullable<System.DateTime> IND_REG { get; set; }
        public Nullable<System.DateTime> UD_REG { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string NAVN5 { get; set; }
        public string ILUL { get; set; }
        public Nullable<int> LN_IALT { get; set; }
        public Nullable<int> TXT_LN { get; set; }
        public Nullable<System.DateTime> TXT_DATO { get; set; }
        public Nullable<int> AVR_IALT { get; set; }
        public Nullable<decimal> KG_IALT { get; set; }
        public Nullable<System.DateTime> LAST_DATO { get; set; }
        public string LAST_ID { get; set; }
        public Nullable<System.DateTime> OPR_DATO { get; set; }
        public string OPR_HH { get; set; }
        public string OPR_MM { get; set; }
        public string OPR_ID { get; set; }
        public string STUTT { get; set; }
        public Nullable<int> MODEM { get; set; }
        public Nullable<System.DateTime> DISK_DATO { get; set; }
        public Nullable<int> DISK_NU { get; set; }
        public Nullable<int> DISK_IALT { get; set; }
        public string PREFIX { get; set; }
        public Nullable<int> SKIB_IALT { get; set; }
        public Nullable<int> ALT_ID { get; set; }
        public Nullable<System.DateTime> ETI_DATO { get; set; }
        public string ETI_HH { get; set; }
        public string ETI_MM { get; set; }
        public string ETI_ID { get; set; }
        public string STAT { get; set; }
        public Nullable<int> FK_TURHEAD_Id { get; set; }
        public Nullable<int> ARB_NR { get; set; }
        public Nullable<decimal> FRT_LON { get; set; }
        public Nullable<System.DateTime> FRT_LON_DATO { get; set; }
        public Nullable<decimal> FRT_LON_NU { get; set; }
        public Nullable<int> A_REG { get; set; }
        public Nullable<int> A_KTO { get; set; }
    
        public virtual LIMART LIMART { get; set; }
        public virtual TURHEAD TURHEAD { get; set; }
    }
}