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
    
    public partial class Ryk_aldur_tmp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ryk_aldur_tmp()
        {
            this.ryk_aldur_linjur = new HashSet<ryk_aldur_linjur>();
            this.Ryk_aldur_ryk = new HashSet<Ryk_aldur_ryk>();
        }
    
        public int Ryk_aldur_tmpId { get; set; }
        public string navn { get; set; }
        public Nullable<decimal> upphadd_90 { get; set; }
        public Nullable<decimal> upphadd_60_90 { get; set; }
        public Nullable<decimal> upphadd_30_60 { get; set; }
        public bool vid { get; set; }
        public Nullable<decimal> saldo { get; set; }
        public Nullable<decimal> upphadd_30 { get; set; }
        public Nullable<decimal> debit_iar { get; set; }
        public Nullable<decimal> kredit_iar { get; set; }
        public Nullable<decimal> sumdebit { get; set; }
        public bool bokad { get; set; }
        public bool utskriva { get; set; }
        public int ryksidstufer { get; set; }
        public Nullable<decimal> inngjald { get; set; }
        public Nullable<int> KontolistiId { get; set; }
        public Nullable<int> Ryk_koyringId { get; set; }
        public Nullable<int> Ryk_rykkjaraId { get; set; }
        public byte[] rv { get; set; }
        public Nullable<int> yvir { get; set; }
    
        public virtual Kontolisti Kontolisti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ryk_aldur_linjur> ryk_aldur_linjur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ryk_aldur_ryk> Ryk_aldur_ryk { get; set; }
        public virtual Ryk_koyring Ryk_koyring { get; set; }
        public virtual Ryk_rykkjarar Ryk_rykkjarar { get; set; }
    }
}
