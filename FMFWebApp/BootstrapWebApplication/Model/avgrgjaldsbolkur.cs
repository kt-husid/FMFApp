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
    
    public partial class avgrgjaldsbolkur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public avgrgjaldsbolkur()
        {
            this.Kontolisti = new HashSet<Kontolisti>();
        }
    
        public int AvgrGjaldsBolkurId { get; set; }
        public int ag_nummar { get; set; }
        public string ag_navn { get; set; }
        public Nullable<decimal> ag_kontoavrit_satsur { get; set; }
        public Nullable<decimal> ag_faktura_satsur { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti { get; set; }
    }
}
