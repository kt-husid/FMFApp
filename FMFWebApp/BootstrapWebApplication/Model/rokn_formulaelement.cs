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
    
    public partial class rokn_formulaelement
    {
        public int nr { get; set; }
        public Nullable<int> lyklatolnr { get; set; }
        public Nullable<decimal> radfylgja { get; set; }
        public Nullable<int> slag { get; set; }
        public Nullable<int> kontogr { get; set; }
        public string tegn { get; set; }
        public Nullable<decimal> tal { get; set; }
        public byte[] rv { get; set; }
        public int upphaddvalutahagtol { get; set; }
    
        public virtual rokn_grundtol rokn_grundtol { get; set; }
        public virtual rokn_lyklatol rokn_lyklatol { get; set; }
    }
}