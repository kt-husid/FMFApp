using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class TripViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        public int TripId { get; set; } //ID_TUR

        public Nullable<System.DateTime> From { get; set; } //FRA_DATO

        public Nullable<System.DateTime> To { get; set; } //TIL_DATO

        [Obsolete("Use Trip.Id instead")]
        public Nullable<int> TripNumber { get; set; } //TR_NR

        public Nullable<int> PairTrawlerDocumentId { get; set; } //PAR_SKJAL

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

        public string ShipTypeCode { get; set; } // SK_ID

        public int? ShipTypeId { get; set; }
        public virtual ShipType ShipType { get; set; }

        [Obsolete("Use instead: Trip.TotalWeightCalculated() !!!")]
        public Nullable<decimal> TotalWeight { get; set; } // KG_IALT

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

        public Nullable<decimal> TotalAmount { get; set; } //UPPH_IALT

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

        public Nullable<decimal> CrewSharePercentage { get; set; } //TIL_MANN_PCT

        public Nullable<decimal> CrewSharePart { get; set; }

        public Nullable<decimal> CrewShareMoney { get; set; } //TIL_MANN_KR

        public Nullable<decimal> CrewShareMoneyCalculated //TIL_MANN_KR
        {
            get
            {
                var pct = this.CrewSharePercentage.HasValue ? this.CrewSharePercentage.Value / 100m : 0m;
                var totalAmount = this.TotalAmountCalculated.HasValue ? this.TotalAmountCalculated.Value : 0m;
                return (totalAmount - this.DeductionsTotal) * pct - this.FRADRAG2;
            }
        }

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

        public string VIDAR { get; set; }

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

        public Nullable<decimal> Crew { get; set; } //MANNING

        public Nullable<decimal> CrewIncludingStayingAtHome { get; set; } // MANNING_I

        public Nullable<decimal> MANNING_X { get; set; }

        public Nullable<decimal> SKIP_STUD { get; set; }

        public Nullable<decimal> MAN_STUD { get; set; }

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

        public Nullable<decimal> FRADRAG { get; set; }

        public Nullable<decimal> TIL_MANN_TOT { get; set; } // CrewShareMoney + MAN_STUD

        public Nullable<decimal> FRADRAG2 { get; set; }

        public Nullable<decimal> MANN_PART { get; set; }

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

        public Nullable<decimal> PR_DAG { get; set; }

        public Nullable<decimal> MinimumWage { get; set; }

        public Nullable<decimal> DAGLON { get; set; }

        
        public string SKIB_TXT { get; set; }

        
        public Nullable<decimal> TOTAL_KR { get; set; }

        
        public Nullable<decimal> MANN_PART_IS { get; set; }

        
        public Nullable<int> MID_AR { get; set; }

        public Nullable<decimal> MANN_PART_I { get; set; }

        
        public Nullable<int> EmployerNumber { get; set; } // ARB_NR

        
        public string PAR_ART { get; set; }

        
        public string PAR_TUR_ID { get; set; }

        
        public string ALT_MP { get; set; }

        
        public Nullable<int> ALT_MA { get; set; }

        
        public string CHECK { get; set; }

        
        public Nullable<int> TRYG_ANT { get; set; }

        
        public Nullable<decimal> LaborInsurance { get; set; } //TRYG_KR

        
        
        public decimal? LaborInsuranceTotal
        {
            get
            {
                return LaborInsurance.HasValue ? new Nullable<decimal>(LaborInsurance.Value * (decimal)Days) : 0m;
            }
        }

        
        public Nullable<decimal> TRYG_KRHB { get; set; }

        
        public string TRYG_KVIT { get; set; }

        
        public Nullable<int> TRYG_BILAG { get; set; }

        
        public Nullable<System.DateTime> TRYG_DATO { get; set; }

        public Nullable<int> PortOfLandingId { get; set; }
        public virtual Company PortOfLanding { get; set; } // P600

        public Nullable<System.DateTime> DateOfLanding { get; set; } //AVR_DATO
        
        public int? ShipId { get; set; }
        public virtual Ship Ship { get; set; } // SKIB1

        public Nullable<int> PairShipId { get; set; }
        public virtual Ship PairShip { get; set; }  //PAR_SKIP

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

        public virtual ICollection<SignOn> SignOns { get; set; }
        public ICollection<MI2_HOVD> MI2_HOVD { get; set; }
        public ICollection<ShippingCompany> ShippingCompanies { get; set; }
        public ICollection<TripDeduction> TripDeductions { get; set; }
        public ICollection<TripLine> TripLines { get; set; }
        public ICollection<TripCrewAid> TripCrewAid { get; set; }
        public ICollection<TripShipAid> TripShipAid { get; set; }

        public TripViewModel()
        {
            this.SignOns = new HashSet<SignOn>();
            this.MI2_HOVD = new HashSet<MI2_HOVD>();
            this.ShippingCompanies = new HashSet<ShippingCompany>(); // REIDARI
            this.TripDeductions = new HashSet<TripDeduction>(); //OK
            this.TripLines = new HashSet<TripLine>();
            this.TripCrewAid = new HashSet<TripCrewAid>(); //OK
            this.TripShipAid = new HashSet<TripShipAid>(); //OK
        }
    }
}