using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    //ID,MI_NR,PERS_NR,SKIB_ID,AR,FRA_DATO,TIL_DATO,STATUS,RET_DATO,RET_HH,RET_MM,RET_ID,ANT_LN_TXT,SK_ID,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,PART,KR_IALT,KG_IALT,ID_TUR,STARV,TR_NR,FRT_PART,TIL_PR_DG,TIL_PR_TUR,PART_NETTO,FODT,ARB_NR,I_PART,FRT_NR,JN,TRYG_JN,TRYG_KR,TRYG_KVT,STARV20
    //ID,MI_NR,PERS_NR,SKIB_ID,AR,FRA_DATO,TIL_DATO,RET_DATO,RET_HH,RET_MM,RET_ID,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,MI_NRGR,PERS_NRGR,SK_NRGR,STARV20,DAGAR_GR
    [DataContract]
    public partial class MI2_HOVD : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> PERS_NR { get; set; }

        [DataMember]
        public string SKIB_ID { get; set; }

        [DataMember]
        public string AR { get; set; }

        [DataMember]
        public Nullable<System.DateTime> From { get; set; } //FRA_DATO

        [DataMember]
        public Nullable<System.DateTime> To { get; set; } //TIL_DATO

        [DataMember]
        public int MI_NRGR { get; set; }

        [DataMember]
        public int PERS_NRGR { get; set; }

        [DataMember]
        public int SK_NRGR { get; set; }

        [DataMember]
        public string STARV20 { get; set; }

        [DataMember]
        public int DAGAR_GR { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100

        [ForeignKey("ShippingCompany")]
        [DataMember]
        public int? ShippingCompanyId { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; } //P300 Reiðarí

        [ForeignKey("Ship")]
        [DataMember]
        public int? ShipId { get; set; }
        public virtual Ship Ship { get; set; } //SKIB 

        [ForeignKey("TR_HOVD")]
        [DataMember]
        public int? TR_HOVDId { get; set; }
        public virtual Trip TR_HOVD { get; set; }

        [ForeignKey("Job")]
        [DataMember]
        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("Status")]
        [DataMember]
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }

        [ForeignKey("ShipType")]
        [DataMember]
        public int? ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [ForeignKey("Postal")]
        [DataMember]
        public int? PostalId { get; set; }
        public virtual Postal Postal { get; set; }

        [ForeignKey("Company")]
        [DataMember]
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; } // P600

        [ForeignKey("MemberType")]
        [DataMember]
        public int? MemberTypeId { get; set; }
        public virtual MemberType MemberType { get; set; } // LSLAG
    }
}