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
    
    public partial class gjaldoyra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gjaldoyra()
        {
            this.Kontolisti = new HashSet<Kontolisti>();
        }
    
        public int GjaldoyraId { get; set; }
        public string navn { get; set; }
        public string eind { get; set; }
        public Nullable<decimal> kursur { get; set; }
        public Nullable<System.DateTime> dagfortseinast { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti { get; set; }
    }
}