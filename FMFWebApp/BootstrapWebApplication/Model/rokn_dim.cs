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
    
    public partial class rokn_dim
    {
        public int rokn_dimId { get; set; }
        public int rokn_reportsLinesId { get; set; }
        public Nullable<int> DimensionirId { get; set; }
        public byte[] rv { get; set; }
    
        public virtual Dimensionir Dimensionir { get; set; }
        public virtual rokn_reportsLines rokn_reportsLines { get; set; }
    }
}
