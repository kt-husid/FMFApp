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
    
    public partial class SMAvgreidsluhovd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMAvgreidsluhovd()
        {
            this.SMAvgreidslulinjur = new HashSet<SMAvgreidslulinjur>();
        }
    
        public int Id { get; set; }
        public int BonId { get; set; }
        public int KassiId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> AvgrStatus { get; set; }
        public Nullable<System.DateTime> AvgrByrja { get; set; }
        public Nullable<System.DateTime> AvgrEnda { get; set; }
        public Nullable<int> KassauppteljingId { get; set; }
        public Nullable<int> AvrundingOyru { get; set; }
        public string Info { get; set; }
        public byte[] rv { get; set; }
    
        public virtual SMKassauppteljing SMKassauppteljing { get; set; }
        public virtual SMKassi SMKassi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMAvgreidslulinjur> SMAvgreidslulinjur { get; set; }
    }
}
