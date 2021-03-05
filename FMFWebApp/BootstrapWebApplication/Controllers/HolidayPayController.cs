using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class HolidayPayController : Controller<HolidayPay_HOVD>
    {
        protected override IQueryable<HolidayPay_HOVD> PagedListFilter(IQueryable<HolidayPay_HOVD> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                               );
        }

        protected override void AddViewBag(HolidayPay_HOVD obj)
        {

        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)//, string from, string to)
        {
            var fromDate = DateTime.Now.AddYears(-15);
            var toDate = DateTime.Now;
            if (request.Filters.Count == 1)
            {
                var filter = request.Filters[0] as Kendo.Mvc.CompositeFilterDescriptor;
                if (filter.FilterDescriptors.Count == 2)
                {
                    var filter1 = filter.FilterDescriptors[0] as Kendo.Mvc.FilterDescriptor;
                    var filter2 = filter.FilterDescriptors[1] as Kendo.Mvc.FilterDescriptor;
                    if (filter1.Member == "from")
                    {
                        DateTime.TryParseExact(filter1.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDate);
                    }
                    if (filter2.Member == "to")
                    {
                        DateTime.TryParseExact(filter2.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out toDate);
                    }
                }
            }
            request.Filters.Clear();
            var member = db.Members.Where(x => x.Id == id).FirstOrDefault();
            if (member == null)
            {
                return new EmptyResult();
            }
            return Json(GetFiltered(member, fromDate, toDate, request));
        }

        private DataSourceResult GetFiltered(Member member, DateTime? from, DateTime? to, DataSourceRequest request)
        {
            var filtered = member.HolidayPay_HOVD.Where(s => DateTime.Compare(from.Value, s.TransferDate.Value) <= 0 && DateTime.Compare(to.Value, s.TransferDate.Value) >= 0);
            return GetFiltered(request, filtered);
        }

        private DataSourceResult GetFiltered(DataSourceRequest request, IEnumerable<HolidayPay_HOVD> list)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = list.FilterDeleted().Select(m => new MemberHolidayPay_HOVD_ViewModel()
            {
                Id = m.Id,
                TransferDate = m.TransferDate,
                Amount = m.Amount,
                EmployerNumber = m.EmployerNumber,
                REG_NR_FF = m.REG_NR_FF,
                KONTO_FF = m.KONTO_FF,
                REG_NR = m.REG_NR,
                KONTO = m.KONTO,
                ART = m.ART,
                KOYR_DATO = m.KOYR_DATO,
                TR_NR = m.TR_NR
            });//;ToViewModel(m));
            return result.ToDataSourceResult(request);
        }

        private MemberHolidayPay_HOVD_ViewModel ToViewModel(HolidayPay_HOVD m)
        {
            return new MemberHolidayPay_HOVD_ViewModel()
            {
                Id = m.Id,
                TransferDate = m.TransferDate,
                Amount = m.Amount,
                EmployerNumber = m.EmployerNumber,
                REG_NR_FF = m.REG_NR_FF,
                KONTO_FF = m.KONTO_FF,
                REG_NR = m.REG_NR,
                KONTO = m.KONTO,
                ART = m.ART,
                KOYR_DATO = m.KOYR_DATO,
                TR_NR = m.TR_NR
            };
        }

        protected override void CreateGetHook()
        {

        }

        protected override void EditGetHook()
        {

        }

        protected override void DeleteGetHook()
        {

        }

        public ActionResult MemberHolidayPayReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        [HttpGet]
        public ActionResult HolidayPays(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "HolidayPays");
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = db.Set<Member>().Find(id);
            if (member != null)
            {
                var result = member.HolidayPay_HOVD.Select(m => new MemberHolidayPay_HOVD_ViewModel()
                {
                    Id = m.Id,
                    TransferDate = m.TransferDate,
                    Amount = m.Amount,
                    EmployerNumber = m.EmployerNumber,
                    REG_NR_FF = m.REG_NR_FF,
                    KONTO_FF = m.KONTO_FF,
                    REG_NR = m.REG_NR,
                    KONTO = m.KONTO,
                    ART = m.ART,
                    KOYR_DATO = m.KOYR_DATO,
                    TR_NR = m.TR_NR
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }
    }
}