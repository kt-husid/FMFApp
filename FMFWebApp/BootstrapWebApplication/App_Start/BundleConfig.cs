using System.Web;
using System.Web.Optimization;

namespace BootstrapWebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // http://jqueryvalidation.org/
            bundles.Add(new ScriptBundle("~/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/js/signalr").Include(
                        "~/Scripts/jquery.signalR-*"));

            //bundles.Add(new ScriptBundle("~/js/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/js/jquery.serialize").Include(
                        "~/Scripts/jquery.serialize-object.js",
                        "~/Scripts/jquery.todictionary.js"));

            // https://github.com/jquery/globalize  
            //bundles.Add(new ScriptBundle("~/js/globalize").Include(
            //        "~/Scripts/globalize/globalize.js"));

            // https://github.com/scottjehl/Respond
            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-dashboard.js"));

            // https://github.com/eternicode/bootstrap-datepicker/
            //bundles.Add(new ScriptBundle("~/js/bootstrap-datepicker").Include(
            //          "~/Scripts/bootstrap-datepicker.js",
            //          "~/Scripts/bootstrap-datepicker-globalize.js",
            //          "~/Scripts/locales/bootstrap-datepicker.da.js",
            //          "~/Scripts/locales/bootstrap-datepicker.fo.js"));

            // http://underscorejs.org/
            bundles.Add(new ScriptBundle("~/js/underscore").Include(
                      "~/Scripts/underscore.js",
                      "~/Scripts/underscore.string.js"));

            // https://github.com/douglascrockford/JSON-js
            bundles.Add(new ScriptBundle("~/js/json2").Include(
                    "~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle("~/js/kthusid").Include(
                      "~/Scripts/kthusid.js"));

            bundles.Add(new ScriptBundle("~/js/kthusid-knockout-extensions").Include(
                      "~/Scripts/kthusid.knockout.extensions.js"));

            // http://www.openjs.com/scripts/events/keyboard_shortcuts/
            // https://github.com/tzuryby/jquery.hotkeys
            bundles.Add(new ScriptBundle("~/js/shortcuts").Include(
                      "~/Scripts/shortcut.js"
                      // bug: jquery.hotkeys.js somehow breaks kendo ui autocomplete, but only searches (works) on copy paste.
                      //"~/Scripts/jquery.hotkeys.js"
                      ));

            //bundles.Add(new ScriptBundle("~/js/editable").Include(
            //            "~/Content/bootstrap3-editable/js/bootstrap-editable.js"));

            bundles.Add(new ScriptBundle("~/js/knockout").Include(
                        "~/Scripts/knockout-{version}.js",//3.2.0
                        "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle("~/js/sammy").Include(
                        "~/Scripts/sammy-{version}.js"));//0.7.5

            //bundles.Add(new ScriptBundle("~/js/fuelux").Include(
            //            "~/Content/fuelux/js/fuelux.js"));

            // http://momentjs.com/
            bundles.Add(new ScriptBundle("~/js/moment").Include(
                        "~/Scripts/moment-with-locales.js"));

            bundles.Add(new ScriptBundle("~/js/linq").Include(
                        "~/Scripts/rx.lite.js"
                        ));

            bundles.Add(new ScriptBundle("~/js/maskedinput").Include(
                        "~/Scripts/jquery.maskedinput.js"
                        ));

            //bundles.Add(new ScriptBundle("~/js/inputmask").Include(
            //            "~/Scripts/jquery.inputmask/jquery.inputmask.js",
            //            "~/Scripts/jquery.inputmask/jquery.inputmask.extensions.js",
            //            "~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
            //            "~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions.js"));

            //bundles.Add(new ScriptBundle("~/js/datatable").Include(
            //            "~/Scripts/DataTables-1.10.3/jquery.dataTables.js"));

            //bundles.Add(new StyleBundle("~/css/datatable").Include(
            //          "~/Content/DataTables-1.10.3/css/jquery.dataTables.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/css/kthusid").Include(
                      "~/Content/kthusid.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/bootstrap-extensions.css",
                      //"~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-dashboard.css"));

            //bundles.Add(new StyleBundle("~/css/bootstrap-datepicker").Include(
            //          "~/Content/bootstrap-datepicker.css"));

            //bundles.Add(new StyleBundle("~/css/jqueryui").Include(
            //            "~/Content/themes/base/jquery.ui.css",
            //            "~/Content/themes/base/jquery.ui.core.css",
            //    //"~/Content/themes/base/jquery.ui.resizable.css",
            //    //"~/Content/themes/base/jquery.ui.selectable.css",
            //    //"~/Content/themes/base/jquery.ui.accordion.css",
            //    //"~/Content/themes/base/jquery.ui.autocomplete.css",
            //    //"~/Content/themes/base/jquery.ui.button.css",
            //    //"~/Content/themes/base/jquery.ui.dialog.css",
            //    //"~/Content/themes/base/jquery.ui.slider.css",
            //    //"~/Content/themes/base/jquery.ui.tabs.css",
            //    //"~/Content/themes/base/jquery.ui.datepicker.css",
            //    //"~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"
            //            ));

            //bundles.Add(new StyleBundle("~/css/pagedlist").Include(
            //            "~/Content/PagedList.css"));

            //bundles.Add(new StyleBundle("~/css/editable").Include(
            //            "~/Content/bootstrap3-editable/css/bootstrap-editable.css"));

            //bundles.Add(new StyleBundle("~/css/fuelux").Include(
            //            "~/Content/fuelux/css/fuelux.css"));

            bundles.Add(new ScriptBundle("~/js/kendo").Include(
            "~/Content/kendo/2015.1.408/js/kendo.all.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            //"~/Content/kendo/js/kendo.culture.da.min.js",
            //"~/Content/kendo/js/kendo.culture.en.min.js",
            //"~/Content/kendo/js/kendo.culture.fo.min.js",
            "~/Content/kendo/2015.1.408/aspnet/js/kendo.aspnetmvc.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/kendo/2015.1.408/styles/kendocss").Include(
            "~/Content/kendo/2015.1.408/styles/kendo.common.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.default.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.dataviz.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.dataviz.default.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.dataviz.mobile.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.common-bootstrap.min.css",
            "~/Content/kendo/2015.1.408/styles/kendo.uniform.min.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            // If is not debug mode then enable optimizations
            BundleTable.EnableOptimizations = !Kthusid.Helpers.Debug.IsDebugMode;
        }
    }
}