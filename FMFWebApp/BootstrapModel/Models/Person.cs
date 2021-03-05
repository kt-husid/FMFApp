using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Person : IHasChangeEvent, IPerson, IHasEntity //IHasEntityWithMore, IPerson // 
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [ForeignKey("Entity")]
        [DataMember]
        public int? EntityId { get; set; }
        [DataMember]
        public virtual Entity Entity { get; set; }

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

        [Display(Name = "FullName", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        //[NotMapped]
        public string FullName { get; set; }
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(MiddleName))
        //        {
        //            return FirstName + " " + LastName;
        //        }
        //        return FirstName + " " + MiddleName + " " + LastName;
        //    }
        //}

        [DataMember]
        [Display(Name = "Birthday", ResourceType = typeof(BootstrapResources.Resources))]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "DateRequired")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [DataMember]
        [Display(Name = "IsAlive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsAlive { get; set; }

        [ForeignKey("Gender")]
        [Display(Name = "Gender", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public int? GenderId { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonGender Gender { get; set; }

        [ForeignKey("Title")]
        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        [DataMember]
        public int? TitleId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual PersonTitle Title { get; set; }

        //public virtual ICollection<Company> Companies { get; set; }

        public Person()
        {
            //Companies = new HashSet<Company>();
        }
    }
}