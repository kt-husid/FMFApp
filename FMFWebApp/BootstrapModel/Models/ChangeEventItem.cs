using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class ChangeEventItem : IHasChangeEvent, IHasEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        //[ForeignKey("ChangeEvent")]
        //[DataMember]
        //public int? ChangeEventId { get; set; }
        //[DataMember]
        //public virtual ChangeEvent ChangeEvent { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public string UserIdCode { get; set; }
        
        [DataMember]
        public ChangeEventType Type { get; set; }

        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string ChangedFrom { get; set; }

        [DataMember]
        public string ChangedTo { get; set; }

        [ForeignKey("Entity")]
        [DataMember]
        public int? EntityId { get; set; }
        [DataMember]
        public virtual Entity Entity { get; set; }
    }

    public enum ChangeEventType
    {
        CREATED,
        UPDATED,
        DELETED
    }
}