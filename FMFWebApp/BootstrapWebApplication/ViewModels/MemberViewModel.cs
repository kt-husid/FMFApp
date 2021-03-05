using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberViewModel : ViewModelBaseWithChangeEvent
    {
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public string MemberType { get; set; }

        public int? JobId { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int? PRI_OWN { get; set; }

        public int? BETALER { get; set; }

        public string M_STATUS { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool IsAlive { get; set; }

        public int Age { get; set; }

        public string GenderCode { get; set; }

        public string SSN { get; set; }
    }

    public class CreateMemberViewModel : ViewModelBaseWithChangeEvent
    {
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public string MemberType { get; set; }

        public string JobTitle { get; set; }

        public int? PRI_OWN { get; set; }

        public int? BETALER { get; set; }

        public string M_STATUS { get; set; }
    }


    public class MemberPrintLabelViewModel : ViewModelBase
    {
        public DateTime? ETI_DATO { get; set; }

        public string ETI_ID { get; set; }
    }
}