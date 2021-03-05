using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Postal : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Code { get; set; } //POSTNR

        [DataMember]
        public String CityName { get; set; } // BYGD

        [DataMember]
        [NotMapped]
        public string BYKEY
        {
            get
            {
                if (CityName.Length >= 4)
                {
                    return CityName.Substring(0, 4).ToUpperFirst();
                }
                return null;
            }
        }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string CountryCode { get; set; } // LAND

        //[DataMember]
        //public int ANT_K { get; set; }

        //[DataMember]
        //public int ANT_M { get; set; }

        //[DataMember]
        //public int ANT_X { get; set; }

        //[DataMember]
        //public int KOMNR { get; set; }

        [DataMember]
        public int OKI { get; set; }

        [DataMember]
        public int FAX_SP { get; set; }

        [DataMember]
        public int FAX_FB { get; set; }

        [DataMember]
        public int FAX_SJ { get; set; }

        [DataMember]
        public int FAX_F { get; set; }

        //public Nullable<int> MEDL_IALT { get; set; }
        //public Nullable<int> MEDL_HAN { get; set; }
        //public Nullable<int> MEDL_HUN { get; set; }
        //public Nullable<System.DateTime> RET_DATO { get; set; }
        //public string RET_HH { get; set; }
        //public string RET_MM { get; set; }
        //public string RET_ID { get; set; }
    }
}