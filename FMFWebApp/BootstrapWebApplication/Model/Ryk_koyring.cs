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
    
    public partial class Ryk_koyring
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ryk_koyring()
        {
            this.Ryk_aldur_tmp = new HashSet<Ryk_aldur_tmp>();
        }
    
        public int Ryk_koyringId { get; set; }
        public int koyringnr { get; set; }
        public Nullable<System.DateTime> dato { get; set; }
        public Nullable<System.TimeSpan> tid { get; set; }
        public byte[] rv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ryk_aldur_tmp> Ryk_aldur_tmp { get; set; }
    }
}
