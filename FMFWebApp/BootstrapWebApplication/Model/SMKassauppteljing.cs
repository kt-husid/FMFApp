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
    
    public partial class SMKassauppteljing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMKassauppteljing()
        {
            this.SMAvgreidsluhovd = new HashSet<SMAvgreidsluhovd>();
        }
    
        public int Id { get; set; }
        public int KassiId { get; set; }
        public int UppteljingNr { get; set; }
        public System.DateTime Opnad { get; set; }
        public Nullable<System.DateTime> Lukkad { get; set; }
        public int UserId { get; set; }
        public Nullable<int> UpptStatus { get; set; }
        public Nullable<int> Oyru25 { get; set; }
        public Nullable<int> Oyru50 { get; set; }
        public Nullable<int> Kr1 { get; set; }
        public Nullable<int> Kr2 { get; set; }
        public Nullable<int> Kr5 { get; set; }
        public Nullable<int> Kr10 { get; set; }
        public Nullable<int> Kr20 { get; set; }
        public Nullable<int> Kr50 { get; set; }
        public Nullable<int> Kr100 { get; set; }
        public Nullable<int> Kr200 { get; set; }
        public Nullable<int> Kr500 { get; set; }
        public Nullable<int> Kr1000 { get; set; }
        public Nullable<int> Kr5000 { get; set; }
        public Nullable<decimal> KontantUpptalt { get; set; }
        public Nullable<decimal> DankortPBS { get; set; }
        public Nullable<decimal> OnnurkortPBS { get; set; }
        public Nullable<decimal> TaltTilsamans { get; set; }
        public Nullable<decimal> KontantSM { get; set; }
        public Nullable<decimal> TerminalkortSM { get; set; }
        public Nullable<decimal> TotalSM { get; set; }
        public Nullable<decimal> InngjoldSM { get; set; }
        public Nullable<decimal> UtgjoldSM { get; set; }
        public Nullable<decimal> Kassamunur { get; set; }
        public Nullable<decimal> PrimoBeholdningur { get; set; }
        public Nullable<decimal> UltimoBeholdning { get; set; }
        public Nullable<decimal> FlytTilBanka { get; set; }
        public Nullable<decimal> GavukortSM { get; set; }
        public byte[] rv { get; set; }
        public Nullable<decimal> GavukortUpptalt { get; set; }
        public string OnnurFeltirXML { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMAvgreidsluhovd> SMAvgreidsluhovd { get; set; }
        public virtual SMKassi SMKassi { get; set; }
    }
}