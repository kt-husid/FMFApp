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
    
    public partial class LIMART
    {
        public LIMART()
        {
            this.F100 = new HashSet<F100>();
            this.G100 = new HashSet<G100>();
            this.LEVs = new HashSet<LEV>();
            this.LIMAGJs = new HashSet<LIMAGJ>();
            this.MANNINGs = new HashSet<MANNING>();
            this.P100 = new HashSet<Member>();
            this.P200 = new HashSet<P200>();
            this.P300 = new HashSet<P300>();
            this.P600 = new HashSet<P600>();
            this.REDERIs = new HashSet<REDERI>();
            this.S100 = new HashSet<S100>();
            this.SBs = new HashSet<SB>();
            this.SKIBs = new HashSet<SKIB>();
            this.VIRKIs = new HashSet<VIRKI>();
        }
    
        public int LIMART_Id { get; set; }
        public string LIMART1 { get; set; }
        public string TXT { get; set; }
        public Nullable<int> MEDL_IALT { get; set; }
        public Nullable<int> MEDL_HAN { get; set; }
        public Nullable<int> MEDL_HUN { get; set; }
        public Nullable<System.DateTime> OPTALT { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
    
        public virtual ICollection<F100> F100 { get; set; }
        public virtual ICollection<G100> G100 { get; set; }
        public virtual ICollection<LEV> LEVs { get; set; }
        public virtual ICollection<LIMAGJ> LIMAGJs { get; set; }
        public virtual ICollection<MANNING> MANNINGs { get; set; }
        public virtual ICollection<Member> P100 { get; set; }
        public virtual ICollection<P200> P200 { get; set; }
        public virtual ICollection<P300> P300 { get; set; }
        public virtual ICollection<P600> P600 { get; set; }
        public virtual ICollection<REDERI> REDERIs { get; set; }
        public virtual ICollection<S100> S100 { get; set; }
        public virtual ICollection<SB> SBs { get; set; }
        public virtual ICollection<SKIB> SKIBs { get; set; }
        public virtual ICollection<VIRKI> VIRKIs { get; set; }
    }
}
