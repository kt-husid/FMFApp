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
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers
{
    public enum SignOnStatusCode
    {
        TripSignOnsIsFull = 1,
        MemberAlreadySignedOn = 2
    }

    public class SignOnController : Controller<SignOn>
    {
        //public override ActionResult Read(DataSourceRequest request, int? id = null)
        //{
        //    var trip = db.Set<Trip>().Where(x => x.Id == id).FirstOrDefault();
        //    if (trip != null)
        //    {
        //        db.Entry(trip).Collection(x => x.TripDeductions).Load();
        //        db.Entry(trip).Collection(x => x.TripLines).Load();
        //        db.Entry(trip).Collection(x => x.SignOns).Load();
        //    }
        //    return base.Read(request, id);
        //}

        protected override IQueryable<SignOn> PagedListFilter(IQueryable<SignOn> list, string filterName = null)
        {
            return list.Where(s => s.ShipCode.ToString().Contains(filterName.ToUpper()));
        }

        protected override void AddViewBag(SignOn obj)
        {
            //if (Phone != null)
            //{
            //    ViewBag.PersonId = Phone.PersonId;
            //}
            //ViewBag.PersonId = new SelectList(db.Persons.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.FullName,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", Phone == null ? 1 : Phone.PersonId);



            //ViewBag.AddressId = new SelectList(db.Addresses, "Id", "CareOf");
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.CompanyId = new SelectList(db.Companies, "Id", "CID");
            //ViewBag.JobId = new SelectList(db.Jobs, "Id", "Code");
            //ViewBag.MemberId = new SelectList(db.Members, "Id", "Comment");
            //ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "Id", "Code");
            //ViewBag.ShipId = new SelectList(db.Ships, "Id", "Code");
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies, "Id", "Name");
            //ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Code");
            //ViewBag.TR_HOVD_Id = new SelectList(db.Trips, "Id", "TripId");
            //ViewBag.ShipTypeId = new SelectList(db.ShipTypes, "Id", "TEKSTUR");
        }

        public ActionResult TripSignOns(int? id)
        {
            var trip = db.Trips.Where(x => x.Id == id).FirstOrDefault();
            if (trip != null)
            {
                db.Entry(trip).Collection(x => x.TripLines).Load();
                db.Entry(trip).Collection(x => x.TripDeductions).Load();
                db.Entry(trip).Collection(x => x.SignOns).Load();
            }
            return GetRelatedDataForParent<Trip>(id, "TripSignOns");
            //return GetRelatedData(id, "TripSignOns");
        }

        public ActionResult ShipSignOns(int? id)
        {
            return GetRelatedDataForParent<Ship>(id, "ShipSignOns");
            //return GetRelatedData(id, "SignOns");
        }

        // Moved to: /TripController/ReadTripSignOns
        //public ActionResult ReadShipSignOns([DataSourceRequest] DataSourceRequest request, int id, DateTime from, DateTime to)
        //{
        //    var ship = db.Set<Ship>().Find(id);
        //    if (ship != null)
        //    {
        //        if (request.Filters.Count == 1)
        //        {
        //            var filter = request.Filters[0] as Kendo.Mvc.CompositeFilterDescriptor;
        //            if (filter.FilterDescriptors.Count == 2)
        //            {
        //                var filter1 = filter.FilterDescriptors[0] as Kendo.Mvc.FilterDescriptor;
        //                var filter2 = filter.FilterDescriptors[1] as Kendo.Mvc.FilterDescriptor;
        //                if (filter1.Member == "from")
        //                {
        //                    DateTime.TryParseExact(filter1.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out from);
        //                    //DateTime.TryParse(filter1.Value as string, out from);
        //                }
        //                if (filter2.Member == "to")
        //                {
        //                    DateTime.TryParseExact(filter2.Value as string, Core.Settings.Instance.DateTimeFormatReporting, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out to);
        //                    //DateTime.TryParse(filter2.Value as string, out to);
        //                }
        //            }
        //        }
        //        request.Filters.Clear();


        //        var result = new List<ShipSignOnViewModel>();
        //        var trips = ship.Trips.Where(x => x.From.HasValue && x.From.Value.CompareTo(from) >= 0 && x.To.HasValue && x.To.Value.CompareTo(to) <= 0);
        //        foreach (var trip in trips)
        //        {
        //            var tempResult = trip.SignOns.Select(m => new ShipSignOnViewModel()
        //            {
        //                Id = m.Id,
        //                From = m.From,
        //                To = m.To,
        //                ShipCode = m.ShipCode,
        //                JobCode = m.JobCode,
        //                DG_IALT = ((m.To.HasValue && m.From.HasValue) ? m.To.Value.Subtract(m.From.Value).Days + 1 : 0),
        //                Birthday = ((m.Member != null && m.Member.Person != null) ? m.Member.Person.Birthday : null),
        //                FullName = ((m.Member != null && m.Member.Person != null) ? m.Member.Person.FullName : "")
        //                //ChangeEvent = m.ChangeEvent
        //            });
        //            result.AddRange(tempResult);
        //        }
        //        return Json(result.ToDataSourceResult(request));
        //    }
        //    return Json(null);
        //}

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var trip = db.Set<Trip>().Find(id);
            if (trip != null)
            {
                // load Trip SignOns explicitly
                db.Entry(trip).Collection(x => x.SignOns).Load();

                var result = trip.SignOns.FilterDeleted().OrderByDescending(x => x.Id).Select(m => new TripTripSignOnViewModel()
                {
                    Id = m.Id,
                    MemberId = m.MemberId,
                    PersonNumber = (m.Member != null) ? m.Member.NR : -1,
                    PersonFullName = (m.Member != null && m.Member.Person != null) ? m.Member.Person.FullName : "",
                    Birthday = (m.Member != null && m.Member.Person != null) ? m.Member.Person.Birthday : null,
                    From = m.From,
                    To = m.To,
                    JobCode = m.JobCode,
                    PART_NotAdjusted = m.JobWhileSignedOn.PART,
                    PART = m.PART,
                    TIL_PR_DG = m.TIL_PR_DG,
                    TIL_PR_TUR = m.TIL_PR_TUR,
                    KR_IALT = m.KR_IALT,
                    HasInsurance = m.HasInsurance,
                    MembershipPayment = (m.HasInsurance && m.KR_IALT.HasValue) ? (m.KR_IALT.Value * 0.0125m) : 0m
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetJobFromLastSignOn(int? memberId)
        {
            var jobCode = "";
            var member = db.Members.Where(x => x.Id == memberId).FirstOrDefault();
            if (member != null)
            {
                // load SignOns explicitly
                db.Entry(member).Collection(x => x.SignOns).Load();

                var lastSignOn = member.SignOns.FilterDeleted().OrderByDescending(x => x.SignOnNumber).FirstOrDefault();
                if (lastSignOn != null)
                {
                    jobCode = lastSignOn.JobCode;
                }
                else
                {
                    if (member.Job != null)
                    {
                        jobCode = member.Job.Code;
                    }
                }
            }
            return Json(jobCode, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            var viewModel = new TripSignOnCreateEditViewModel()
            {
                From = parent.From,
                To = parent.To
            };
            AddViewBag(null);
            return Create("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripSignOnCreateEditViewModel vmObj)
        {
            // Convert the ViewModel to DB Object (Model)
            var trip = db.Trips.Where(x => x.Id == vmObj.TripId).FirstOrDefault();

            if (trip == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            // load Trip SignOns explicitly
            db.Entry(trip).Collection(x => x.SignOns).Load();
            var signOns = trip.SignOns.FilterDeleted();
            //if (signOns.Where(x => x.MemberId == vmObj.MemberId).Count() > 0)
            //{
            //    return Json(SignOnStatusCode.MemberAlreadySignedOn);
            //}

            //var deductionType = db.DeductionTypes.Where(x => x.Id == o.DeductionTypeId).FirstOrDefault();
            var job = db.Jobs.Where(x => x.Code == vmObj.JobCode).FirstOrDefault();
            var member = db.Members.Where(x => x.Id == vmObj.MemberId).FirstOrDefault();
            var part = vmObj.PART.HasValue ? vmObj.PART.Value : 1;
            //if (trip == null || job == null || member == null || !vmObj.From.HasValue || !vmObj.To.HasValue || !vmObj.PART.HasValue || !vmObj.TIL_PR_DG.HasValue || !vmObj.TIL_PR_TUR.HasValue)
            if (trip == null || job == null || member == null || !vmObj.From.HasValue || !vmObj.To.HasValue)
            {
                return null;
            }
            vmObj.TIL_PR_DG = vmObj.TIL_PR_DG.HasValue ? vmObj.TIL_PR_DG : 0m;
            vmObj.TIL_PR_TUR = vmObj.TIL_PR_TUR.HasValue ? vmObj.TIL_PR_TUR : 0m;
            //if (vmObj.MemberId.HasValue && member.Id == vmObj.MemberId.Value)
            //{
            //    return null;
            //}

            var lastSignOn = db.SignOns.OrderByDescending(x => x.SignOnNumber).FirstOrDefault();

            // only create and add a new SignOn if it's lower than the amount of Crew members assigned to the trip!
            // Deactivated because this rule isn't always valid
            //var tripSignOnCount = trip.SignOns.FilterDeleted().Count();
            //if (tripSignOnCount < trip.CrewIncludingStayingAtHome)
            //{
            SignOn dbObj = null;
            var result = CreateRelatedObjectUsingViewModel<Trip, SignOn>(vmObj.TripId, vmObj, (parent) =>
            {
                var user = GetUser();
                var KR_IALT = (vmObj.KR_IALT.HasValue && vmObj.KR_IALT.Value > 0) ? vmObj.KR_IALT.Value : SignOnHandler.CalcKR_IALT(parent, vmObj);
                // load Trip SignOns explicitly
                db.Entry(parent).Collection(x => x.SignOns).Load();
                var laborInsuranceAmountPerDay = (user != null && user.AppSettings != null) ? user.AppSettings.LaborInsuranceAmountPerDay : 0m;
                dbObj = new SignOn()
                {
                    SignOnNumber = lastSignOn != null ? lastSignOn.SignOnNumber + 1 : null,
                    PersonNumber = member.NR,
                    Year = vmObj.To.HasValue ? vmObj.To.Value.ToString("yy") : DateTime.Now.ToString("yy"),
                    From = vmObj.From,
                    To = vmObj.To,
                    SK_ID = "",
                    PART = part,
                    KR_IALT = KR_IALT,
                    //KG_IALT = null,
                    //ID_TUR = trip.TripId,
                    //TR_NR = trip.TripNumber,
                    //FRT_PART = 0m,
                    TIL_PR_DG = vmObj.TIL_PR_DG,
                    TIL_PR_TUR = vmObj.TIL_PR_TUR,
                    //PART_NETTO = 0m,
                    EmployerNumber = trip.EmployerNumber,
                    I_PART = (KR_IALT.HasValue ? KR_IALT : 1m) / ((vmObj.PART.HasValue || vmObj.PART.Value != 0) ? vmObj.PART : 1m), // correctly calculated?
                    FRT_NR = 0,
                    // !!! NOTE!!!
                    // if TRYG_JN == 'J', then 'samlagstrygging' has to be calculated
                    TRYG_JN = job.HasInsurance ? "J" : "N",
                    HasInsurance = job.HasInsurance,
                    // todo: calculate samlagstrygging !!!
                    LaborInsuranceAmountPerDay = laborInsuranceAmountPerDay,
                    //TRYG_KR = job.HasInsurance ? new Nullable<decimal>(0m) : null,// trygJN.ToLower().Equals("j") ? new Nullable<decimal>(0m) : null,
                    TRYG_KVT = String.Empty, // todo: what's this for?
                    Member = member,
                    ShippingCompanyId = (trip != null && trip.Ship != null) ? trip.Ship.ShippingCompanyId : null,
                    Trip = trip,
                    JobWhileSignedOn = job,
                    MemberType = member != null ? member.MemberType : null,
                    Company = trip.PortOfLanding,
                    ShipCode = (trip != null && trip.Ship != null) ? trip.Ship.Code : null,
                    ShipName = (trip != null && trip.Ship != null) ? trip.Ship.Name : null
                };
                db.SignOns.Add(dbObj);
                dbObj.Trip = TripHandler.UpdateTripInfo(parent, laborInsuranceAmountPerDay, db);
                return dbObj;
            });
            if (member != null && dbObj != null)
            {
                member.LastSignOnId = dbObj.Id;
                base.Update<Member>(member);
            }
            return result;
            //}
            //return Json(SignOnStatusCode.TripSignOnsIsFull);
        }

        [HttpGet]
        public ActionResult Edit(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load Trip SignOns explicitly
            db.Entry(parent).Collection(x => x.SignOns).Load();

            var obj = parent != null ? parent.SignOns.FilterDeleted().Where(x => x.Id == id).FirstOrDefault() : null;

            ViewModelBaseWithChangeEvent viewModel = null;
            if (obj != null)
            {
                AddViewBag(obj);
                viewModel = new TripDeductionCreateOrEditViewModel()
                {

                };
            }
            return Edit("CreateOrEdit", viewModel, parent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripSignOnCreateEditViewModel vmObj)
        {
            vmObj.TIL_PR_DG = vmObj.TIL_PR_DG.HasValue ? vmObj.TIL_PR_DG : 0m;
            vmObj.TIL_PR_TUR = vmObj.TIL_PR_TUR.HasValue ? vmObj.TIL_PR_TUR : 0m;

            return UpdateRelatedObjectUsingViewModel<Trip, SignOn>(vmObj.TripId, vmObj, (parent) =>
            {
                var job = db.Jobs.Where(x => x.Code == vmObj.JobCode).FirstOrDefault();
                var member = db.Members.Where(x => x.Id == vmObj.MemberId).FirstOrDefault();
                var user = GetUser();
                var laborInsuranceAmountPerDay = (user != null && user.AppSettings != null) ? user.AppSettings.LaborInsuranceAmountPerDay : 0m;
                var KR_IALT = (vmObj.KR_IALT.HasValue && vmObj.KR_IALT.Value > 0) ? vmObj.KR_IALT.Value : SignOnHandler.CalcKR_IALT(parent, vmObj);
                // load Trip SignOns explicitly
                db.Entry(parent).Collection(x => x.SignOns).Load();

                var trip = TripHandler.UpdateTripInfo(parent, laborInsuranceAmountPerDay, db);

                //Convert the ViewModel to DB Object (Model)
                //var dbObj = parent.SignOns.FilterDeleted().Where(x => x.Id == vmObj.Id && x.TripId == vmObj.TripId && x.MemberId == vmObj.MemberId).FirstOrDefault();
                var dbObj = parent.SignOns.FilterDeleted().Where(x => x.Id == vmObj.Id).FirstOrDefault();
                if (dbObj != null)
                {
                    dbObj.From = vmObj.From;
                    dbObj.To = vmObj.To;
                    dbObj.PART = vmObj.PART;
                    dbObj.KR_IALT = KR_IALT;
                    dbObj.TIL_PR_DG = vmObj.TIL_PR_DG;
                    dbObj.TIL_PR_TUR = vmObj.TIL_PR_TUR;
                    dbObj.Member = member;
                    dbObj.JobWhileSignedOn = job;
                    dbObj.TRYG_JN = job.HasInsurance ? "J" : "N";
                    //dbObj.TRYG_KVT = string.Empty; // todo: what's this for?
                    dbObj.HasInsurance = job.HasInsurance;
                    dbObj.LaborInsuranceAmountPerDay = laborInsuranceAmountPerDay;
                    dbObj.Trip = trip;
                    dbObj.ShipCode = (trip != null && trip.Ship != null) ? trip.Ship.Code : null;
                    dbObj.ShipName = (trip != null && trip.Ship != null) ? trip.Ship.Name : null;
                }
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id, int? tripId)
        {
            var parent = db.Set<Trip>().Find(tripId);
            // load Trip SignOns explicitly
            db.Entry(parent).Collection(x => x.SignOns).Load();
            var obj = parent != null ? parent.SignOns.Where(x => x.Id == id).FirstOrDefault() : null;
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TripTripSignOnViewModel dbObj)
        {
            var id = dbObj.Id;
            var parent = db.Set<Trip>().Find(dbObj.TripId);
            db.Entry(parent).Collection(x => x.SignOns).Load();
            var obj = parent != null ? parent.SignOns.Where(x => x.Id == id).FirstOrDefault() : null;
            if (obj != null)
            {
                obj.Trip.SignOns.Remove(obj);
                // Update the trip info after signon has been deleted
                obj.Trip = TripHandler.UpdateTripInfo(parent, obj.LaborInsuranceAmountPerDay, db);
                base.Update<Trip>(obj.Trip);

                
                // update the signon
                base.Update<SignOn>(obj);

                var member = obj.Member;
                // load SignOns explicitly
                db.Entry(member).Collection(x => x.SignOns).Load();
                var lastSignOn = member.SignOns.FilterDeleted().OrderByDescending(x => x.SignOnNumber).FirstOrDefault();
                if (lastSignOn != null)
                {
                    // update the member last signon id
                    member.LastSignOnId = lastSignOn.Id;
                    base.Update<Member>(obj.Member);
                }
                //if (obj != null)
                //{
                //    db.SignOns.Remove(obj);
                //}
                //db.SaveChanges();
            }
            return DeleteObject<SignOn, Trip>(dbObj.Id, dbObj.TripId);
        }
    }
}
