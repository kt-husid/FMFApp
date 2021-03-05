using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Admin.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication
{
    public class DatabaseSetupConfig
    {
        public static void Execute(bool canRun)
        {
            if (!canRun) return;

            CountryHandler.TryCreate(HttpContext.Current.Server.MapPath("~/Content/files/countries.json"));
            //PersonGenderHandler.TryCreate();
            //PersonTitleHandler.TryCreate();
            new Converter("dd'/'MM'/'yyyy");
        }
    }
}