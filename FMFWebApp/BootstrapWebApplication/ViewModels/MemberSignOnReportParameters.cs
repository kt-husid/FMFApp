using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberSignOnReportParameters
    {
        public int MemberId { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}