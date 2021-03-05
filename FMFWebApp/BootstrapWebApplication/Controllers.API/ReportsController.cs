//using System;
//using System.Web;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;
//using Telerik.Reporting.Cache.Interfaces;
//using Telerik.Reporting.Services.Engine;
//using Telerik.Reporting.Services.WebApi;

//namespace BootstrapWebApplicationf.Controllers
//{
//    public class ReportController : ReportsControllerBase
//    {
//        protected override IReportResolver CreateReportResolver()
//        {
//            var reportsPath = HttpContext.Current.Server.MapPath("~/Reports");

//            return new ReportFileResolver(reportsPath).AddFallbackResolver(new ReportTypeResolver());
//        }

//        protected override ICache CreateCache()
//        {
//            return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache();
//        }
//    }
//}
namespace BootstrapWebApplication.Controllers.API
//namespace BootstrapWebApplication.Views.Report.Controllers
{
    using Kthusid.Web;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using Telerik.Reporting.Cache.Interfaces;
    using Telerik.Reporting.Services.Engine;
    using Telerik.Reporting.Services.WebApi;

    // The class name determines the service URL. 
    // ReportsController class name defines /api/report/ service URL.
    public class ReportsController : ReportsControllerBase
    {
        protected override IReportResolver CreateReportResolver()
        {
            //var dir = HttpContext.Current.Server.MapPath(@"~/App_Data/ReportsCache");
            //foreach (var d in Directory.GetDirectories(dir))
            //{
            //    Directory.Delete(d, true);
            //}
            //This is the folder that contains the XML (trdx) report definitions.
            var reportsPath = HttpContext.Current.Server.MapPath(@"~/Reports");

            //Resolver for trdx report definitions.
            var reportFileResolver = new ReportFileResolver(reportsPath);

            // Resolver for class report definitions.
            var reportTypeResolver = new ReportTypeResolver();


            // Add the ReportTypeResolver as fallback resolver and 
            // return the ReportFileResolver instance.
            return reportFileResolver.AddFallbackResolver(reportTypeResolver);
        }

        protected override ICache CreateCache()
        {
            // Create instance of the default file cache.
            return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache(HttpContext.Current.Server.MapPath(@"~/App_Data/ReportsCache"));
            //return Telerik.Reporting.Services.Engine.CacheFactory.CreateDatabaseCache("mssql", "Data Source=WIN-4N34M48TAKS;Initial Catalog=TelerikSession;Persist Security Info=True;User ID=benjamin;Password=kurlA6906");
        }

        //protected override Telerik.Reporting.Cache.Interfaces.IStorage CreateStorage()
        //{
        //    return new Telerik.Reporting.Cache.MsSqlServerStorage("Data Source=WIN-4N34M48TAKS;Initial Catalog=TelerikSession;Persist Security Info=True;User ID=benjamin;Password=kurlA6906");
        //}

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            var cultureName = CookieHelper.GetCultureName(HttpContext.Current.Request);
            var cultureInfo = new System.Globalization.CultureInfo(cultureName);
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            base.Initialize(controllerContext);
        }
    }
}