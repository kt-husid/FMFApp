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
    
    public partial class KONTTXT
    {
        public int KONTTXT_Id { get; set; }
        public Nullable<int> NR { get; set; }
        public Nullable<int> FK_SKIP_Id { get; set; }
        public string NAVN { get; set; }
        public Nullable<int> ANTAL { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string NAVNKEY { get; set; }
    
        public virtual SKIP SKIP { get; set; }
    }
}
