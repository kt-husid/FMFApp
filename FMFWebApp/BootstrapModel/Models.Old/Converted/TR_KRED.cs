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
    
    public partial class TR_KRED
    {
        public int ID { get; set; }
        public Nullable<int> TR_NR { get; set; }
        //public Nullable<int> FK_TR_HOVD_Id { get; set; }
        public string ART { get; set; }
        public Nullable<decimal> NOGD { get; set; }
        public Nullable<decimal> PRIS { get; set; }
        public string S_STUD_JN { get; set; }
        public string M_STUD_JN { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public Nullable<System.DateTime> OPR_DATO { get; set; }
        public string OPR_HH { get; set; }
        public string OPR_MM { get; set; }
        public string OPR_ID { get; set; }
        public string STATUS { get; set; }
        public Nullable<decimal> ALT_PRIS { get; set; }
        public Nullable<decimal> KR { get; set; }
        public string AVR_STAD { get; set; } 
        public Nullable<System.DateTime> AVR_DATO { get; set; }

        //public virtual TR_HOVD TR_HOVD { get; set; }

        public int ReidariId { get; set; }
        public string SkipId { get; set; }
        public int? TR_HOVD_PostNr { get; set; }
        public string TypaId { get; set; }
        public int? StatId { get; set; }
        public int? PostNrPostNr { get; set; }
        public string CompanyId { get; set; }
        public string Frad_ArtId { get; set; }
    }
}
