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
    
    public partial class rentubolkur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rentubolkur()
        {
            this.Kontolisti = new HashSet<Kontolisti>();
        }
    
        public int RentubolkurId { get; set; }
        public string rb_nummar { get; set; }
        public string rb_navn { get; set; }
        public Nullable<decimal> rb_satsur { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti { get; set; }
    }
}
