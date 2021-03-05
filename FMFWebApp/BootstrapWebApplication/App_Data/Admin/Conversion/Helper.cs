using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion
{
    public class Helper
    {
        public static DateTime? ParseDate(DateTime? dateTime, string hour, string min, string sec = "0")
        {
            if (dateTime.HasValue)
            {
                int h = 0;
                int m = 0;
                int s = 0;
                int.TryParse(hour, out h);
                int.TryParse(min, out m);
                int.TryParse(sec, out s);
                return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, h, m, s);
            }
            return null;
        }
    }
}