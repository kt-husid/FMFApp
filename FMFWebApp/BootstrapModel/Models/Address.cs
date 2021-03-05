using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Address : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        [Display(Name = "Address",  ResourceType = typeof(BootstrapResources.Resources))]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "CareOf", ResourceType = typeof(BootstrapResources.Resources))]
        public string CareOf { get; set; }

        [DataMember]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "AddressLine1", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine1 { get; set; }

        [DataMember]
        [Display(Name = "AddressLine2", ResourceType = typeof(BootstrapResources.Resources))]
        public string AddressLine2 { get; set; }

        [DataMember]
        [Display(Name = "IsActive", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsActive { get; set; }

        [DataMember]
        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }

        [DataMember]
        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        [ForeignKey("Postal")]
        public int? PostalId { get; set; }
        [DataMember]
        [Display(Name = "Postal", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Postal Postal { get; set; }
        
        //[DataMember]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[Display(Name = "ZipCode", ResourceType = typeof(BootstrapResources.Resources))]
        //public string ZipCode { get; set; }

        //[DataMember]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[Display(Name = "City", ResourceType = typeof(BootstrapResources.Resources))]
        //public string CityName { get; set; }

        [DataMember]
        [Display(Name = "StateProvinceRegion", ResourceType = typeof(BootstrapResources.Resources))]
        public string StateProvinceRegion { get; set; }
        
        //[ForeignKey("City")]
        //public int CityId { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [DataMember]
        [ForeignKey("Entity")]
        public int? EntityId { get; set; }
        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        public virtual Entity Entity { get; set; }

        //[DataMember]
        //[ForeignKey("EntityBase")]
        //public int? EntityBaseId { get; set; }
        ////[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        //[Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        //public virtual EntityBase EntityBase { get; set; }

        //[ForeignKey("AddressOwnershipInfo")]
        //public int AddressOwnershipInfoId { get; set; }

        //public virtual City City { get; set; }


        //public virtual AddressOwnershipInfo AddressOwnershipInfo { get; set; }
    }
}