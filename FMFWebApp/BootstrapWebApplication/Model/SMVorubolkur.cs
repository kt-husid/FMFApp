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
    
    public partial class SMVorubolkur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMVorubolkur()
        {
            this.SMVorur = new HashSet<SMVorur>();
        }
    
        public int VorubolkurId { get; set; }
        public string Navn { get; set; }
        public Nullable<int> SoluMvgTypaId { get; set; }
        public Nullable<int> KeypMvgTypaId { get; set; }
        public Nullable<int> VoruSolaKonta { get; set; }
        public Nullable<int> VoruKeypKonta { get; set; }
        public Nullable<int> VoruSolaKontaMVGFri { get; set; }
        public Nullable<int> VoruKeypKontaMVGFri { get; set; }
        public byte[] rv { get; set; }
        public string Navn2 { get; set; }
    
        public virtual Kontolisti Kontolisti { get; set; }
        public virtual Kontolisti Kontolisti1 { get; set; }
        public virtual Kontolisti Kontolisti2 { get; set; }
        public virtual Kontolisti Kontolisti3 { get; set; }
        public virtual MvgTypa MvgTypa { get; set; }
        public virtual MvgTypa MvgTypa1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorur> SMVorur { get; set; }
    }
}