using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// (Mynstring (P522.IB2 og MI_HOVD.def)
    /// MI_HOVD (Mi_hovd): ID,MI_NR,PERS_NR,SKIB_ID,AR,FRA_DATO,TIL_DATO,STATUS,RET_DATO,RET_HH,RET_MM,RET_ID,ANT_LN_TXT,SK_ID,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,PART,KR_IALT,KG_IALT,ID_TUR,STARV,TR_NR,FRT_PART,TIL_PR_DG,TIL_PR_TUR,PART_NETTO,FODT,ARB_NR,I_PART,FRT_NR,JN,TRYG_JN,TRYG_KR,TRYG_KVT
    /// 
    /// ID,MI_NR,PERS_NR,SKIB_ID,AR,FRA_DATO,TIL_DATO,STATUS,RET_DATO,RET_HH,RET_MM,RET_ID,ANT_LN_TXT,SK_ID,OPR_DATO,OPR_HH,OPR_MM,OPR_ID,PART,KR_IALT,KG_IALT,ID_TUR,STARV,TR_NR,FRT_PART,TIL_PR_DG,TIL_PR_TUR,PART_NETTO,FODT,ARB_NR,I_PART,FRT_NR,JN,TRYG_JN,TRYG_KR,TRYG_KVT
    /// </summary>
    [DataContract]
    public partial class SignOn : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> SignOnNumber { get; set; } //MI_NR

        [DataMember]
        public Nullable<int> PersonNumber { get; set; } //PERS_N

        //[DataMember]
        //public string ShipCode { get; set; } //SKIB_ID

        [DataMember]
        public string Year { get; set; } //AR

        [DataMember]
        public Nullable<System.DateTime> From { get; set; } //FRA_DATO

        [DataMember]
        public Nullable<System.DateTime> To { get; set; } //TIL_DATO

        //public string STATUS { get; set; }
        //public Nullable<int> ANT_LN_TXT { get; set; }

        // todo: what's this for?!!!!
        [DataMember]
        public string SK_ID { get; set; }

        [DataMember]
        public Nullable<decimal> PART { get; set; }

        [DataMember]
        public Nullable<decimal> KR_IALT { get; set; }

        [NotMapped]
        [DataMember]
        public decimal KR_IALT_PART
        {
            get
            {
                if (!KR_IALT.HasValue) return 0;
                if (!PART.HasValue) return 0;
                if (PART.Value == 0) return 0;

                return Math.Round(KR_IALT.Value / PART.Value, 2);
            }
        }

        //[Obsolete("")]
        //[DataMember]
        //public Nullable<decimal> KG_IALT { get; set; }

        [Obsolete("Same as Trip.TripId")]
        [DataMember]
        public string ID_TUR { get; set; }

        //[DataMember]
        //public string JobWhileSignedOn { get; set; } // STARV

        [Obsolete("Same as Trip.TripNumber")]
        [DataMember]
        public Nullable<int> TR_NR { get; set; }

        //[Obsolete("Unused")]
        //[DataMember]
        //public Nullable<decimal> FRT_PART { get; set; }

        //[Obsolete("Unused")]
        //[DataMember]
        //public Nullable<decimal> PART_NETTO { get; set; }

        [NotMapped]
        [DataMember]
        public decimal KR_I
        {
            get
            {
                if (KR_IALT.HasValue && TIL_PR_TUR.HasValue)
                {
                    var part = 1m / ((PART.HasValue && PART.Value > 0) ? PART.Value : 1m);
                    return Math.Round((part * KR_IALT.Value) - TIL_PR_TUR.Value, 2);
                }
                return 0m;
            }
        }

        [NotMapped]
        [DataMember]
        public decimal KR_FF
        {
            get
            {
                return KR_I;
            }
        }

        [DataMember]
        public Nullable<decimal> TIL_PR_DG { get; set; }

        [DataMember]
        public Nullable<decimal> TIL_PR_TUR { get; set; }

        //[Obsolete("use instead Member.Birthday")]
        //[DataMember]
        //public Nullable<System.DateTime> Birthday { get; set; } //FODT

        [DataMember]
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        [DataMember]
        public Nullable<decimal> I_PART { get; set; }

        [DataMember]
        public Nullable<int> FRT_NR { get; set; }

        //public string JN { get; set; }

        [Obsolete("Use: HasInsurance")]
        [DataMember]
        public string TRYG_JN { get; set; }

        [DataMember]
        public bool HasInsurance { get; set; }

        [DataMember]
        public Nullable<decimal> LaborInsuranceAmountPerDay { get; set; } // TRYG_KR

        [DataMember]
        public string TRYG_KVT { get; set; }

        [DataMember]
        [ForeignKey("Member")]
        public Nullable<int> MemberId { get; set; } //FK_P100_Id
        [DataMember]
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; } //P100

        [ForeignKey("ShippingCompany")]
        [DataMember]
        public Nullable<int> ShippingCompanyId { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; } //P300

        //[ForeignKey("Ship")]
        //[DataMember]
        //public Nullable<int> ShipId { get; set; }
        //[DataMember]
        //public virtual Ship Ship { get; set; }

        [NotMapped]
        [DataMember]
        public Ship Ship
        {
            get
            {
                if (Trip != null)
                {
                    return Trip.Ship;
                }
                return null;
            }
        }

        //[NotMapped]
        [DataMember]
        public string ShipCode { get; set; }
        //{
        //    get
        //    {
        //        if (Ship != null)
        //        {
        //            return Ship.Code;
        //        }
        //        return String.Empty;
        //    }
        //}

        //[NotMapped]
        [DataMember]
        public string ShipName { get; set; }
        //{
        //    get
        //    {
        //        if (Ship != null)
        //        {
        //            return Ship.Name;
        //        }
        //        return String.Empty;
        //    }
        //}

        [NotMapped]
        [DataMember]
        public string JobCode
        {
            get
            {
                if (JobWhileSignedOn != null)
                {
                    return JobWhileSignedOn.Code;
                }
                return String.Empty;
            }
        }

        [NotMapped]
        [DataMember]
        public int? TripNumber
        {
            get
            {
                if (Trip != null)
                {
                    return Trip.Id;
                }
                return null;
            }
        }


        [ForeignKey("Trip")]
        [DataMember]
        public Nullable<int> TripId { get; set; }
        [DataMember]
        public virtual Trip Trip { get; set; }

        [ForeignKey("JobWhileSignedOn")]
        [DataMember]
        public Nullable<int> JobWhileSignedOnId { get; set; }
        public virtual Job JobWhileSignedOn { get; set; }

        [ForeignKey("MemberType")]
        [DataMember]
        public Nullable<int> MemberTypeId { get; set; }
        public virtual MemberType MemberType { get; set; }

        //[ForeignKey("ShipType")]
        //[DataMember]
        //public Nullable<int> ShipTypeId { get; set; }
        //public virtual ShipType ShipType { get; set; }

        [NotMapped]
        [DataMember]
        public ShipType ShipType
        {
            get
            {
                if (Trip != null)
                {
                    return Trip.Ship.ShipType;
                }
                return null;
            }
        }

        //[ForeignKey("Postal")]
        //[DataMember]
        //public int? PostalId { get; set; }
        //public virtual Postal Postal { get; set; }

        [ForeignKey("Company")]
        [DataMember]
        public Nullable<int> CompanyId { get; set; }
        public virtual Company Company { get; set; }

        //[ForeignKey("MemberType")]
        //[DataMember]
        //public Nullable<int> MemberTypeId { get; set; }
        //public virtual MemberType MemberType { get; set; }


        public SignOn()
        {
            
        }

        [NotMapped]
        public int? EntityId
        {
            get
            {
                return Ship.EntityId;
            }
            set
            {
                Ship.EntityId = value;
            }
        }

        [NotMapped]
        public Entity Entity
        {
            get
            {
                return Ship.Entity;
            }
            set
            {
                Ship.Entity = value;
            }
        }
    }
}