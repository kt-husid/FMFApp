using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;

namespace BootstrapWebApplication.Models
{
    public partial class LO_HOVD : IHasChangeEvent
    {
        public int Id { get; set; }
        public Nullable<int> LO_NR { get; set; }

        [NotMapped()]
        public Nullable<int> ARB_NR { get { return ShippingCompany.Id; } }
        [NotMapped()]
        public Nullable<int> SKIB_NR { get { return Ship.Id; } }

        public Nullable<System.DateTime> FRA_DATO { get; set; }
        public Nullable<System.DateTime> TIL_DATO { get; set; }
        public string STATUS { get; set; }
        //public Nullable<System.DateTime> RET_DATO { get; set; }
        //public string RET_HH { get; set; }
        //public string RET_MM { get; set; }
        //public string RET_ID { get; set; }
        public Nullable<int> ANT_LN_TXT { get; set; }
        //public Nullable<System.DateTime> OPR_DATO { get; set; }
        //public string OPR_HH { get; set; }
        //public string OPR_MM { get; set; }
        //public string OPR_ID { get; set; }
        public Nullable<decimal> KR_IALT_A { get; set; }
        public Nullable<decimal> KR_IALT_F { get; set; }
        public Nullable<decimal> KR_IALT_FRT { get; set; }
        public Nullable<int> SK_A { get; set; }
        public Nullable<int> KO_A { get; set; }
        public Nullable<int> SK_FR { get; set; }
        public Nullable<int> KO_FR { get; set; }
        public Nullable<int> MANS_IALT { get; set; }
        public string SKEI { get; set; }
        public Nullable<System.DateTime> UT_DATO { get; set; }
        public string UT_HH { get; set; }
        public string UT_MM { get; set; }
        public string UT_ID { get; set; }
        public Nullable<int> UT_ANT { get; set; }

        [ForeignKey("ShippingCompany")]
        public int? ShippingCompanyId { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }

        [ForeignKey("Ship")]
        public int? ShipId { get; set; }
        public virtual Ship Ship { get; set; }

        [ForeignKey("Status")]
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }

        [ForeignKey("ShipType")]
        public int? ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [ForeignKey("Postal")]
        public Nullable<int> PostalId { get; set; }
        public virtual Postal Postal { get; set; }
    }
}
