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
    
    public partial class skuffukonto
    {
        public int SkuffuKontoId { get; set; }
        public Nullable<decimal> startsaldo { get; set; }
        public Nullable<int> SkuffurId { get; set; }
        public Nullable<int> KontolistiId { get; set; }
        public byte[] rv { get; set; }
    
        public virtual Kontolisti Kontolisti { get; set; }
        public virtual skuffur skuffur { get; set; }
    }
}
