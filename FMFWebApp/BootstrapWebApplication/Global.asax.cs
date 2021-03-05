using AutoMapper;
using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Telerik.Reporting.Services.WebApi;

namespace BootstrapWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AddGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Handle i18n decimal separators
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            CultureConfig.LoadCultures(new string[] { "en", "fo", "da" }, "~/Content/files/cultures.json");
            //DatabaseSetupConfig.Execute(false);
            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
            Core.Settings.Instance.AppName = "Skipast";
            Core.Settings.Instance.DateTimeFormatReporting = "ddMMyyyy";


            //var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
            //var xmlFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //xmlFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            //var dcs = new DataContractSerializer(typeof(Website), null, int.MaxValue, false, /* preserveObjectReferences: */ true, null);
            //xmlFormatter.CreateDefaultSerializerSettings(<Website>(dcs);

            
        }

        public static void AddGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new BootstrapCore.Attributes.LoggingFilterAttribute());
        }
    }
}
