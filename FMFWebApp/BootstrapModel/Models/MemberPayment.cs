using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// old name: LGJALD
    /// </summary>
    [DataContract]
    public partial class MemberPayment : IHasChangeEvent
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Nullable<int> MemberPaymentNR { get; set; } //LG_NR

        [DataMember]
        public Nullable<int> MemberNR { get; set; } //NR

        [DataMember]
        [Display(Name = "Year", ResourceType = typeof(BootstrapResources.Resources))]
        public string Year { get; set; } //AR

        //public string STARV { get; set; }

        [DataMember]
        [Display(Name = "Price", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Price { get; set; } //PRIS

        // todo: move Barsil % into global variable
        [DataMember]
        [NotMapped]
        [Display(Name = "MaternityPayment", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal MaternityPayment
        {
            get
            {
                if (HolidayPay.HasValue)
                {
                    //var maternityPct = 0m;
                    //using (var db = new BootstrapWebApplication.DAL.BootstrapContext())
                    //{
                    //    maternityPct = db.AppSettings.
                    //}
                    return Math.Floor(HolidayPay.Value * 0.0062m);// maternityPct);
                }
                return 0m;
            }
        }

        // todo: move A-Trygg % into global variable
        [DataMember]
        [NotMapped]
        [Display(Name = "LaborInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal LaborInsurance
        {
            get
            {
                if (HolidayPay.HasValue)
                {
                    return HolidayPay.Value;// Math.Floor(HolidayPay.Value * 0.0099m);
                }
                return 0m;
            }
        }

        [DataMember]
        [NotMapped]
        [Display(Name = "Tax", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? Tax
        {
            get
            {
                return HolidayPay - Price - MembershipPayment - MaternityPayment - LaborInsurance;
            }
        }

        [DataMember]
        [Display(Name = "PaidOn", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> PaidOn { get; set; } //ING_DATO + ING_HH + ING_MM

        [DataMember]
        [Display(Name = "PaidBy", ResourceType = typeof(BootstrapResources.Resources))]
        public string PaidById { get; set; } //ING_ID
        
        //public Nullable<int> REG_NR { get; set; }
        //public Nullable<int> KONTO { get; set; }

        [DataMember]
        [Display(Name = "HolidayPay", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> HolidayPay { get; set; } //FRT_LON

        [DataMember]
        [Display(Name = "MembershipPayment", ResourceType = typeof(BootstrapResources.Resources))]
        [Column("LGJ")]
        public Nullable<decimal> MembershipPayment { get; set; }

        [DataMember]
        [Display(Name = "Code", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; } // KOTA

        //todo: TRANSP_IALT, SLAG and K1 are probably not used anymore, but keep it, as there's some data in these columns

        [DataMember]
        public Nullable<decimal> TRANSP_IALT { get; set; }

        [DataMember]
        public string K1 { get; set; }
        //public string UDS { get; set; }

        [ForeignKey("Member")]
        [DataMember]
        public int? MemberId { get; set; }
        public virtual Member Member { get; set; } //P100

        [ForeignKey("BankAccount")]
        [DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankAccountId { get; set; }
        [DataMember]
        public virtual BankAccount BankAccount { get; set; } //P100

        //public virtual ICollection<LG_TXT> LG_TXT { get; set; }
        //public virtual ICollection<MANNING> MANNINGs { get; set; }
        //public virtual ICollection<TRANS_LG> TRANS_LG { get; set; }
        
        public MemberPayment()
        {
            //this.LG_TXT = new HashSet<LG_TXT>();
            //this.MANNINGs = new HashSet<MANNING>();
            //this.TRANS_LG = new HashSet<TRANS_LG>();
        }
    }
}
