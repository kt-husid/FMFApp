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
    
    public partial class TYPA
    {
        public TYPA()
        {
            this.P200 = new HashSet<P200>();
            this.STUDPRIS = new HashSet<STUDPRI>();
            this.STXSKs = new HashSet<STXSK>();
        }
    
        public int Id { get; set; }
        public string TypaCode { get; set; }
        public string TEKSTUR { get; set; }
        public string STUTT { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string BREV_UT { get; set; }
        public string FLYTAST { get; set; }
        public string DLISTA { get; set; }
        public string BLISTA { get; set; }
        public string ARS_LISTI { get; set; }
        public decimal? TIL_SKIP { get; set; }
        public decimal? TIL_MANN { get; set; }
    
        public virtual ICollection<P200> P200 { get; set; }
        public virtual ICollection<STUDPRI> STUDPRIS { get; set; }
        public virtual ICollection<STXSK> STXSKs { get; set; }
    }
}
