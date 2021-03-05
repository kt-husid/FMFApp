using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{

    public class LogViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string UserIdCode { get; set; }

        public ChangeEventType Type { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ChangedFrom { get; set; }

        public string ChangedTo { get; set; }

        public int ChangeEventId { get; set; }
    }
}