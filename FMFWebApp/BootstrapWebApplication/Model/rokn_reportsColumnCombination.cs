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
    
    public partial class rokn_reportsColumnCombination
    {
        public int rokn_reportsColumnCombinationId { get; set; }
        public int rokn_reportsLinesId { get; set; }
        public decimal sortField { get; set; }
        public int kind { get; set; }
        public Nullable<int> rokn_reportsLinesIdColumn { get; set; }
        public string sign { get; set; }
        public Nullable<int> number { get; set; }
        public byte[] rv { get; set; }
    
        public virtual rokn_reportsLines rokn_reportsLines { get; set; }
        public virtual rokn_reportsLines rokn_reportsLines1 { get; set; }
    }
}