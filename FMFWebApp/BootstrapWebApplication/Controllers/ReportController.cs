using BootstrapWebApplication.Models;
using Kthusid.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BootstrapWebApplication.Controllers
{
    public class ReportController : Controller<object>
    {
        //// GET: Report
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    var cultureName = CookieHelper.GetCultureName(Request);
        //    var cultureInfo = new System.Globalization.CultureInfo(cultureName);
        //    // Modify current thread's cultures            
        //    Thread.CurrentThread.CurrentCulture = cultureInfo;
        //    Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //    return base.BeginExecuteCore(callback, state);
        //}

        [HttpPost]
        public ActionResult ClearReportsCache()
        {
            try
            {
                var dirInfo = new DirectoryInfo(Server.MapPath("~/App_Data/ReportsCache"));
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        protected override IQueryable<object> PagedListFilter(IQueryable<object> list, string filterName = null)
        {
            throw new NotImplementedException();
        }

        protected override void AddViewBag(object obj)
        {
            
        }

        protected override Kendo.Mvc.UI.DataSourceResult Get(int id, Kendo.Mvc.UI.DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        public ActionResult PrintIncomeStats()
        {
            return View();
        }

        public ActionResult IncomeStatsReport(bool isPartialView = false)
        {
            var model = new IncomeStatsModel()
            {
                UserIdCode = GetUserIdCode()
            };
            if (isPartialView)
                return PartialView(model);
            return View(model);
        }

        public ActionResult PrintFishStats()
        {
            return View();
        }

        public ActionResult FishStatsReport(bool isPartialView = false)
        {
            var model = new FishStatsModel()
            {
                UserIdCode = GetUserIdCode()
            };
            if (isPartialView)
                return PartialView(model);
            return View(model);
        }

        public ActionResult FishStatsPercentageReport(bool isPartialView = false)
        {
            var model = new FishStatsModel()
            {
                UserIdCode = GetUserIdCode()
            };
            if (isPartialView)
                return PartialView(model);
            return View(model);
        }

        public ActionResult PrintSaleStats()
        {
            return View();
        }

        public ActionResult SaleStatsReport(bool isPartialView = false)
        {
            var model = new SaleStatsModel()
            {
                UserIdCode = GetUserIdCode()
            };
            if (isPartialView)
                return PartialView(model);
            return View(model);
        }

        public ActionResult PrintInsuranceReport()
        {
            return View();
        }

        public ActionResult InsuranceReport(bool isPartialView = false)
        {
            var model = new InsuranceReportViewModel()
            {
                UserIdCode = GetUserIdCode()
            };
            if (isPartialView)
                return PartialView(model);
            return View(model);
        }
    }
}