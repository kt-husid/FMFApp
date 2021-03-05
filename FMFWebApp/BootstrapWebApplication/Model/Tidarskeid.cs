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
    
    public partial class Tidarskeid
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tidarskeid()
        {
            this.BokDim = new HashSet<BokDim>();
            this.Bokingar = new HashSet<Bokingar>();
            this.BokUpp = new HashSet<BokUpp>();
            this.rokn_reportsLines = new HashSet<rokn_reportsLines>();
            this.rokn_reportsLines1 = new HashSet<rokn_reportsLines>();
            this.skuffur = new HashSet<skuffur>();
            this.users = new HashSet<users>();
        }
    
        public int TidarskeidId { get; set; }
        public string Tidarskeid1 { get; set; }
        public Nullable<System.DateTime> FraDato { get; set; }
        public Nullable<System.DateTime> TilDato { get; set; }
        public string Vidmerking { get; set; }
        public bool nb { get; set; }
        public bool stongt { get; set; }
        public int ArId { get; set; }
        public byte[] rv { get; set; }
    
        public virtual Ar Ar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokDim> BokDim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bokingar> Bokingar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_reportsLines> rokn_reportsLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_reportsLines> rokn_reportsLines1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skuffur> skuffur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users> users { get; set; }
    }
}
