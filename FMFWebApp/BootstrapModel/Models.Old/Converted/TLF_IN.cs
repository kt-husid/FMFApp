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
    
    public partial class TLF_IN
    {
        public int TLF_IN_Id { get; set; }
        public Nullable<int> NR { get; set; }
        public Nullable<int> FK_P100_Id { get; set; }
        public Nullable<System.DateTime> DATO { get; set; }
        public string HH { get; set; }
        public string MM { get; set; }
    
        public virtual Member P100 { get; set; }
    }
}
