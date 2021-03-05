using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class Website : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public String Name { get; set; }

        [DataMember]
        [Display(Name = "Description", ResourceType = typeof(BootstrapResources.Resources))]
        public String Description { get; set; }

        [DataMember]
        [Display(Name = "Uri", ResourceType = typeof(BootstrapResources.Resources))]
        [NotMapped()]
        public Uri Uri
        {
            get
            {
                Uri uri = null;
                Uri.TryCreate(UriString, UriKind.RelativeOrAbsolute, out uri);
                return uri;
            }
            set
            {
                if (value != null)
                {
                    UriString = value.ToString();
                }
            }
        }

        [Column("Uri")]
        [Display(Name = "Uri", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "InputRequired")]
        [MaxLength(2083, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "InvalidLength")]
        [MinLength(1, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "InvalidLength")]
        [DataType(DataType.Url)]
        public string UriString { get; set; }

        //[ForeignKey("Person")]
        //[DataMember]
        //public int PersonId { get; set; }
        //public virtual Person Person { get; set; }

        [DataMember]
        [ForeignKey("Entity")]
        public int? EntityId { get; set; }
        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        public virtual Entity Entity { get; set; }

        [DataMember]
        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }
    }
}