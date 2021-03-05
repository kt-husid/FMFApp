//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootstrapWebApplication.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kontolisti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kontolisti()
        {
            this.BokDim = new HashSet<BokDim>();
            this.Bokingar = new HashSet<Bokingar>();
            this.Bokingar1 = new HashSet<Bokingar>();
            this.BokUpp = new HashSet<BokUpp>();
            this.BokUpp1 = new HashSet<BokUpp>();
            this.BokUpp2 = new HashSet<BokUpp>();
            this.BokUpp3 = new HashSet<BokUpp>();
            this.BokUpp4 = new HashSet<BokUpp>();
            this.BokUpp5 = new HashSet<BokUpp>();
            this.BokUpp6 = new HashSet<BokUpp>();
            this.BokUpp7 = new HashSet<BokUpp>();
            this.BokUpp8 = new HashSet<BokUpp>();
            this.budgetkonto = new HashSet<budgetkonto>();
            this.emaillog = new HashSet<emaillog>();
            this.heimabankidef = new HashSet<heimabankidef>();
            this.hopflyting = new HashSet<hopflyting>();
            this.Kontolisti1 = new HashSet<Kontolisti>();
            this.KontoVidmerking = new HashSet<KontoVidmerking>();
            this.Kontolisti11 = new HashSet<Kontolisti>();
            this.MvgTypa1 = new HashSet<MvgTypa>();
            this.MvgTypa2 = new HashSet<MvgTypa>();
            this.MvgTypa3 = new HashSet<MvgTypa>();
            this.rel_konto_flokkar = new HashSet<rel_konto_flokkar>();
            this.rel_iusers_konto = new HashSet<rel_iusers_konto>();
            this.rel_konto_felt = new HashSet<rel_konto_felt>();
            this.rokn_kontogruppalinjir = new HashSet<rokn_kontogruppalinjir>();
            this.Ryk_aldur_tmp = new HashSet<Ryk_aldur_tmp>();
            this.Saldolisti = new HashSet<Saldolisti>();
            this.skuffukonto = new HashSet<skuffukonto>();
            this.skuffur = new HashSet<skuffur>();
            this.SMKassi = new HashSet<SMKassi>();
            this.SMKassi1 = new HashSet<SMKassi>();
            this.SMKassi2 = new HashSet<SMKassi>();
            this.SMKassi3 = new HashSet<SMKassi>();
            this.SMKassi4 = new HashSet<SMKassi>();
            this.SMKassi5 = new HashSet<SMKassi>();
            this.SMKassi6 = new HashSet<SMKassi>();
            this.SMKassi7 = new HashSet<SMKassi>();
            this.SMSedlahovd = new HashSet<SMSedlahovd>();
            this.SMSedlahovd1 = new HashSet<SMSedlahovd>();
            this.SMVorubolkur = new HashSet<SMVorubolkur>();
            this.SMVorubolkur1 = new HashSet<SMVorubolkur>();
            this.SMVorubolkur2 = new HashSet<SMVorubolkur>();
            this.SMVorubolkur3 = new HashSet<SMVorubolkur>();
            this.SMVorur = new HashSet<SMVorur>();
        }
    
        public int KontolistiId { get; set; }
        public int KontoNummar { get; set; }
        public string Navn { get; set; }
        public string Budstadur { get; set; }
        public string Budstadur2 { get; set; }
        public string Byur { get; set; }
        public string Land { get; set; }
        public string Kontaktpersonur { get; set; }
        public string Tlf { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Sperradur { get; set; }
        public int RaksturStatus { get; set; }
        public decimal Saldo { get; set; }
        public bool Dim1 { get; set; }
        public bool Dim2 { get; set; }
        public bool Dim3 { get; set; }
        public bool Dim4 { get; set; }
        public bool Dim5 { get; set; }
        public bool OgnarKartotek { get; set; }
        public bool Hagtol { get; set; }
        public bool Ovirkin { get; set; }
        public bool skuldari { get; set; }
        public bool ognari { get; set; }
        public string kontoslag { get; set; }
        public Nullable<int> bankaregnr { get; set; }
        public string bankakontonr { get; set; }
        public string stytting { get; set; }
        public decimal v_saldo { get; set; }
        public decimal h_saldo { get; set; }
        public bool kontoavritvidemail { get; set; }
        public string vtal { get; set; }
        public string debitorpassword { get; set; }
        public bool fakturavidemail { get; set; }
        public Nullable<System.DateTime> update_dato { get; set; }
        public Nullable<System.TimeSpan> update_klokkan { get; set; }
        public Nullable<int> GjaldoyraId { get; set; }
        public Nullable<int> EindirId { get; set; }
        public Nullable<int> MvgTypaId { get; set; }
        public Nullable<int> RentubolkurId { get; set; }
        public Nullable<int> AvgrGjaldsBolkurId { get; set; }
        public Nullable<int> UpdateUsersId { get; set; }
        public byte[] rv { get; set; }
        public string KontoNummarTxt { get; set; }
        public Nullable<int> Gjaldstreyt { get; set; }
        public Nullable<bool> MVG_skyldugur { get; set; }
        public Nullable<int> Leveringskonta { get; set; }
        public Nullable<decimal> AvslattarPct { get; set; }
        public Nullable<int> Fakturakonta { get; set; }
        public string GLN { get; set; }
        public int FakturarVid { get; set; }
    
        public virtual avgrgjaldsbolkur avgrgjaldsbolkur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokDim> BokDim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bokingar> Bokingar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bokingar> Bokingar1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BokUpp> BokUpp8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<budgetkonto> budgetkonto { get; set; }
        public virtual eindir eindir { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emaillog> emaillog { get; set; }
        public virtual gjaldoyra gjaldoyra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<heimabankidef> heimabankidef { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hopflyting> hopflyting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti1 { get; set; }
        public virtual Kontolisti Kontolisti2 { get; set; }
        public virtual SMGjaldstreyt SMGjaldstreyt { get; set; }
        public virtual MvgTypa MvgTypa { get; set; }
        public virtual rentubolkur rentubolkur { get; set; }
        public virtual users users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KontoVidmerking> KontoVidmerking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kontolisti> Kontolisti11 { get; set; }
        public virtual Kontolisti Kontolisti3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MvgTypa> MvgTypa1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MvgTypa> MvgTypa2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MvgTypa> MvgTypa3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rel_konto_flokkar> rel_konto_flokkar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rel_iusers_konto> rel_iusers_konto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rel_konto_felt> rel_konto_felt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rokn_kontogruppalinjir> rokn_kontogruppalinjir { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ryk_aldur_tmp> Ryk_aldur_tmp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Saldolisti> Saldolisti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skuffukonto> skuffukonto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skuffur> skuffur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMKassi> SMKassi7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSedlahovd> SMSedlahovd { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSedlahovd> SMSedlahovd1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorubolkur> SMVorubolkur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorubolkur> SMVorubolkur1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorubolkur> SMVorubolkur2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorubolkur> SMVorubolkur3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMVorur> SMVorur { get; set; }
    }
}