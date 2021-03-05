﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public partial class ChangeEvent : IHasId
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "CreatedOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        [Display(Name = "UpdatedOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime UpdatedOn { get; set; }

        [DataMember]
        [Display(Name = "DeletedOn", ResourceType = typeof(BootstrapResources.Resources))]
        public DateTime? DeletedOn { get; set; }

        [DataMember]
        public string CreatedByUserIdCode { get; set; }
        
        [DataMember]
        public string UpdatedByUserIdCode { get; set; }

        [DataMember]
        public string DeletedByUserIdCode { get; set; }

        [NotMapped]
        [DataMember]
        public bool IsDeleted
        {
            get
            {
                return DeletedOn != null;// && !String.IsNullOrEmpty(DeletedByUserIdCode);
            }
        }

        [DataMember]
        public ICollection<ChangeEventItem> ChangeEventItems { get; set; }

        //public ChangeEvent()
        //{
        //    ChangeEventItems = new HashSet<ChangeEventItem>();
        //}
        
    }
}