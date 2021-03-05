using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class BankAccount : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        //[DataMember]
        //public int RegNumber { get; set; }

        [ForeignKey("Bank")]
        [DataMember]
        public int? BankId { get; set; }
        [DataMember]
        public virtual Bank Bank { get; set; }
        
        [NotMapped]
        [DataMember]
        [Display(Name = "BankCode", ResourceType = typeof(BootstrapResources.Resources))]
        public int RegNumber
        {
            get
            {
                if (Bank != null)
                    return Bank.RegNumber;
                return 0;
            }
        }

        [DataMember]
        [Display(Name = "AccountNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public int AccountNumber { get; set; }

        [DataMember]
        [Display(Name = "IsPrimary", ResourceType = typeof(BootstrapResources.Resources))]
        public bool IsPrimary { get; set; }

        //[ForeignKey("Member")]
        //[DataMember]
        //public int? MemberId { get; set; }
        //[DataMember]
        //public virtual Member Member { get; set; }

        [DataMember]
        [ForeignKey("Entity")]
        public int? EntityId { get; set; }
        //[Display(Name = "Entity", ResourceType = typeof(BootstrapResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        //[DataMember]
        public virtual Entity Entity { get; set; }
    }
}