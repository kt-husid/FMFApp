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
    
    public partial class TR_TXT
    {
        public int TR_TXT_Id { get; set; }
        public Nullable<int> TR_NR { get; set; }
        public Nullable<int> FK_TR_HOVD_Id { get; set; }
        public Nullable<int> LINJE { get; set; }
        public string TEKST { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
    
        public virtual TR_HOVD TR_HOVD { get; set; }
    }
}