using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Trip : IHasChangeEvent, IHasId
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        //[Obsolete("Don't use this property!")]
        //public string SK_ID { get; set; }
        //[Obsolete("Don't use this property!")]
        //public Nullable<decimal> ShipsSharePercentage { get; set; } //TIL_SKIP_PCT
        //[Obsolete("Don't use this property!")]
        //public Nullable<decimal> ShipsShareMoney { get; set; } //TIL_SKIP_KR
        //[Obsolete("Don't use this property!")]
        //public Nullable<decimal> TILSKOT { get; set; }


        //public string ID_SKIB { get; set; }
        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "TripNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int TripId { get; set; } //ID_TUR

        [DataMember]
        [Display(Name = "From", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public Nullable<System.DateTime> From { get; set; } //FRA_DATO

        [DataMember]
        [Display(Name = "To", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public Nullable<System.DateTime> To { get; set; } //TIL_DATO

        // todo: remove this (is replaced by Trip.Id)
        [Obsolete("Use Trip.Id instead")]
        [DataMember]
        public Nullable<int> TripNumber { get; set; } //TR_NR

        [DataMember]
        public Nullable<int> PairTrawlerDocumentId { get; set; } //PAR_SKJAL

        [DataMember]
        [Display(Name = "CrewWithInsurance", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewWithInsurance
        {
            get
            {
                var c = 0m;
                foreach (var signOn in SignOns.FilterDeleted())
                {
                    if (signOn.HasInsurance)
                    {
                        c++;
                    }
                }
                return c;
            }
        }


        [DataMember]
        //[NotMapped]
        [Display(Name = "ShipTypeCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string ShipTypeCode { get; set; } // SK_ID
        //{
        //    get
        //    {
        //        //if (Ship != null && Ship.ShipType != null)
        //        //{
        //        //    return Ship.ShipType.Code;
        //        //}
        //        //return null;
        //        if (ShipType != null)
        //        {
        //            return ShipType.Code;
        //        }
        //        return null;
        //    }
        //}

        [DataMember]
        [ForeignKey("ShipType")]
        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        public int? ShipTypeId { get; set; }
        [Display(Name = "ShipType", ResourceType = typeof(BootstrapResources.Resources))]
        [ForeignKey("ShipTypeId")]
        public virtual ShipType ShipType { get; set; }

        //public string STATUS { get; set; }
        //public string UDSKREVET { get; set; }
        //public string UPPTALD { get; set; }
        //public Nullable<System.DateTime> UD_DATO { get; set; }
        //public string UD_HH { get; set; }
        //public string UD_MM { get; set; }
        //public string UD_ID { get; set; }

        [DataMember]
        [Display(Name = "TotalWeight", ResourceType = typeof(BootstrapResources.Resources))]
        [Obsolete("Use instead: Trip.TotalWeightCalculated() !!!")]
        public Nullable<decimal> TotalWeight { get; set; } // KG_IALT

        [DataMember]
        [Display(Name = "TotalWeight", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> TotalWeightCalculated //KG_IALT
        {
            get
            {
                var tripLinesTotalWeight = 0m;
                foreach (var tripLine in TripLines.FilterDeleted())
                {
                    tripLinesTotalWeight += tripLine.Amount;
                }
                return tripLinesTotalWeight;
            }
        }

        [DataMember]
        [NotMapped]
        public bool IsTotalWeightCorrect
        {
            get
            {
                var sum = 0m;
                foreach (var tripLine in TripLines.FilterDeleted())
                {
                    sum += tripLine.Amount;
                }
                return sum == this.TotalWeightCalculated;// this.TotalWeight;
            }
        }

        [DataMember]
        [Obsolete("Use instead: Trip.TotalAmountCalculated() !!!")]
        [Display(Name = "TotalAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> TotalAmount { get; set; } //UPPH_IALT

        [DataMember]
        [Display(Name = "TotalAmount", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> TotalAmountCalculated //UPPH_IALT
        {
            get
            {
                var tripLinesTotalCost = 0m;
                foreach (var tripLine in TripLines.FilterDeleted())
                {
                    tripLinesTotalCost += tripLine.TotalPrice;
                }
                return tripLinesTotalCost;
            }
        }

        [DataMember]
        [NotMapped]
        public bool IsTotalAmountCorrect
        {
            get
            {
                var sum = 0m;
                foreach (var tripLine in TripLines.FilterDeleted())
                {
                    sum += tripLine.TotalPrice;
                }
                return sum == TotalAmountCalculated;// this.TotalAmount;
            }
        }

        [DataMember]
        [Display(Name = "CrewSharePercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewSharePercentage { get; set; } //TIL_MANN_PCT

        [DataMember]
        [Display(Name = "CrewSharePart", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewSharePart { get; set; }

        [Obsolete("Use CrewShareMoneyCalculated instead")]
        [DataMember]
        public Nullable<decimal> CrewShareMoney { get; set; } //TIL_MANN_KR

        [DataMember]
        [NotMapped]
        [Display(Name = "CrewShareMoney", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewShareMoneyCalculated //TIL_MANN_KR
        {
            get
            {
                var pct = this.CrewSharePercentage.HasValue ? this.CrewSharePercentage.Value / 100m : 0m;
                var totalAmount = this.TotalAmountCalculated.HasValue ? this.TotalAmountCalculated.Value : 0m;
                return (totalAmount - this.DeductionsTotal) * pct - this.FRADRAG2;
            }
        }

        [DataMember]
        [NotMapped]
        [Display(Name = "CrewShareMoneyPerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewShareMoneyPerDay
        {
            get
            {
                if (CrewShareMoneyCalculated.HasValue && Days > 0)
                {
                    return CrewShareMoneyCalculated.Value / (decimal)Days;
                }
                return 0m;
            }
        }

        //[DataMember]
        //public Nullable<int> LOBNR { get; set; }

        // todo: what is this?
        [DataMember]
        public string VIDAR { get; set; }

        //[DataMember]
        //public Nullable<int> LN_IALT { get; set; }

        //[DataMember]
        //public Nullable<int> ANT_LN_TXT { get; set; }

        //[DataMember]
        //public Nullable<int> TUR_NR { get; set; }

        [NotMapped]
        [DataMember]
        [Display(Name = "Days", ResourceType = typeof(BootstrapResources.Resources))]
        public double Days //DAGAR
        {
            get
            {
                if (To.HasValue && From.HasValue)
                {
                    return To.Value.Subtract(From.Value).TotalDays + 1;
                }
                return 0;
            }
        }

        //[DataMember]
        //public Nullable<int> ID_REDERI { get; set; }

        [DataMember]
        [Display(Name = "Crew", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> Crew { get; set; } //MANNING

        /// <summary>
        /// Amount of crew member signons, which gets paid including those staying at home
        /// </summary>
        [DataMember]
        [Display(Name = "CrewIncludingStayingAtHome", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> CrewIncludingStayingAtHome { get; set; } // MANNING_I

        //todo: What is this used for?
        [DataMember]
        public Nullable<decimal> MANNING_X { get; set; }

        [Obsolete("Not used anymore")]
        [DataMember]
        public Nullable<decimal> SKIP_STUD { get; set; }

        [Obsolete("Not used anymore")]
        [DataMember]
        public Nullable<decimal> MAN_STUD { get; set; }

        // always zero
        //[DataMember]
        //public Nullable<decimal> TILSKOT { get; set; }

        [DataMember]
        [NotMapped]
        public decimal DeductionsTotal
        {
            get
            {
                var sum = 0m;
                foreach (var deduction in TripDeductions.FilterDeleted())
                {
                    if (deduction.TotalPrice.HasValue)
                    {
                        sum += deduction.TotalPrice.Value;
                    }
                }
                return sum;
            }
        }

        // todo: remove this property?
        [Obsolete("Use the DeductionsTotal property instead")]
        [DataMember]
        public Nullable<decimal> FRADRAG { get; set; }

        // todo: remove this property??
        [Obsolete()]
        [DataMember]
        public Nullable<decimal> TIL_MANN_TOT { get; set; } // CrewShareMoney + MAN_STUD

        [Obsolete()]
        [DataMember]
        public Nullable<decimal> FRADRAG2 { get; set; }

        [Obsolete("Use: CrewSharePerCrewMember instead!")]
        [DataMember]
        public Nullable<decimal> MANN_PART { get; set; }

        [NotMapped]
        [DataMember]
        [Display(Name = "CrewSharePerCrewMember", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewSharePerCrewMember
        {
            get
            {
                //var crew = (this.CrewIncludingStayingAtHome.HasValue && this.CrewIncludingStayingAtHome.Value > 0) ? this.CrewIncludingStayingAtHome.Value : 1m;
                var crew = (this.Crew.HasValue && this.Crew.Value > 0) ? this.Crew.Value : 1m;
                crew = (this.CrewSharePart.HasValue && this.CrewSharePart.Value > 0) ? this.CrewSharePart.Value : crew;
                if (this.CrewShareMoneyCalculated.HasValue)
                {
                    return Math.Round(this.CrewShareMoneyCalculated.Value / crew, 4);
                }
                return 0m;
            }
        }

        [NotMapped]
        [DataMember]
        [Display(Name = "CrewSharePerCrewMember", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? CrewPartPerCrewMember
        {
            get
            {
                var crew = (this.Crew.HasValue && this.Crew.Value > 0) ? this.Crew.Value : 1m;
                //crewIncludingStayingAtHome = this.CrewSharePart.HasValue ? this.CrewSharePart.Value : crewIncludingStayingAtHome;
                if (this.CrewShareMoneyCalculated.HasValue)
                {
                    return Math.Round(this.CrewShareMoneyCalculated.Value / crew, 4);
                }
                return 0m;
            }
        }

        [NotMapped]
        [DataMember]
        public decimal? PerDay
        {
            get
            {
                //var crewShare = this.CrewSharePerCrewMember.HasValue ? this.CrewSharePerCrewMember.Value : 0m;
                var crewShare = this.CrewPartPerCrewMember.HasValue ? this.CrewPartPerCrewMember.Value : 0m;
                if (this.Days > 0)
                {
                    return Math.Round(crewShare / (decimal)this.Days, 4);
                }
                return 0m;
            }
        }

        //todo: remove this property??
        [Obsolete("Use Trip.PerDay instead!")]
        [DataMember]
        public Nullable<decimal> PR_DAG { get; set; }

        [DataMember]
        public Nullable<decimal> MinimumWage { get; set; }

        [DataMember]
        public Nullable<decimal> DAGLON { get; set; }

        [DataMember]
        public string SKIB_TXT { get; set; }

        [DataMember]
        public Nullable<decimal> TOTAL_KR { get; set; }

        [DataMember]
        public Nullable<decimal> MANN_PART_IS { get; set; }

        [DataMember]
        public Nullable<int> MID_AR { get; set; }

        [DataMember]
        [Display(Name = "CrewShare", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> MANN_PART_I { get; set; }

        [DataMember]
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        [DataMember]
        public string PAR_ART { get; set; }

        [DataMember]
        public string PAR_TUR_ID { get; set; }

        [DataMember]
        public string ALT_MP { get; set; }

        [DataMember]
        public Nullable<int> ALT_MA { get; set; }

        [DataMember]
        public string CHECK { get; set; }

        [DataMember]
        public Nullable<int> TRYG_ANT { get; set; }

        [DataMember]
        public Nullable<decimal> LaborInsurance { get; set; } //TRYG_KR

        [DataMember]
        [NotMapped]
        public decimal? LaborInsuranceTotal
        {
            get
            {
                return LaborInsurance.HasValue ? new Nullable<decimal>(LaborInsurance.Value * (decimal)Days) : 0m;
            }
        }

        [DataMember]
        public Nullable<decimal> TRYG_KRHB { get; set; }

        [DataMember]
        public string TRYG_KVIT { get; set; }

        [DataMember]
        public Nullable<int> TRYG_BILAG { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TRYG_DATO { get; set; }

        //[DataMember]
        //public string PortOfLanding { get; set; } //AVR_STAD

        [ForeignKey("PortOfLanding")] // AVR_STAD
        [DataMember]
        public Nullable<int> PortOfLandingId { get; set; }
        [Display(Name = "PortOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Company PortOfLanding { get; set; } // P600

        [DataMember]
        [Display(Name = "DateOfLanding", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<System.DateTime> DateOfLanding { get; set; } //AVR_DATO

        //[ForeignKey("ShippingCompany")] //ID_REDERI
        //[DataMember]
        //public Nullable<int> ShippingCompanyId { get; set; }
        //public virtual ShippingCompany ShippingCompany { get; set; } //REIDARI

        // ship has a Shipping Company & ShipType
        //[ForeignKey("Ship")] //ID_SKIP
        [DataMember]
        [ForeignKey("Ship")]
        [Display(Name = "Ship", ResourceType = typeof(BootstrapResources.Resources))]
        public int? ShipId { get; set; }
        [Display(Name = "Ship", ResourceType = typeof(BootstrapResources.Resources))]
        [ForeignKey("ShipId")]
        public virtual Ship Ship { get; set; } // SKIB1


        [DataMember]
        [ForeignKey("PairShip")]
        [Display(Name = "Ship", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<int> PairShipId { get; set; }
        [ForeignKey("PairShipId")]
        [Display(Name = "PairShip", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Ship PairShip { get; set; }  //PAR_SKIP

        [NotMapped]
        [DataMember]
        [Display(Name = "Address", ResourceType = typeof(BootstrapResources.Resources))]
        public Address PrimaryAddress
        {
            get
            {
                if (Ship != null && Ship.Entity != null)
                {
                    return Ship.Entity.PrimaryAddress;
                }
                return null;
            }
        }

        //public virtual ICollection<FR_LINE> FR_LINE { get; set; }
        //public virtual ICollection<LINE> LINEs { get; set; }
        //public virtual ICollection<MA_LINE> MA_LINE { get; set; }
        //public virtual ICollection<MANNING> MANNINGs { get; set; }
        public virtual ICollection<SignOn> SignOns { get; set; }
        public ICollection<MI2_HOVD> MI2_HOVD { get; set; }
        public ICollection<ShippingCompany> ShippingCompanies { get; set; }
        public ICollection<TripDeduction> TripDeductions { get; set; }
        public ICollection<TripLine> TripLines { get; set; }
        //public virtual ICollection<TR_STUD> TR_STUD { get; set; }
        public ICollection<TripCrewAid> TripCrewAid { get; set; }
        public ICollection<TripShipAid> TripShipAid { get; set; }
        //public virtual ICollection<TR_TXT> TR_TXT { get; set; }
        //public virtual ICollection<TRANS_TR> TRANS_TR { get; set; }

        public Trip()
        {
            //this.FR_LINE = new HashSet<FR_LINE>(); // Not in use (no data in DB)
            //this.LINEs = new HashSet<LINE>(); // Not found in DB or any FRM file
            //this.MA_LINE = new HashSet<MA_LINE>(); // No data in DB or found in any FRM file
            //this.MANNINGs = new HashSet<MANNING>()

            this.SignOns = new HashSet<SignOn>();
            this.MI2_HOVD = new HashSet<MI2_HOVD>();
            this.ShippingCompanies = new HashSet<ShippingCompany>(); // REIDARI
            this.TripDeductions = new HashSet<TripDeduction>(); //OK
            this.TripLines = new HashSet<TripLine>();
            //this.TR_STUD = new HashSet<TR_STUD>(); // Not in DB
            this.TripCrewAid = new HashSet<TripCrewAid>(); //OK
            this.TripShipAid = new HashSet<TripShipAid>(); //OK
            //this.TR_TXT = new HashSet<TR_TXT>(); //OK
            //this.TRANS_TR = new HashSet<TRANS_TR>(); //OK
        }
    }
}