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
    /// Not found in DB or any FRM file
    /// </summary>
    public partial class LINE
    {
        public int LINE_Id { get; set; }
        public Nullable<int> SKJAL_NR { get; set; }
        public Nullable<int> FK_TR_HOVD_Id { get; set; }
        public string ART { get; set; }
        public Nullable<decimal> NOGD { get; set; }
        public Nullable<decimal> PRIS { get; set; }
        public string S_STUD_JN { get; set; }
        public string M_STUD_JN { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
    
        public virtual TR_HOVD TR_HOVD { get; set; }
    }
}
