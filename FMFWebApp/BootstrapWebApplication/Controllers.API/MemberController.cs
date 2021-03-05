using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using BootstrapWebApplication.BLL;
using System.Globalization;
using Kthusid;
using System.Web.Http.OData;

namespace BootstrapWebApplication.Controllers.API
{
    public class MemberController : ApiController<Member>
    {
        protected override IQueryable<Member> PagedListFilter(IQueryable<Member> list, string filterName = null)
        {
            DateTime dt = DateTime.Now;
            if (!DateTime.TryParseExact(filterName, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                if (!DateTime.TryParseExact(filterName, "dMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    DateTime.TryParseExact(filterName, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                }
            }
            return list.Where(s => s.Person.Birthday.Value.Equals(dt)
                                    || s.Id.ToString().ToLower().Contains(filterName.ToLower())
                //|| s.Person.PrimaryPhone.RawNumber.ToLower().Contains(filterName.ToLower())
                                   || s.Person.SSN.ToLower().Contains(filterName.ToLower())
                //|| s.Person.Birthday.Value.ToShortDateString().ToLower().Contains(filterName.ToLower())
                                   || s.Person.FirstName.ToLower().Contains(filterName.ToLower())
                                   || s.Person.MiddleName.ToLower().Contains(filterName.ToLower())
                                   || s.Person.LastName.ToLower().Contains(filterName.ToLower())
                                   ||(String.IsNullOrEmpty(s.Person.MiddleName) ? (s.Person.FirstName + " " + s.Person.LastName) : (s.Person.FirstName + " " + s.Person.MiddleName + " " + s.Person.LastName)).ToLower().Equals(filterName.ToLower())
            );
        }

        [ResponseType(typeof(Member))]
        public override HttpResponseMessage Get(string sortName = "", string sortType = "", string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return base.Get(sortName, sortType, filterName, page, pageSize, type);
        }

        // GET: api/Member/5
        [ResponseType(typeof(Member))]
        public override HttpResponseMessage Get(int id, string type = null)
        {
            return base.Get(id, type);
        }

        // PUT: api/Member/5
        [ResponseType(typeof(void))]
        public override IHttpActionResult Put(int id, Member Member)
        {
            return base.Put(id, Member);
        }

        // PUT: api/Member/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, string MemberObject)
        {
            Member Member = JsonConvert.DeserializeObject<Member>(MemberObject);
            return base.Post(Member);
        }

        // POST: api/Member
        [ResponseType(typeof(Member))]
        public override IHttpActionResult Post([Bind(Include = "Id,PersonId,Comment,JobTitle,LastSignOnId,JobId,StatId,PostNrId,LSLAGId,PRI_OWN,BETALER,M_STATUS,ChangeEventId")] Member Member)
        {
            return base.Post(Member);
        }

        [ResponseType(typeof(Member))]
        public override IHttpActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [ResponseType(typeof(ICollection<BankAccount>))]
        public HttpResponseMessage BankAccounts(int? id, string type = null)
        {
            return GetRelatedData(id, "BankAccounts", type);
        }

        [ResponseType(typeof(ICollection<SignOn>))]
        public HttpResponseMessage SignOns(int? id, string type = null)
        {
            return GetRelatedData(id, "SignOns", type);
        }

        [ResponseType(typeof(ICollection<Member>))]
        [System.Web.Http.HttpGet]
        [EnableQuery]
        public HttpResponseMessage Find(string filter, string type = null)
        {
            var membersFiltered = new MemberHandler().GetMembersFiltered(db.Members, filter, 3);
            //var membersFilteredViewModel = membersFiltered.Select(m => new MemberViewModel()
            //{
            //    Id = m.Id,
            //    FullName = (m.Person != null) ? (m.Person.FirstName + " " + (m.Person.MiddleName.Trim().Length > 0 ? " " + m.Person.MiddleName : "") + m.Person.LastName) : "",
            //    Birthday = (m.Person != null) ? m.Person.Birthday : null,
            //    MemberType = (m.MemberType != null) ? m.MemberType.Description : "",
            //    JobTitle = m.JobTitle,
            //    PRI_OWN = m.PRI_OWN,
            //    BETALER = m.BETALER,
            //    M_STATUS = m.M_STATUS
            //});
            return Request.GetResponse(membersFiltered, type);
        }

        [Obsolete("Use /MemberController/MemberHolidayPaysForPeriod_Read instead")]
        [ResponseType(typeof(MemberHolidayPayOverviewViewModel))]
        public HttpResponseMessage GetHolidayPaySumForYear(int id, int from, int to, string type = null)
        {
            var member = Find(id);
            if (member == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var mh = new MemberHandler(member);
            var result = new MemberHolidayPayOverviewViewModel(member);
            for (int year = from; year <= to; year++)
            {
                result.MemberHolidayPayYearTotals.Add(mh.GetHolidayPaySumForYear(year));
            }
            return Request.GetResponse(result, type);
        }

        [ResponseType(typeof(MemberStatisticsViewModel))]
        public HttpResponseMessage GetStatistics(int id, int year, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var handler = new MemberHandler(obj);
            return Request.GetResponse(handler.GetStatistics(year), type);
        }

        [ResponseType(typeof(MemberLastPaymentViewModel))]
        public HttpResponseMessage GetLastPayment(int id, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var handler = new MemberHandler(obj);
            return Request.GetResponse(handler.GetLastPayment(), type);
        }

        [ResponseType(typeof(MemberCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetMemberCreateRelatedDataListViewModel(string type = null)
        {
            var viewModel = new MemberCreateEditGetRelatedDataViewModel()
            {
                Jobs = db.Jobs.FilterDeleted().ToList(),
                MemberTypes = db.MemberTypes.FilterDeleted().ToList(),
                PostalCodes = db.Postals.FilterDeleted().ToList(),
                Countries = db.Countries.FilterDeleted().ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

        [ResponseType(typeof(MemberCreateEditGetRelatedDataViewModel))]
        public HttpResponseMessage GetMemberEditRelatedDataListViewModel(string type = null)
        {
            var viewModel = new MemberCreateEditGetRelatedDataViewModel()
            {
                Jobs = db.Jobs.FilterDeleted().ToList(),
                MemberTypes = db.MemberTypes.FilterDeleted().ToList()
            };
            return Request.GetResponse(viewModel, type);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage PrintEnvelope(int id, string paperSize, int xPos, int yPos, string fontFamily, int fontSize, string fontStyle, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if (UpdateLabel(obj))
            {
                var user = GetUser();
                if (user != null)
                {
                    if (user.UserSettings == null)
                    {
                        var response = Request.CreateResponse(HttpStatusCode.Found);
                        response.Headers.Location = new Uri("/Account/EditUserSettings", UriKind.Relative);
                        return response;
                    }
                    else
                    {
                        var printerName = user.UserSettings.LabelPrinterName;
                        var pageSettings = new PrintPageSettings()
                        {
                            PaperSize = paperSize,
                            XPos = xPos,
                            YPos = yPos,
                            FontFamily = fontFamily,
                            FontSize = fontSize,
                            FontStyle = fontStyle,
                            PrinterName = printerName
                        };
                        return Request.GetResponse(new MemberHandler(obj).PrintEnvelope(pageSettings, GetUser()), type);
                    }
                }
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage PrintSignOns(int id, string fontFamily, int fontSize, string fontStyle, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if (UpdateLabel(obj))
            {
                return Request.GetResponse(new MemberHandler(obj).PrintSignOns(fontFamily, fontSize, fontStyle), type);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        private bool UpdateLabel(Member member)
        {
            member.ETI_DATO = DateTime.UtcNow;
            member.ETI_ID = GetUserId();
            return Update(member);
        }

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(ICollection<SignOn>))]
        public HttpResponseMessage SignOns(int id, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj.SignOns.FilterDeleted().ToList(), type);
        }

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(ICollection<BankAccount>))]
        public HttpResponseMessage BankAccounts(int id, string type = null)
        {
            var obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj.BankAccounts.FilterDeleted().ToList(), type);
        }
    }
}