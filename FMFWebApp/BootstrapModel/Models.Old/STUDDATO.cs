//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootstrapWebApplication.Models.Old
{
    using System;
    using System.Collections.Generic;
    
    public partial class STUDDATO
    {
        public STUDDATO()
        {
            this.TURHEADs = new HashSet<TURHEAD>();
        }
    
        public int STUDDATO_Id { get; set; }
        public string STUD_DATO { get; set; }
        public string STUD_KODE { get; set; }
        public Nullable<int> FK_VARETXT_Id { get; set; }
        public Nullable<decimal> STUD_PRIS { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
    
        public virtual VARETXT VARETXT { get; set; }
        public virtual ICollection<TURHEAD> TURHEADs { get; set; }
    }
}
