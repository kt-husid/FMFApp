using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BootstrapWebApplication.Interfaces;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    /// <summary>
    /// Limir (P100.DEF og P100.IB5)
    /// </summary>
    [DataContract]
    public partial class Member : IHasStuffWithPersonAndChangeEvent//IHasChangeEvent, IHasPerson, IHasStuff //IHasEntityWithMore
    {
        //              OKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOK    OKOKOKOKOKOKOKOKOKOKOKOK OKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOK OK      OK      OK                           OK      OKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOKOK OK        OK             OK          OK      OK          OK       OK                                              
        //       ???    ------------------PERSON------------------    -------ADDRESSA--------- -------CHANGESET-------------------- ??????? ??????? PersonGender ??????? ??????? ??????? ---------CHANGESET-------------------------- COMMENT?  --PHONE-- ???? ????????    PERSON  JobTitle    ???????? ???????? ???????? ????? ????    ?????   ???????? OK           OK                                                 OK
        //ID    ,NR     FODT         ,FNAVN             ,ENAVN       ,ADR1             ,POSTNR,RET_DATO    ,RET_HH ,RET_MM ,RET_ID ,FN5    ,STRIKA ,KYN         ,PRI_OWN,BETALER,EN5    ,OPR_DATO    ,OPR_HH,OPR_MM ,OPR_ID ,M_STATUS,TXT_LN   ,TLF_HEIMA,TYPE ,ENFO       ,CPR    ,STARV      ,BURT_DG,TUR_IALT,INN_IALT,DG   ,MD      ,STAT  ,TXT_DATO,LAST_DATO   ,MNAVN,MI_TOT,LG_TOT ,MI_AR,MI_ID  ,LG_AR,LG_KR    ,LAST_ID,ETI_DATO    ,ETI_HH,ETI_MM ,ETI_ID ,SLAG,TRANSPORT,LG_IALT ,LAST_KONTO,LAST_REG,FKOTA
        //1     ,2000   ,"07/07/1971","Mads"            ,"Arendal"   ,"›sterbrogade 90",2100  ,"08/02/1992","12"   ,"22"   ,"oj"   ,"Mads" ,""     ,"M"         ,0      ,0      ,"Aren" ,"20/01/1992","16"  ,"19"   ,"rc"   ,""      ,0        ,0        ,0    ,"AreMad"   ,59     ,""         ,0.00   ,0.00    ,0.00    ,"07" ,"07"    ,""    ,""      ,""          ,""   ,0     ,2      ,""   ,""     ,""   ,0.00     ,""     ,"31/03/1998","10"  ,"01"   ,""     ,"ff",0.00     ,0.00    ,1990162   ,9181    ,""
        //15329 ,26200  ,"28/09/1963","Bogi Martinsson" ,"Hansen"    ,"Torkilsg›ta 20" ,188   ,"26/11/2013","08"   ,"38"   ,"di"   ,"Bogi" ,""     ,"M"         ,0      ,0      ,"Hans" ,"22/11/113" ,"14"  ,"07"   ,"hj"   ,""      ,0        ,0        ,0    ,"HanBog"   ,11     ,"de"       ,0.00   ,0.00    ,0.00    ,"28" ,"09"    ,""    ,""      ,""          ,""   ,1     ,0      ,"13" ,""     ,""   ,0.00     ,""     ,""          ,""    ,""     ,""     ,"ff",0.00     ,0.00    ,0         ,0       ,""
        //14    ,2013   ,"20/10/1959","Poul Knud"       ,"Ejdesgaard","Kastalag 11"    ,470   ,"08/12/2008","06"   ,"36"   ,"hj"   ,"Poul" ,""     ,"M"         ,0      ,2791   ,"Ejde" ,"20/01/1992","16"  ,"19"   ,"rc"   ,"J"     ,0        ,0        ,0    ,"EjdPou"   ,11     ,"de"       ,45.00  ,4.00    ,0.00    ,"20" ,"10"    ,""    ,""      ,"06/03/2014",""   ,4     ,17     ,"99" ,"KG477","00" ,1395.78  ,"KG477","22/11/2013","14"  ,"42"   ,""     ,"ff",0.00     ,0.00    ,4020946   ,9181    ,"1"



        //BURT_DG,TUR_IALT,INN_IALT,DG   ,MD      ,STAT  ,TXT_DATO,LAST_DATO   ,MNAVN,MI_TOT,LG_TOT ,MI_AR,MI_ID  ,LG_AR,LG_KR    ,LAST_ID,ETI_DATO    ,ETI_HH,ETI_MM ,ETI_ID ,SLAG,TRANSPORT,LG_IALT ,LAST_KONTO,LAST_REG,FKOTA
        //0.00   ,0.00    ,0.00    ,"07" ,"07"    ,""    ,""      ,""          ,""   ,0     ,2      ,""   ,""     ,""   ,0.00     ,""     ,"31/03/1998","10"  ,"01"   ,""     ,"ff",0.00     ,0.00    ,1990162   ,9181    ,""
        //45.00  ,4.00    ,0.00    ,"20" ,"10"    ,""    ,""      ,"06/03/2014",""   ,4     ,17     ,"99" ,"KG477","00" ,1395.78  ,"KG477","22/11/2013","14"  ,"42"   ,""     ,"ff",0.00     ,0.00    ,4020946   ,9181    ,"1"

        //[Key]
        //[DataMember]
        //public int Id { get; set; }

        [DataMember]
        public int NR { get; set; }

        //// FODT, FNAVN, ENAVN, KYN
        //[DataMember]
        //[ForeignKey("Person")]
        //public int? PersonId { get; set; }
        //[Display(Name = "Person", ResourceType = typeof(BootstrapResources.Resources))]
        ////[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        //public virtual Person Person { get; set; }

        [NotMapped]
        [DataMember]
        public bool IsAlive
        {
            get
            {
                if (Person != null)
                {
                    return Person.IsAlive;
                }
                return false;
            }
        }

        [NotMapped]
        [DataMember]
        public string FullName
        {
            get
            {
                if (Person != null)
                {
                    return Person.FullName;
                }
                return String.Empty;
            }
        }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.DaysThisYear")]
        [DataMember]
        [Display(Name = "DaysThisYear", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> BURT_DG { get; set; }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.TripsThisYear")]
        [DataMember]
        [Display(Name = "TripsThisYear", ResourceType = typeof(BootstrapResources.Resources))]
        public Nullable<decimal> TUR_IALT { get; set; }


        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.LastSignOnFromDate")]
        //todo: remove this and use the BLL code instead which retreives the last signon date
        [DataMember]
        public Nullable<System.DateTime> LAST_DATO { get; set; }


        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.LastSignOnShipCode")]
        [DataMember]
        [Display(Name = "Last", ResourceType = typeof(BootstrapResources.Resources))]
        public string LAST_ID { get; set; }

        //[DataMember]
        //public Nullable<decimal> INN_IALT { get; set; }

        //todo: remove this from Member and make a new class using BURT_DG, TUR_IALT, LAST_DATO and LAST_ID ?
        // LAST_DATO, BURT_DG, TUR_IALT, LAST_ID
        [ForeignKey("LastSignOn")]
        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.LastSignOnFromDate & json.LastSignOnShipCode")]
        public int? LastSignOnId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.LastSignOnFromDate & json.LastSignOnShipCode")]
        [DataMember]
        public virtual SignOn LastSignOn { get; set; }

        [Obsolete("Use instead Job")]
        [DataMember]
        public string JobTitle { get; set; } // STARV

        [ForeignKey("Job")]
        [DataMember]
        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public int? JobId { get; set; }
        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Job Job { get; set; }

        //// stat
        //[ForeignKey("Status")]
        //[DataMember]
        //public int? StatusId { get; set; }
        //[Display(Name = "Status", ResourceType = typeof(BootstrapResources.Resources))]
        //public virtual Status Status { get; set; }

        //[ForeignKey("Postal")]
        //[DataMember]
        //public Nullable<int> PostalId { get; set; }
        //public virtual Postal Postal { get; set; }

        [ForeignKey("MemberType")]
        [DataMember]
        public int? MemberTypeId { get; set; }
        [Display(Name = "MembershipType", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public virtual MemberType MemberType { get; set; } //LSLAG

        //public virtual LIMART LIMART { get; set; }
        //public virtual TURHEAD TURHEAD { get; set; }
        //public Nullable<int> FK_LIMART_Id { get; set; }
        //public string STRIKA { get; set; }
        [DataMember]
        public Nullable<int> PRI_OWN { get; set; }

        [DataMember]
        public Nullable<int> BETALER { get; set; }

        [DataMember]
        public string M_STATUS { get; set; }

        //todo: verify this is calculated (see  /api/Member/GetStatistics (property PaymentsTotal and SignOnsThisYear in MemberStatisticsViewModel))
        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.SignOnsThisYear")]
        [DataMember]
        public Nullable<int> MI_TOT { get; set; }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.PaymentsTotal")]
        [DataMember]
        public Nullable<int> LG_TOT { get; set; }

        [Obsolete("Use GET: /api/Member/GetStatistics and $('#lastSignOnYear').html(moment(json.SignOn.From).format('YY')); instead")]
        [DataMember]
        public string MI_AR { get; set; }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.LastSignOnShipCode")]
        [DataMember]
        public string MI_ID { get; set; }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & moment(json.PaymentsYear).format('YY')")]
        [DataMember]
        public string LG_AR { get; set; }

        [Obsolete("Use instead GET: /api/Member/GetStatistics & json.PaymentsSum")]
        [DataMember]
        public Nullable<decimal> LG_KR { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ETI_DATO { get; set; }

        [DataMember]
        public string ETI_ID { get; set; }
        
        [NotMapped]
        [DataMember]
        public string MemberTypeText
        {
            get
            {
                if (MemberType != null)
                {
                    return MemberType.Description;
                }
                return "";
            }
        }

        [NotMapped()]
        public string DG
        {
            get
            {
                if (Person != null && Person.Birthday.HasValue)
                {
                    return Person.Birthday.Value.Day.ToString();
                }
                return "";
            }
        }

        [NotMapped()]
        public string MD
        {
            get
            {
                if (Person != null && Person.Birthday.HasValue)
                {
                    return Person.Birthday.Value.Month.ToString();
                }
                return "";
            }
        }

        [NotMapped()]
        public string FN5
        {
            get
            {
                if (Person != null && Person.FirstName.Length >= 4)
                {
                    return Person.FirstName.Substring(0, 4).ToUpperFirst();
                }
                return null;
            }
        }

        [NotMapped()]
        public string EN5
        {
            get
            {
                if (Person != null && Person.LastName.Length >= 4)
                {
                    return Person.LastName.Substring(0, 4).ToUpperFirst();
                }
                return null;
            }
        }

        [NotMapped()]
        public string ENFO
        {
            get
            {
                if (Person != null && Person.FirstName.Length >= 4 && Person.LastName.Length >= 4)
                {
                    return Person.LastName.Substring(0, 3).ToUpperFirst() + Person.FirstName.Substring(0, 3).ToUpperFirst();
                }
                return null;
            }
        }

        [DataMember]
        public string MemberTypeCode { get; set; }
        [DataMember]
        public string MemberTypeDescription { get; set; }
        [DataMember]
        public string JobCode { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string GenderCode { get; set; }
        [DataMember]
        [NotMapped]
        public DateTime? Birthday
        {
            get
            {
                if (Person != null)
                {
                    return Person.Birthday;
                }
                return null;
            }
        }
        // only used with report Alka (a1709)
        [DataMember]
        public int? Age { get; set; }

        // LAST_KONTO,LAST_REG,
        //[NotMapped()]
        //public BankAccount LastUsedBankAccount { get; set; }
        //[ForeignKey("PrimaryBankAccount")]
        //public int? PrimaryBankAccountId { get; set; }
        //public virtual BankAccount PrimaryBankAccount { get; set; }
        [NotMapped]
        [DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public BankAccount LastUsedBankAccount
        {
            get
            {
                return Person.Entity.PrimaryBankAccount;// Person.LastUsedBankAccount;
            }
        }

        [NotMapped]
        //[DataMember]
        public ICollection<BankAccount> BankAccounts
        {
            get
            {
                return Person.Entity.BankAccounts;// Person.BankAccounts;
            }
        }

        //public virtual ICollection<frt_lon7> frt_lon7 { get; set; }
        public virtual ICollection<DEB_CON> DEB_CON { get; set; }
        public virtual ICollection<HolidayPay_HOVD> HolidayPay_HOVD { get; set; } //DB
        public virtual ICollection<HolidayPay> HolidayPay { get; set; } //DB
        public virtual ICollection<MemberPayment> Payments { get; set; } //DB
        public virtual ICollection<MemberPayment2> LGJALD2 { get; set; }
        public virtual ICollection<MemberFinancialAid> FinancialAids { get; set; }
        //public virtual ICollection<LO_LINE> LO_LINE { get; set; }

        [InverseProperty("Member")]
        public virtual ICollection<SignOn> SignOns { get; set; }
        public virtual ICollection<MI2_HOVD> MI2_HOVD { get; set; } //DB
        public virtual ICollection<P400> P400 { get; set; }
        public virtual ICollection<MemberComment> Comments { get; set; }
        //public virtual ICollection<TLF_IN> TLF_IN { get; set; }
        //public virtual ICollection<TRANS_P> TRANS_P { get; set; }
        //public virtual ICollection<TRANSPRT> TRANSPRTs { get; set; }

        public Member()
        {
            //this.frt_lon7 = new HashSet<frt_lon7>();
            this.DEB_CON = new HashSet<DEB_CON>();
            this.HolidayPay_HOVD = new HashSet<HolidayPay_HOVD>();
            this.HolidayPay = new HashSet<HolidayPay>();
            this.Payments = new HashSet<MemberPayment>();
            this.LGJALD2 = new HashSet<MemberPayment2>();
            this.FinancialAids = new HashSet<MemberFinancialAid>();
            //this.LO_LINE = new HashSet<LO_LINE>();
            this.SignOns = new HashSet<SignOn>();
            this.MI2_HOVD = new HashSet<MI2_HOVD>();
            this.P400 = new HashSet<P400>();
            this.Comments = new HashSet<MemberComment>();
            //this.TLF_IN = new HashSet<TLF_IN>();
            //this.TRANS_P = new HashSet<TRANS_P>();
            //this.TRANSPRTs = new HashSet<TRANSPRT>();
        }

        public string Status { get; set; }
    }
}