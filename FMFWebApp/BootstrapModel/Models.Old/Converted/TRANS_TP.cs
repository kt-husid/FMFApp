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
    
    public partial class TRANS_TP
    {
        public int TRANS_TP_Id { get; set; }
        public Nullable<System.DateTime> IND_DATO { get; set; }
        public string IND_HH { get; set; }
        public string IND_MM { get; set; }
        public string IND_ID { get; set; }
        public Nullable<int> IND_TTY { get; set; }
        public string PROG { get; set; }
        public Nullable<int> TRP_NR { get; set; }
        public Nullable<int> FK_TRANSPRT_Id { get; set; }
        public string STATUS { get; set; }
        public Nullable<int> UDSKR_NR { get; set; }
        public string FRA { get; set; }
        public string TIL { get; set; }
        public Nullable<int> FELT { get; set; }
    
        public virtual TRANSPRT TRANSPRT { get; set; }
    }
}
