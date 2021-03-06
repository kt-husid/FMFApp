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
    
    public partial class rokn_reportsLines
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rokn_reportsLines()
        {
            this.rokn_dim = new HashSet<rokn_dim>();
            this.rokn_dimAnrOr = new HashSet<rokn_dimAnrOr>();
            this.rokn_reportsColumnCombination = new HashSet<rokn_reportsColumnCombination>();
            this.rokn_reportsColumnCombination1 = new HashSet<rokn_reportsColumnCombination>();
        }
    
        public int rokn_reportsLinesId { get; set; }
        public string navn { get; set; }
        public int rokn_reportsId { get; set; }
        public Nullable<int> reportColumnNr { get; set; }
        public int columnKind { get; set; }
        public Nullable<bool> saldoRorslur { get; set; }
        public Nullable<System.DateTime> fromDate { get; set; }
        public Nullable<System.DateTime> toDate { get; set; }
        public Nullable<int> fromPeriodId { get; set; }
        public Nullable<int> toPeriodId { get; set; }
        public Nullable<int> budgetId { get; set; }
        public Nullable<bool> budgetKind { get; set; }
        public Nullable<int> monthsQuantity { get; set; }
        public Nullable<int> fromMonth { get; set; }
        public Nullable<int> toMonth { get; set; }
        public byte[] rv { get; set; }
        public bool useDimension { get; set; }
        public int UpphaddValutaHagtol { get; set; }
    
        public virtual budget budget { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_dim> rokn_dim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_dimAnrOr> rokn_dimAnrOr { get; set; }
        public virtual rokn_reports rokn_reports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_reportsColumnCombination> rokn_reportsColumnCombination { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_reportsColumnCombination> rokn_reportsColumnCombination1 { get; set; }
        public virtual Tidarskeid Tidarskeid { get; set; }
        public virtual Tidarskeid Tidarskeid1 { get; set; }
    }
}
