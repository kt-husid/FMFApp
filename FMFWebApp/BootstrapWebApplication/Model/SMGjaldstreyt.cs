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
    
    public partial class SMGjaldstreyt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMGjaldstreyt()
        {
            this.Kontolisti = new HashSet<Kontolisti>();
            this.SMSedlahovd = new HashSet<SMSedlahovd>();
        }
    
        public int GjaldstreytId { get; set; }
        public string Navn { get; set; }
        public Nullable<int> Eind1 { get; set; }
        public Nullable<int> Eindir1 { get; set; }
        public Nullable<int> Eind2 { get; set; }
        public Nullable<int> Eindir2 { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSedlahovd> SMSedlahovd { get; set; }
    }
}
