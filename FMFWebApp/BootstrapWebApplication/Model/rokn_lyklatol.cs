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
    
    public partial class rokn_lyklatol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rokn_lyklatol()
        {
            this.rokn_formulaelement = new HashSet<rokn_formulaelement>();
            this.rokn_kontogruppa = new HashSet<rokn_kontogruppa>();
        }
    
        public int nr { get; set; }
        public string navn { get; set; }
        public Nullable<decimal> radfylgja { get; set; }
        public string visast { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_formulaelement> rokn_formulaelement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_kontogruppa> rokn_kontogruppa { get; set; }
    }
}