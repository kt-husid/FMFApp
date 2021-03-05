using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.BLL;
using System.Threading.Tasks;
using BootstrapWebApplication.Interfaces;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Globalization;
using AutoMapper;
using BootstrapWebApplication.Hubs;
using Microsoft.AspNet.SignalR;

namespace BootstrapWebApplication.Controllers
{
    public class MemberController : Controller<Member>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            return Json(GetMembersFiltered(filter, request).Data, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            using (var context = new BootstrapContext())
            {
                var sqlUpdateMemberAge = @"UPDATE dbo.Member SET Age = (SELECT DATEDIFF(yy, p.Birthday, getdate()) - 1 FROM dbo.Person AS p LEFT OUTER JOIN dbo.Member AS m ON p.Id = m.PersonId LEFT OUTER JOIN dbo.ChangeEvent AS ce ON m.ChangeEventId = ce.Id WHERE (ce.DeletedOn IS NULL) AND m.Id = dbo.Member.Id)";
                context.Database.ExecuteSqlCommand(sqlUpdateMemberAge);
            }

            var filter = "";
            if (request.Filters.Count > 0)
            {
                filter = (request.Filters[0] as Kendo.Mvc.FilterDescriptor).Value as string;
            }
            request.Filters.Clear();
            var result = Json(GetMembersFiltered(filter, request));
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        private DataSourceResult GetMembersFiltered(string filter, DataSourceRequest request)
        {
            var membersFiltered = new MemberHandler().GetMembersFiltered(db.Members, filter, 3);
            return GetMembers(request, membersFiltered);
        }

        private DataSourceResult GetMembers(DataSourceRequest request, IQueryable<Member> members)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                FullName = (m.Person != null) ? (!string.IsNullOrEmpty(m.Person.FullName) ? m.Person.FullName : (String.IsNullOrEmpty(m.Person.MiddleName) ? (m.Person.FirstName + " " + m.Person.LastName) : (m.Person.FirstName + " " + m.Person.MiddleName + " " + m.Person.LastName))) : "",
                Birthday = (m.Person != null) ? m.Person.Birthday : null,
                SSN = (m.Person != null) ? m.Person.SSN : "-",
                MemberType = (m.MemberType != null) ? m.MemberType.Description : "",
                JobId = m.JobId,
                JobTitle = m.JobTitle,// !string.IsNullOrEmpty(m.JobTitle) ? m.JobTitle : (m.Job != null ? m.Job.Code : "-"),
                JobDescription = m.JobDescription,
                PRI_OWN = m.PRI_OWN,
                BETALER = m.BETALER,
                M_STATUS = m.M_STATUS,
                PhoneNumber = m.PhoneNumber,
                Address = ("" + m.AddressLine).Trim() + ", " + m.CountryCode + "-" + m.PostalCode.Value + " " + m.CityName,
                IsAlive = m.Person != null ? m.Person.IsAlive : false,
                Age = m.Age.HasValue ? m.Age.Value : -1,
                GenderCode = m.GenderCode
            });
            return result.ToDataSourceResult(request);
        }

        public ActionResult ToolbarTemplate_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJobs(request), JsonRequestBehavior.AllowGet);
        }

        public DataSourceResult GetJobs(DataSourceRequest request)
        {
            var result = db.Jobs.FilterDeleted().OrderBy(j => j.Code).Select(j => new
            {
                Id = j.Id,
                Name = j.Description
            });
            return result.ToDataSourceResult(request);
        }

        private DataSourceResult GetMemberEmployments(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = Find(id);
            if (member != null)
            {
                var result = new List<MemberEmploymentViewModel>();
                var signOns = member.SignOns.FilterDeleted().OrderByDescending(x => x.From);
                foreach (var signOn in signOns)
                {
                    //db.Entry(signon.Trip).Collection(x => x.TripLines).Load();
                    //db.Entry(signon.Trip).Collection(x => x.TripDeductions).Load();
                    result.Add(new MemberEmploymentViewModel()
                    {
                        Id = signOn.Id,
                        JobCode = signOn.JobCode,
                        ShipId = signOn.Ship != null ? signOn.Ship.Id : -1,
                        ShipCode = signOn.ShipCode,
                        ShipName = signOn.ShipName,
                        From = signOn.From,
                        To = signOn.To,
                        CrewSharePerCrewMember = (signOn.Trip != null && signOn.Trip.MANN_PART.HasValue) ? signOn.Trip.MANN_PART.Value : 0m, // m.KR_I,
                        //CrewSharePerCrewMember = (signOn.KR_IALT.HasValue) ? signOn.KR_IALT.Value : 0m, //(signon.Trip != null && signon.Trip.CrewSharePerCrewMember.HasValue) ? Math.Round(signon.Trip.CrewSharePerCrewMember.Value, 2) : (signon.Trip.MANN_PART.HasValue ? signon.Trip.MANN_PART.Value : 0m),
                        FinancialAid = signOn.Trip != null ? signOn.Trip.MinimumWage : null
                    });
                }
                //var result = member.SignOns.FilterDeleted().OrderByDescending(x => x.From).Select(m => new MemberEmploymentViewModel()
                //{
                //    Id = m.Id,
                //    JobCode = m.JobCode,
                //    ShipId = m.Ship != null ? m.Ship.Id : -1,
                //    ShipCode = m.ShipCode,
                //    ShipName = m.ShipName,
                //    From = m.From,
                //    To = m.To,
                //    CrewSharePerCrewMember = (m.Trip != null && m.Trip.CrewSharePerCrewMember.HasValue) ? Math.Round(m.Trip.CrewSharePerCrewMember.Value, 2) : (m.Trip.MANN_PART.HasValue ? m.Trip.MANN_PART.Value : 0m),
                //    FinancialAid = m.Trip != null ? m.Trip.MinimumWage : null,
                //});
                return result.AsEnumerable().ToDataSourceResult(request);
            }
            return null;
        }

        public ActionResult MemberEmployments_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            return Json(GetMemberEmployments(id, request));
        }

        public ActionResult MemberSignOn_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            return Json(GetMemberSignOns(id, request));
        }

        private DataSourceResult GetMemberSignOns(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var member = Find(id);
            if (member != null)
            {
                var fromDate = DateTime.Now.AddYears(-1);
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

                var memberSignOns = member.SignOns.FilterDeleted();
                memberSignOns = memberSignOns.Where(s => DateTime.Compare(fromDate, s.From.Value) <= 0 && DateTime.Compare(toDate, s.To.Value) >= 0);

                var result = memberSignOns.OrderByDescending(x => x.From).Select(m => new MemberSignOnViewModel()
                {
                    Id = m.Id,
                    TripId = m.TripId,
                    TripNumber = m.Trip != null ? "" + m.Trip.TripId : null,
                    //Trip = m.Trip,
                    From = m.From,
                    To = m.To,
                    Days = m.From.HasValue && m.To.HasValue ? m.To.Value.Subtract(m.From.Value).Days + 1 : 0,
                    ShipId = m.Ship != null ? m.Ship.Id : -1,
                    ShipCode = m.ShipCode,
                    ShipName = m.ShipName,
                    JobCode = m.JobCode,
                    PART = m.PART,
                    TIL_PR_DG = m.TIL_PR_DG,
                    EmployerNumber = m.EmployerNumber,
                    KR_IALT = m.KR_IALT,
                    KR_I = (m.Trip != null && m.Trip.MANN_PART.HasValue) ? m.Trip.MANN_PART.Value : 0m, // m.KR_I,
                    KR_FF = m.KR_FF,
                    MembershipPayment = (m.HasInsurance && m.KR_IALT.HasValue) ? (m.KR_IALT.Value * 0.0125m) : 0m 
//                    MembershipPayment = (m.HasInsurance && m.Trip.MANN_PART.HasValue) ? (m.Trip.MANN_PART.Value * 0.0125m) : 0m // Hans: rokna út frá KR_I - umbestemma meg, skal rokanst fra kr_ialt
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        public ActionResult MemberHolidayPaysForPeriod_Read([DataSourceRequest] DataSourceRequest request, int id, int from, int to)
        {
            if (request.Filters.Count == 1)
            {
                var filter = request.Filters[0] as Kendo.Mvc.CompositeFilterDescriptor;
                if (filter.FilterDescriptors.Count == 2)
                {
                    var filter1 = filter.FilterDescriptors[0] as Kendo.Mvc.FilterDescriptor;
                    var filter2 = filter.FilterDescriptors[1] as Kendo.Mvc.FilterDescriptor;
                    if (filter1.Member == "from")
                    {
                        int.TryParse(filter1.Value as string, out from);
                    }
                    if (filter2.Member == "to")
                    {
                        int.TryParse(filter2.Value as string, out to);
                    }
                }
            }
            request.Filters.Clear();

            var member = Find(id);
            if (member == null)
            {
                return new EmptyResult();
            }
            var mh = new MemberHandler(member);
            var result = new MemberHolidayPayOverviewViewModel(member);
            for (int year = from; year <= to; year++)
            {
                result.MemberHolidayPayYearTotals.Add(mh.GetHolidayPaySumForYear(year));
            }
            //return Request.GetResponse(result, type);
            return Json(result.MemberHolidayPayYearTotals.ToDataSourceResult(request));//GetMemberPayments(id, request));
        }

        public ActionResult MemberTripInfoForPeriod_Read([DataSourceRequest] DataSourceRequest request, int id, int from, int to)
        {
            if (request.Filters.Count == 1)
            {
                var filter = request.Filters[0] as Kendo.Mvc.CompositeFilterDescriptor;
                if (filter.FilterDescriptors.Count == 2)
                {
                    var filter1 = filter.FilterDescriptors[0] as Kendo.Mvc.FilterDescriptor;
                    var filter2 = filter.FilterDescriptors[1] as Kendo.Mvc.FilterDescriptor;
                    if (filter1.Member == "from")
                    {
                        int.TryParse(filter1.Value as string, out from);
                    }
                    if (filter2.Member == "to")
                    {
                        int.TryParse(filter2.Value as string, out to);
                    }
                }
            }
            request.Filters.Clear();

            var member = Find(id);
            if (member == null)
            {
                return new EmptyResult();
            }
            var mh = new MemberHandler(member);
            var result = new MemberTripInfoForYearOverviewViewModel(member);
            for (int year = from; year <= to; year++)
            {
                result.MemberTripInfoForYearTotals.Add(mh.GetTripInfoForYear(year));
            }
            return Json(result.MemberTripInfoForYearTotals.ToDataSourceResult(request));
        }

        protected override IQueryable<Member> PagedListFilter(IQueryable<Member> list, string filterName = null)
        {
            var filteredList = list.Where(s => s.Person.FirstName.ToLower().Contains(filterName.ToLower())
                                   || s.Person.MiddleName.ToLower().Contains(filterName.ToLower())
                                   || s.Person.LastName.ToLower().Contains(filterName.ToLower())
                                   || s.Person.FullName.ToLower().Contains(filterName.ToLower()));

            return filteredList;
        }

        protected override void AddViewBag(Member Member)
        {
            if (Member != null)
            {
                ViewBag.ChangeEventId = Member.ChangeEventId;

            }
            else
            {
                ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "Id");

                //ViewBag.MemberBankAccountId = null;
            }

            ViewBag.PersonId = new SelectList(db.Persons.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.FullName,
                Value = m.Id.ToString()
            }), "Value", "Text", Member != null ? Member.PersonId : null);
            ViewBag.BankId = new SelectList(db.Banks.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text");


            ViewBag.JobId = new SelectList(db.Jobs, "Id", "TEKSTUR");
            ViewBag.LastSignOnId = new SelectList(db.SignOns, "Id", "SKIB_ID");
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "Id", "TEKSTUR");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN");
            ViewBag.PostNrId = new SelectList(db.Postals, "Id", "POSTNR1");
            ViewBag.StatId = new SelectList(db.Statuses, "Id", "TEKSTUR");
            //ViewBag.TitleId = new SelectList(db.MemberTitles.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", Member != null ? Member.TitleId : null);
        }

        public ActionResult Employments(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "Employments");
        }

        public ActionResult SignOns(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "SignOns");
        }

        public ActionResult Insurances(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "Insurances");
        }

        public virtual ActionResult Read_Insurance([DataSourceRequest] DataSourceRequest request, int? id = null)
        {
            return Json(GetInsurance(id.HasValue ? id.Value : -1, request));
        }

        //private DataSourceResult GetInsurance(int id, DataSourceRequest request)
        //{
        //    var parent = db.Set<Member>().Find(id);
        //    if (parent != null)
        //    {
        //        var result = parent.SignOns.FilterDeleted().OrderByDescending(x => x.TripNumber.Value).Select(m => new InsuranceViewModel()
        //        {
        //            Id = m.Id,
        //            TripNumber = m.Trip != null ? m.Trip.TripId : -1,
        //            From = m.From,
        //            To = m.To,
        //            ShipCode = m.ShipCode,
        //            JobCode = m.JobCode,
        //            PART = (m.Trip != null && m.PART.HasValue) ? m.PART.Value : 0m,
        //            PerDay = m.Trip != null ? (decimal)(m.Trip.TRYG_ANT / 1m) : 0m,
        //            EmployerNumber = m.EmployerNumber.HasValue ? m.EmployerNumber.Value : 0,
        //            Days = m.Trip != null ? m.Trip.Days : 0

        //        });
        //        return result.ToDataSourceResult(request);
        //    }
        //    return null;
        //}

        private DataSourceResult GetInsurance(int id, DataSourceRequest request)
        {
            var parent = db.Set<Member>().Find(id);
            if (parent != null)
            {
                var user = GetUser();
                var laborInsurancePercentage = (user != null && user.AppSettings != null && user.AppSettings.LaborInsurancePercentage.HasValue) ? (user.AppSettings.LaborInsurancePercentage.Value / 100m) : 0.01m;

                var result = parent.SignOns.FilterDeleted().OrderByDescending(x => x.TripNumber).ThenByDescending(x => x.From).Select(m => new InsuranceViewModel()
                //var signOns = parent.SignOns.FilterDeleted();
                //signOns = signOns.OrderByDescending(x => x.TripId);
                //var result = signOns.Select(m => new InsuranceViewModel()
                {
                    TripId = m.Trip != null ? m.Trip.Id : -1,
                    TripNumberStr = m.Trip != null ? ("" + m.Trip.TripId).PadLeft(4).Replace(" ", "0") : "-1",
                    TripNumber = m.Trip != null ? m.Trip.TripId : -1,
                    //TripNumber = m.TripNumber.HasValue ? m.TripNumber.Value : 0,
                    From = m.From,
                    To = m.To,
                    ShipCode = m.ShipCode,
                    JobCode = m.JobCode,
                    PART = (m.Trip != null && m.PART.HasValue) ? m.PART.Value : 1m,
                    PerDay = m.LaborInsuranceAmountPerDay.HasValue ? m.LaborInsuranceAmountPerDay.Value : 0m,// til (m.Trip != null && m.Trip.TRYG_ANT.HasValue) ? (decimal)(m.Trip.TRYG_ANT.Value / 1m) : 0m,
                    //PART = m.PART.HasValue ? m.PART.Value : 1m,
                    //PerDay = m.TIL_PR_DG.HasValue ? m.TIL_PR_DG.Value : 0m,
                    EmployerNumber = m.EmployerNumber.HasValue ? m.EmployerNumber.Value : 0,
                    Days = m.Trip != null ? m.Trip.Days : 0,
                    //Days = (m.To.HasValue && m.From.HasValue) ? (m.To.Value.Subtract(m.From.Value).Days + 1) : 0,
                    MembershipPayment = m.KR_IALT.HasValue ? m.KR_IALT.Value * 0.0125m : 0m//(m.Trip != null && m.Trip.CrewShareMoneyCalculated.HasValue) ? (m.Trip.CrewShareMoneyCalculated.Value * (m.PART.HasValue ? m.PART.Value : 1m) * laborInsurancePercentage) : 0m
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        public virtual ActionResult Read_InsuranceYears([DataSourceRequest] DataSourceRequest request, int? id = null)
        {
            return Json(GetInsuranceYears(id.HasValue ? id.Value : -1, request));
        }

        private DataSourceResult GetInsuranceYears(int id, DataSourceRequest request)
        {
            var parent = db.Set<Member>().Find(id);
            if (parent != null)
            {
                var signOns = parent.SignOns.FilterDeleted().OrderByDescending(x => x.TripNumber.Value).ThenByDescending(x => x.From).GroupBy(x => x.Year);
                var result = new List<InsuranceYearsViewModel>();
                DateTime? lastYear = null;
                foreach (var signOn in signOns)
                {
                    result.Add(new InsuranceYearsViewModel()
                    {
                        Year = signOn.FirstOrDefault() != null ? signOn.FirstOrDefault().Year : null,
                        Total = signOn.Sum(x => x.KR_IALT).Value
                    });
                    //if (lastYear == null || signOn.From > lastYear)
                    //{
                    //    result.
                    //}
                }
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        public ActionResult MemberPayments(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "MemberPayments");
        }

        public ActionResult MemberFinancialAids(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "MemberFinancialAids");
        }

        public ActionResult Comments(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "Comments");
        }

        [HttpGet]
        public ActionResult HolidayPaysForPeriod(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "HolidayPaysForPeriod");
        }

        [HttpGet]
        public ActionResult TripInfoForPeriod(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "TripInfoForPeriod");
        }

        [HttpGet]
        public ActionResult PersonInfo(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "PersonInfo");
        }

        [HttpGet]
        public ActionResult LabelInfo(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "LabelInfo");
        }

        [HttpGet]
        public ActionResult Statistics(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "Statistics");
        }

        [HttpGet]
        public ActionResult LastMemberPayment(int? id)
        {
            return GetRelatedDataForParent<Member>(id, "LastMemberPayment");
        }

        private void AddViewBagForCreateOrEditMember(Member member)
        {
            ViewBag.GenderId = new SelectList(db.PersonGenders.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", (member != null && member.Person != null) ? member.Person.GenderId : null);
            //ViewBag.TitleId = new SelectList(db.PersonTitles.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", (member != null && member.Person != null) ? member.Person.TitleId : null);
            ViewBag.JobId = new SelectList(db.Jobs.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " " + m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", member != null ? member.JobId : null);
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " " + m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", member != null ? member.MemberTypeId : null);
            ViewBag.PostalId = new SelectList(db.Postals.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " " + m.CityName,
                Value = m.Id.ToString()
            }), "Value", "Text", (member != null && member.PrimaryAddress != null) ? member.PrimaryAddress.PostalId : null);
            ViewBag.CountryId = new SelectList(db.Countries.FilterDeleted().ToList().Select(m => new SelectListItem
            {
                Text = m.Code + " " + m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", (member != null && member.PrimaryAddress != null) ? new Nullable<int>(member.PrimaryAddress.CountryId) : null);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBagForCreateOrEditMember(null);
            return Create<Member>("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCreateViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                return new MemberHandler().CreateFromViewModel(o, db, new ChangeEvent[] { CreateChangeEvent(), CreateChangeEvent(), CreateChangeEvent() });// CreateMember(o);
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var member = Find(id);
            AddViewBagForCreateOrEditMember(member);
            ViewModelBaseWithChangeEvent viewModel = null;
            if (member != null)
            {
                viewModel = new MemberEditViewModel()
                {
                    FirstName = member.Person != null ? member.Person.FirstName : "",
                    MiddleName = member.Person != null ? member.Person.MiddleName : "",
                    LastName = member.Person != null ? member.Person.LastName : "",
                    Birthday = member.Person != null ? member.Person.Birthday : null,
                    SSN = member.Person != null ? member.Person.SSN : null,
                    Gender = member.Person != null ? member.Person.Gender : null,
                    Job = member.Person != null ? member.Job : null,
                    MemberType = member.Person != null ? member.MemberType : null,
                    JobCode = (member.Person != null && member.Job != null) ? member.Job.Code : null,
                    MemberTypeCode = (member.Person != null && member.MemberType != null) ? member.MemberType.Code : null,
                    IsAlive = member.Person != null ? member.Person.IsAlive : false
                };
            }
            return Edit("Edit", viewModel, member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberEditViewModel o)
        {
            return UpdateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var member = Find(o.Id);
                //member = MemberHandler.Update(member, o, db);

                var entity = member.Person.Entity;
                var gender = db.PersonGenders.Where(x => x.Id == o.GenderId).FirstOrDefault();
                var personTitle = db.PersonTitles.Where(x => x.Id == o.TitleId).FirstOrDefault();
                personTitle = personTitle != null ? personTitle : member.Person.Title;
                var person = member.Person;
                person.ChangeEvent = UpdateChangeEvent(member.Person.ChangeEventId);
                person.FirstName = o.FirstName;
                person.MiddleName = o.MiddleName;
                person.LastName = o.LastName;
                person.Birthday = o.Birthday;
                person.SSN = o.SSN;
                person.GenderId = o.GenderId;
                person.Title = personTitle;
                person.FullName = String.IsNullOrEmpty(("" + person.MiddleName).Trim()) ? (person.FirstName + " " + person.LastName) : (person.FirstName + " " + person.MiddleName + " " + person.LastName);
                person.IsAlive = o.IsAlive;

                var memberType = db.MemberTypes.Where(x => x.Id == o.MemberTypeId).FirstOrDefault();
                memberType = memberType != null ? memberType : member.MemberType;
                var job = db.Jobs.Where(x => x.Id == o.JobId).FirstOrDefault();
                job = job != null ? job : member.Job;

                member.ChangeEvent = UpdateChangeEvent(member.ChangeEventId);
                //member.NR = o.NR;
                member.Person = person;
                member.JobTitle = job.Code;
                member.Job = job;
                member.MemberType = memberType;
                //member.MemberTypeId = o.MemberTypeId;

                member.MemberTypeCode = memberType != null ? memberType.Code : "";
                member.MemberTypeDescription = memberType != null ? memberType.Description : "";
                member.JobCode = job != null ? job.Code : "";
                member.JobDescription = job != null ? job.Description : "";
                member.GenderCode = gender != null ? gender.Code : null;

                return member;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Member>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MemberViewModel dbObj)
        {
            return DeleteObject<Member, Member>(dbObj.Id);
        }

        public ActionResult HolidayPayOverviews()
        {
            return PartialView();
        }

        public ActionResult MemberSignOnReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        public ActionResult PrintLabel()
        {
            return View();
        }
/*        public ActionResult PrintMemberList()
        {
            return View();
        }
 */
        [HttpGet]
        public ActionResult MemberEnvelope(int? MemberId)
        {
            var member = db.Members.Where(x => x.Id == MemberId).FirstOrDefault();
            if (member != null)
            {
                if (UpdateLabel(member))
                {
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
/*        [HttpGet]

        //--------------------------------------------------------------------------------------------MemberListReport Hans

        public ActionResult MemberListReport()
        {
            TempData["CanOpenReport"] = false;
            return View();
        }
        [HttpPost]
        public ActionResult PrintMemberListReport(MemberListReportParameters obj, bool isPartialView = false, string type = "json")
        {
            db.Configuration.AutoDetectChangesEnabled = false;
            var result = new MemberListReportStatusCode()
            {
                StatusCode = MemberListReportStatusCode.Code.OK,
                Message = BootstrapResources.Resources.Success
            };
            var MemberListReportStatus = new MemberListReportStatus()
            {
                ItemsDone = 0,
                ItemsTotal = 0,
                TimeSpent = 0,
                CanOpenReport = true
            };
            TempData["MemberListReportStatus"] = MemberListReportStatus;
            if (obj != null)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
                hub.Clients.All.broadcastProgress("Leitar...");
                var members = db.Members.Where(x => x.ChangeEvent != null && x.ChangeEvent.DeletedOn == null && x.PostalCode >= obj.PostalFrom && x.PostalCode <= obj.PostalTo && x.MemberType.Code == obj.MemberTypeCode &&

                    string.Compare(x.JobCode, obj.MemberJobCodeFrom) >= 0 && string.Compare(x.JobCode, obj.MemberJobCodeTo) <= 0).AsQueryable();

                MemberListReportStatus.ItemsTotal = 0;

                var memberCount = members.Count();
                if (ModelState.IsValid)
                {
                    var i = 0;
                    var eti_id = GetUserIdCode();
                    var eti_dato = DateTime.Now;
                    var s = DateTime.UtcNow;
                    hub.Clients.All.broadcastProgress("Dagførir...");
                    foreach (var member in members)
                    {
                        member.ETI_ID = eti_id;
                        member.ETI_DATO = eti_dato;
                        // attach the entity
                        db.Set<Member>().Attach(member);
                        // change its state to modified so entity framework can update the existing product instead of creating a new one
                        db.Entry(member).State = EntityState.Modified;
                        i++;
                        var progress = Math.Round(((i * 100.0) / (double)memberCount), 2);
                        hub.Clients.All.broadcastProgress(progress + "% liðugt");
                        //if (i % 10 == 0)
                        {
                            //system.diagnostics.debug.writeline(i);
                            MemberListReportStatus.ItemsDone = i;
                            MemberListReportStatus.TimeSpent = DateTime.UtcNow.Subtract(s).Seconds;
                            TempData["MemberListReportStatus"] = MemberListReportStatus;
                        }
                    }
                    try
                    {
                        // Save changes to the DB
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.StatusCode = MemberListReportStatusCode.Code.FAILED;
                        result.Message = ex.Message;
                    }
                }
                //return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);// Json("PostalFrom=" + obj.PostalFrom + "&PostalTo=" + obj.PostalTo + "&MemberTypeCode=" + obj.MemberTypeCode + "&FromBirthday=" + obj.FromBirthday, JsonRequestBehavior.AllowGet);
            }
            db.Configuration.AutoDetectChangesEnabled = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        static bool FilterForMemberList(Member x, MemberListReportParameters obj)
        {
            return //!string.IsNullOrEmpty(x.MemberType.Code) &&
                 x.PostalCode >= obj.PostalFrom && x.PostalCode <= obj.PostalTo &&
                 x.MemberType.Code == obj.MemberTypeCode;


        }
    */

        //--------------------------------------------------------------------------------------------LabelReport
        [HttpGet]
        public ActionResult LabelReport()
        {
            //PostalFrom=' + PostalFrom + '&PostalTo=' + PostalTo + '&MemberTypeCode=' + MemberTypeCode + '&FromBirthday=' + FromBirthday
            //if (isPartialView)
            //    return PartialView();
            //return View();
            TempData["CanOpenReport"] = false;// labelReportStatus.CanOpenReport;
            return View();
        }

        [HttpGet]
        public ActionResult GetTimeElapsedForLabelReportPrintJob()
        {
            return Json(TempData["LabelReportStatus"], JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PrintLabelReport(MemberLabelReportParameters obj, bool isPartialView = false, string type = "json")
        {
            db.Configuration.AutoDetectChangesEnabled = false;
            var result = new LabelReportStatusCode()
            {
                StatusCode = LabelReportStatusCode.Code.OK,
                Message = BootstrapResources.Resources.Success
            };
            var labelReportStatus = new LabelReportStatus()
            {
                ItemsDone = 0,
                ItemsTotal = 0,
                TimeSpent = 0,
                CanOpenReport = true
            };
            TempData["LabelReportStatus"] = labelReportStatus;
            if (obj != null)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
                hub.Clients.All.broadcastProgress("Leitar...");
                //var members = db.Members.FilterDeleted().AsQueryable();
                var members = db.Members.Where(x=>x.ChangeEvent != null && x.ChangeEvent.DeletedOn == null && x.PostalCode >= obj.PostalFrom && x.PostalCode <= obj.PostalTo && x.MemberType.Code == obj.MemberTypeCode &&
                    (x.LastSignOn != null && x.LastSignOn.From.HasValue && x.LastSignOn.To.HasValue && obj.SignOnFrom.HasValue && obj.SignOnTo.HasValue &&
                    ((obj.MemberTypeCode != "al" && DateTime.Compare(x.LastSignOn.From.Value, obj.SignOnFrom.Value) >= 0 && DateTime.Compare(x.LastSignOn.To.Value, obj.SignOnTo.Value) <= 0)) || obj.MemberTypeCode == "al") &&
                    string.Compare(x.JobCode, obj.MemberJobCodeFrom) >= 0 && string.Compare(x.JobCode, obj.MemberJobCodeTo) <= 0).AsQueryable();

                labelReportStatus.ItemsTotal = 0;
                //hub.Clients.All.broadcastProgress("Filtrerar...");
                //members = members.Where(x => FilterMembersForLabels(x, obj));
                var memberCount = members.Count();
                if (ModelState.IsValid)
                {
                    var i = 0;
                    //var list = members.ToList();
                    var eti_id = GetUserIdCode();
                    var eti_dato = DateTime.Now;
                    var s = DateTime.UtcNow;
                    hub.Clients.All.broadcastProgress("Dagførir...");
                    foreach (var member in members)
                    {
                        member.ETI_ID = eti_id;
                        member.ETI_DATO = eti_dato;
                        // attach the entity
                        db.Set<Member>().Attach(member);
                        // change its state to modified so entity framework can update the existing product instead of creating a new one
                        db.Entry(member).State = EntityState.Modified;
                        i++;
                        var progress = Math.Round(((i * 100.0) / (double)memberCount), 2);
                        hub.Clients.All.broadcastProgress(progress + "% liðugt");
                        //if (i % 10 == 0)
                        {
                            //system.diagnostics.debug.writeline(i);
                            labelReportStatus.ItemsDone = i;
                            labelReportStatus.TimeSpent = DateTime.UtcNow.Subtract(s).Seconds;
                            TempData["LabelReportStatus"] = labelReportStatus;
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine("timeSpent: " + timeElapsedForLabelReportPrintJob);
                    try
                    {
                        // Save changes to the DB
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.StatusCode = LabelReportStatusCode.Code.FAILED;
                        result.Message = ex.Message;
                    }
                }
                //return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);// Json("PostalFrom=" + obj.PostalFrom + "&PostalTo=" + obj.PostalTo + "&MemberTypeCode=" + obj.MemberTypeCode + "&FromBirthday=" + obj.FromBirthday, JsonRequestBehavior.AllowGet);
            }
            db.Configuration.AutoDetectChangesEnabled = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        static bool FilterMembersForLabels(Member x, MemberLabelReportParameters obj)
        {
            return /*!string.IsNullOrEmpty(x.MemberType.Code) &&*/
                 x.PostalCode >= obj.PostalFrom && x.PostalCode <= obj.PostalTo &&
                 x.MemberType.Code == obj.MemberTypeCode &&
                /*string.Compare(x.MemberType.Code, obj.MemberTypeCode, true) == 0 &&*/
                 (x.LastSignOn != null && x.LastSignOn.From.HasValue && x.LastSignOn.To.HasValue && obj.SignOnFrom.HasValue && obj.SignOnTo.HasValue &&
                 ((obj.MemberTypeCode != "al" && DateTime.Compare(x.LastSignOn.From.Value, obj.SignOnFrom.Value) >= 0 && DateTime.Compare(x.LastSignOn.To.Value, obj.SignOnTo.Value) <= 0)) || obj.MemberTypeCode == "al");
            //return x.Person != null &&
            //        x.Person.Entity != null &&
            //        x.Person.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault() != null &&
            //        x.Person.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal != null &&
            //        x.Person.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal.Code >= obj.PostalFrom &&
            //        x.Person.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal.Code <= obj.PostalTo &&
            //        !string.IsNullOrEmpty(x.MemberType.Code) &&
            //        string.Compare(x.MemberType.Code, obj.MemberTypeCode, true) == 0 &&
            //        (x.LastSignOn != null && x.LastSignOn.From.HasValue && x.LastSignOn.To.HasValue && obj.SignOnFrom.HasValue && obj.SignOnTo.HasValue &&
            //        DateTime.Compare(x.LastSignOn.From.Value, obj.SignOnFrom.Value) >= 0 &&
            //        DateTime.Compare(x.LastSignOn.To.Value, obj.SignOnTo.Value) <= 0);

            //string.Compare(x.Job.Code, obj.FromJobCode, true) >= 0 &&
            //string.Compare(x.Job.Code, obj.ToJobCode, true) <= 0 &&
            //x.Person.Birthday.HasValue &&
            //x.Person.Birthday.Value.Year >= obj.SignOnFrom &&
            //x.Person.Birthday.Value.Year <= obj.SignOnTo;
        }

        //private async Task<int> CountMembers(IQueryable<Member> members)
        //{
        //    return await members.CountAsync();
        //}

        [HttpPost]
        public ActionResult UpdateJobCode(int? memberId, string jobCode)
        {
            var member = db.Set<Member>().Where(x => x.Id == memberId).FirstOrDefault();
            var job = db.Jobs.Where(x => ("" + x.Code).ToLower().Equals(("" + jobCode).ToLower())).FirstOrDefault();
            if (member != null && job != null)
            {
                member.JobCode = job.Code;
                member.JobDescription = job.Description;
                member.JobTitle = jobCode;
                member.Job = job;
                var result = base.Update(member);
                return Json(result);
            }
            return new EmptyResult();
        }

        private bool UpdateLabel(Member member)
        {
            member.ETI_DATO = DateTime.UtcNow;
            member.ETI_ID = GetUserIdCode();
            return base.Update(member);
        }


        public ActionResult PrintCrewList()
        {
            return View();
        }

        public ActionResult CrewListReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        public ActionResult PrintMemberList()
        {
            return View();
        }

        public ActionResult MemberListReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        public ActionResult PrintAlkaList()
        {
            return View();
        }

        public ActionResult AlkaListReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        public ActionResult PrintAlkaReminder()
        {
            return View();
        }

        public ActionResult AlkaReminderReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        public ActionResult PrintAlkaReminderList()
        {
            return View();
        }

        public ActionResult AlkaReminderListReport(bool isPartialView = false)
        {
            if (isPartialView)
                return PartialView();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InlineUpdate([DataSourceRequest] DataSourceRequest request, MemberViewModel o)
        {
            if (o != null && ModelState.IsValid)
            {
                return UpdateUsingViewModel(o, (parent) =>
                {
                    //Convert the ViewModel to DB Object (Model)
                    var member = Find(o.Id);
                    //member = MemberHandler.Update(member, o, db);


                    //var entity = member.Person.Entity;
                    //var gender = db.PersonGenders.Where(x => x.Id == o.GenderId).FirstOrDefault();
                    //var personTitle = db.PersonTitles.Where(x => x.Id == o.TitleId).FirstOrDefault();
                    //personTitle = personTitle != null ? personTitle : member.Person.Title;
                    var person = member.Person;
                    person.ChangeEvent = UpdateChangeEvent(member.Person.ChangeEventId);
                    //person.FirstName = o.FirstName;
                    //person.MiddleName = o.MiddleName;
                    //person.LastName = o.LastName;
                    //person.Birthday = o.Birthday;
                    person.SSN = o.SSN;
                    //person.GenderId = o.GenderId;
                    //person.Title = personTitle;
                    //person.FullName = String.IsNullOrEmpty(("" + person.MiddleName).Trim()) ? (person.FirstName + " " + person.LastName) : (person.FirstName + " " + person.MiddleName + " " + person.LastName);
                    //person.IsAlive = o.IsAlive;

                    //var memberType = db.MemberTypes.Where(x => x.Id == o.MemberTypeId).FirstOrDefault();
                    //memberType = memberType != null ? memberType : member.MemberType;
                    //var job = db.Jobs.Where(x => x.Id == o.JobId).FirstOrDefault();
                    //job = job != null ? job : member.Job;

                    //member.ChangeEvent = UpdateChangeEvent(member.ChangeEventId);
                    ////member.NR = o.NR;
                    //member.Person = person;
                    //member.JobTitle = job.Code;
                    //member.Job = job;
                    //member.MemberType = memberType;
                    ////member.MemberTypeId = o.MemberTypeId;

                    //member.MemberTypeCode = memberType != null ? memberType.Code : "";
                    //member.MemberTypeDescription = memberType != null ? memberType.Description : "";
                    //member.JobCode = job != null ? job.Code : "";
                    //member.JobDescription = job != null ? job.Description : "";
                    //member.GenderCode = gender != null ? gender.Code : null;

                    return member;
                });
                //productService.Update(obj);
            }

            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }
    }
}