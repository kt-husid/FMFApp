//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootstrapWebApplication.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class emaillog
    {
        public int EmailLogId { get; set; }
        public System.DateTime dato { get; set; }
        public System.TimeSpan klokka { get; set; }
        public string email { get; set; }
        public string fragreiding { get; set; }
        public Nullable<int> UsersId { get; set; }
        public Nullable<int> KontolistiId { get; set; }
        public byte[] rv { get; set; }
    
        public virtual Kontolisti Kontolisti { get; set; }
        public virtual users users { get; set; }
    }
}