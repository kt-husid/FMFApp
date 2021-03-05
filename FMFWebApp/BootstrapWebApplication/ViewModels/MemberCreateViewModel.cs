using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberCreateViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

        //[DataMember]
        //public int? EntityId { get; set; }
        //[DataMember]
        //public virtual Entity Entity { get; set; }

        [DataMember]
        [Display(Name = "SSN", ResourceType = typeof(BootstrapResources.Resources))]
        public string SSN { get; set; }

        [DataMember]
        [Display(Name = "FirstName", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "FirstNameRequired")]
        //[MaxLength(50, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "FirstNameValidLength")]
        //[MinLength(3, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "FirstNameValidLength")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "MiddleName", ResourceType = typeof(BootstrapResources.Resources))]
        //[MaxLength(50, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "MiddleNameValidLength")]
        public string MiddleName { get; set; }

        [DataMember]
        [Display(Name = "LastName", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "LastNameRequired")]
        //[MaxLength(50, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "LastNameValidLength")]
        //[MinLength(3, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "LastNameValidLength")]
        public string LastName { get; set; }


        [DataMember]
        [Display(Name = "Birthday", ResourceType = typeof(BootstrapResources.Resources))]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "DateRequired")]
        //[DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public int? GenderId { get; set; }
        //[Display(Name = "Gender", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonGender Gender { get; set; }

        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public int? TitleId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "TitleRequired")]
        //[Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonTitle Title { get; set; }

        //public string JobTitle { get; set; }

        [DataMember]
        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public int? JobId { get; set; }
        //[Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Job Job { get; set; }

        [DataMember]
        [Display(Name = "MembershipType", ResourceType = typeof(BootstrapResources.Resources))]
        public int MemberTypeId { get; set; }
        //[Display(Name = "MembershipType", ResourceType = typeof(BootstrapResources.Resources))]
        ////[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public virtual MemberType MemberType { get; set; } //LSLAG

        [DataMember]
        public Nullable<int> PRI_OWN { get; set; }

        [DataMember]
        public Nullable<int> BETALER { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public string PhoneNumber { get; set; }

        [Display(Name = "AddressLine1", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        public int? PostalId { get; set; }
        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public virtual Postal Postal { get; set; }

        [DataMember]
        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        public int CountryId { get; set; }
        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public virtual Country Country { get; set; }

        [DataMember]
        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int? AccountNumber { get; set; }

        [DataMember]
        [Display(Name = "Bank", ResourceType = typeof(BootstrapResources.Resources))]
        public int? BankId { get; set; }

        //[DataMember]
        //public string JobCode { get; set; }

        //[DataMember]
        //public string MemberTypeCode { get; set; }


        //[DataMember]
        //public string PostalCode { get; set; }

        //[DataMember]
        //public string CountryCode { get; set; }
    }
}