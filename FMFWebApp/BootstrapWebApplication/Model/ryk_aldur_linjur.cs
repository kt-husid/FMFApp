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
    
    public partial class ryk_aldur_linjur
    {
        public int ryk_aldur_linjurId { get; set; }
        public int bokid { get; set; }
        public Nullable<System.DateTime> dato { get; set; }
        public Nullable<int> skjal { get; set; }
        public string tekstur { get; set; }
        public Nullable<decimal> debit { get; set; }
        public Nullable<decimal> rest { get; set; }
        public byte[] rv { get; set; }
        public Nullable<int> Ryk_aldur_tmpId { get; set; }
    
        public virtual Ryk_aldur_tmp Ryk_aldur_tmp { get; set; }
    }
}