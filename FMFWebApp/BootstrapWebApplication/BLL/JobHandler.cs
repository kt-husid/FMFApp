using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Kthusid;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class JobHandler
    {
        BootstrapContext db = new BootstrapContext();

        public JobHandler()
        {
            
        }

        public IQueryable<Job> GetFiltered(IQueryable<Job> list, string filter, int minItems)
        {
            var filtered = list as IQueryable<Job>;
            if (filter.Length >= minItems)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                filtered = filtered.Where(s => 
                    s.Code.ToLower().Equals(filter.ToLower()) 
                    || ("" + s.Description).ToLower().Contains(filter.ToLower())
                    );
            }
            return filtered;
        }
    }
}