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
    public class FishSpeciesHandler
    {
        BootstrapContext db = new BootstrapContext();

        public FishSpeciesHandler()
        {
            
        }

        public IQueryable<FishSpecies> GetFiltered(IQueryable<FishSpecies> list, string filter, int minItems)
        {
            var filtered = list as IQueryable<FishSpecies>;
            if (filter.Length >= minItems)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                filtered = filtered.Where(s => 
                    s.Code.ToLower().Equals(filter.ToLower()) 
                    || (""+s.OldCode).ToLower().Equals(filter.ToLower())
                    || s.Name.ToLower().Contains(filter.ToLower())
                    || (""+s.OldName).ToLower().Contains(filter.ToLower())
                    );
            }
            if (filtered != null)
            {
                filtered = filtered.OrderBy(x => x.Code);
            }
            return filtered;
        }
    }
}