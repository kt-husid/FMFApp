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
    
    public partial class SKIP
    {
        public SKIP()
        {
            this.KONTTXTs = new HashSet<KONTTXT>();
            this.SHAGs = new HashSet<SHAG>();
            this.STUDPRIS = new HashSet<STUDPRI>();
            this.STXFIs = new HashSet<STXFI>();
            this.TR_LINE = new HashSet<TR_LINE>();
            this.TURHEADs = new HashSet<TURHEAD>();
            this.VHAGs = new HashSet<VHAG>();
        }
    
        public int SKIP_Id { get; set; }
        public string SK_NUMMER { get; set; }
        public string FOEN { get; set; }
        public string FNAVN { get; set; }
        public string SK_TYPE { get; set; }
        public Nullable<int> FK_SKIPTYPE_Id { get; set; }
        public string RE_NUMMER { get; set; }
        public Nullable<int> FK_REIDARI_Id { get; set; }
        public Nullable<decimal> TONNAGE { get; set; }
        public Nullable<int> HK { get; set; }
        public Nullable<System.DateTime> IND_REG { get; set; }
        public Nullable<System.DateTime> UD_REG { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
    
        public virtual ICollection<KONTTXT> KONTTXTs { get; set; }
        public virtual REIDARI REIDARI { get; set; }
        public virtual ICollection<SHAG> SHAGs { get; set; }
        public virtual SKIPTYPE SKIPTYPE { get; set; }
        public virtual ICollection<STUDPRI> STUDPRIS { get; set; }
        public virtual ICollection<STXFI> STXFIs { get; set; }
        public virtual ICollection<TR_LINE> TR_LINE { get; set; }
        public virtual ICollection<TURHEAD> TURHEADs { get; set; }
        public virtual ICollection<VHAG> VHAGs { get; set; }
    }
}