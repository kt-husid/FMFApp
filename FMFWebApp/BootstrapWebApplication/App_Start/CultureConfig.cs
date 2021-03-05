using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BootstrapWebApplication
{
    public class CultureConfig
    {
        public static void LoadCultures(string[] cultures, string path)
        {
            Kthusid.Helpers.CultureHelper.Instance.ClearCultures();
            JsonSerializer serializer = new JsonSerializer();
            var cultureList = serializer.Deserialize<Dictionary<string, Kthusid.Helpers.CultureInfo>>(new JsonTextReader(new StreamReader(HttpContext.Current.Server.MapPath(path))));
            foreach (var item in cultures)
            {
                Kthusid.Helpers.CultureHelper.Instance.AddCulture(cultureList[item]);
            }
        }
    }
}