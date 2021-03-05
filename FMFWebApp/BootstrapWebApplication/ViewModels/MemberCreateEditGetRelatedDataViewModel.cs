using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Models
{
    public class MemberCreateEditGetRelatedDataViewModel
    {
        public ICollection<Job> Jobs { get; set; }

        public ICollection<MemberType> MemberTypes { get; set; }

        public ICollection<Postal> PostalCodes { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}