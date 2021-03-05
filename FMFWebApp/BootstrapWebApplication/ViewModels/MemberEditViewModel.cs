using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberEditViewModel : ViewModelBaseWithChangeEvent
    {
        public int Id { get; set; }

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
        [Display(Name = "Gender", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonGender Gender { get; set; }

        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public int? TitleId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonTitle Title { get; set; }

        [DataMember]
        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public int? JobId { get; set; }
        [Display(Name = "Job", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Job Job { get; set; }

        [DataMember]
        [Display(Name = "MembershipType", ResourceType = typeof(BootstrapResources.Resources))]
        public int MemberTypeId { get; set; }
        [Display(Name = "MembershipType", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public virtual MemberType MemberType { get; set; } //LSLAG

        [DataMember]
        public string JobCode { get; set; }

        [DataMember]
        public string MemberTypeCode { get; set; }

        [DataMember]
        [Display(Name = "IsAlive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsAlive { get; set; }
    }
}