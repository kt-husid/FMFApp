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
    
    public partial class STUDPRI
    {
        public int ID { get; set; }
        public string ART { get; set; }
        //public Nullable<int> FK_STUDART_Id { get; set; }
        public Nullable<System.DateTime> FRA_DATO { get; set; }
        public Nullable<System.DateTime> TIL_DATO { get; set; }
        public Nullable<decimal> PRIS { get; set; }
        public Nullable<System.DateTime> RET_DATO { get; set; }
        public string RET_HH { get; set; }
        public string RET_MM { get; set; }
        public string RET_ID { get; set; }
        public string KOTA { get; set; }
        public string SKIP { get; set; }
        //public Nullable<int> FK_TYPA_Id { get; set; }
        public string FID { get; set; }
        //public Nullable<int> FK_SKIP_Id { get; set; }


        public string FiskArtId { get; set; }
        public string TypaId { get; set; }
        public string StudArtId { get; set; }

        //public virtual SKIP SKIP1 { get; set; }
        //public virtual STUDART STUDART { get; set; }
        //public virtual TYPA TYPA { get; set; }



    }
}