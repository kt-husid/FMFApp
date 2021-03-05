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
using System.Globalization;
using Kendo.Mvc;

namespace BootstrapWebApplication.Controllers
{
    public class ShippingCompanyController : Controller<ShippingCompany>
    {
        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.ShippingCompanies.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue) as IQueryable<ShippingCompany>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => s.Code.ToString().ToLower().Contains(filter.ToLower()) || s.EmployerNumber.ToString().ToLower().Contains(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new ShippingCompanyViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        protected override ShippingCompany DetailsGetHook(ShippingCompany obj)
        {
            ViewBag.StatusText = "-";
            var statusCode = (obj != null && obj.Status != null) ? obj.Status.Code : null;
            var status = db.Statuses.Where(x => x.Code == statusCode).FirstOrDefault();
            if (status != null)
            {
                ViewBag.StatusText = status.Description;
            }
            var holidayPays = db.HolidayPay_HOVDs.Where(x => x.EmployerNumber == obj.EmployerNumber);
            ViewBag.HolidayPayTotal = MemberHandler.GetHolidayPaySum(holidayPays.ToList());
            return base.DetailsGetHook(obj);
        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var filter = "";
            if (request.Filters.Count > 0)
            {
                filter = (request.Filters[0] as Kendo.Mvc.FilterDescriptor).Value as string;
            }
            request.Filters.Clear();
            return Json(GetCompaniesFiltered(filter, request));
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        private DataSourceResult GetCompaniesFiltered(string filter, DataSourceRequest request)
        {
            var companiesFiltered = db.ShippingCompanies as IQueryable<ShippingCompany>;
            if (filter.Length > 3)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                companiesFiltered = companiesFiltered.Where(s => s.Code.ToString().ToLower().Contains(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower())
                );
            }
            return GetShippingCompanies(request, companiesFiltered);
        }


        private DataSourceResult GetShippingCompanies(DataSourceRequest request, IQueryable<ShippingCompany> items)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = items.FilterDeleted().Select(m => new ShippingCompanyViewModel()
            {
                Id = m.Id,
                //CID = m.CID,
                Code = m.Code,
                Name = m.Name,
                //City = "",//m.PrimaryAddress.Postal.CityName,
                //Postal = -1,// m.PrimaryAddress.Postal.PostalCode,
                //Description = m.Description,
                ContactCompanyName = m.ContactCompanyName,
                ContactPersonName = m.ContactPersonName
            });
            return result.ToDataSourceResult(request);
        }

        protected override IQueryable<ShippingCompany> PagedListFilter(IQueryable<ShippingCompany> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToString().ToUpper().Contains(filterName.ToUpper()) || s.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        //[HttpGet]
        //public ActionResult LabelInfo(int? id)
        //{
        //    return GetRelatedDataForParent<Ship>(id, "LabelInfo");
        //}s

        protected override void AddViewBag(ShippingCompany obj)
        {
            //ViewBag.AddressId = new SelectList(db.Addresses, "Id", "CareOf");
            //ViewBag.ChangeEventId = new SelectList(db.ChangeEvents, "Id", "CreatedByUserIdCode");
            //ViewBag.PhoneId = new SelectList(db.Phones, "Id", "Extension");

            //ViewBag.PortOfLandingId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text");
        }

        private void AddViewBagForCreateEdit(ShippingCompany obj)
        {
            //ViewBag.ShippingShippingCompanyId = new SelectList(db.ShippingCompanies.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.ShippingShippingCompanyId : null);

            //ViewBag.ShipTypeId = new SelectList(db.ShipTypes.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.TEKSTUR,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.ShipTypeId : null);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddViewBagForCreateEdit(null);
            IViewModelBase viewModel = new ShippingCompanyCreateOrEditViewModel()
            {

            };
            return Create<ShippingCompany>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShippingCompanyCreateOrEditViewModel o)
        {
            if (db.ShippingCompanies.Where(x => x.Code == o.Code).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Kotan " + o.Code + " er longu brúkt. Vinaliga brúka eitt annað nummar (aðra kotu)");// is unique. Already in use by another shipping company!");
                return PartialView("CreateOrEdit", o);
            }
            return CreateUsingViewModel(o, (parent) =>
            {
                return new ShippingCompanyHandler().Create(db, o.Code, o.Name, o.ContactCompanyName, o.ContactPersonName, CreateChangeEvent());
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ShippingCompany = Find(id);
            AddViewBagForCreateEdit(ShippingCompany);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (ShippingCompany != null)
            {
                viewModel = new ShippingCompanyCreateOrEditViewModel()
                {
                    //CID = ShippingCompany.CID,
                    Code = ShippingCompany.Code,
                    Name = ShippingCompany.Name,
                    //Description = ShippingCompany.Description,
                    ContactCompanyName = ShippingCompany.ContactCompanyName,
                    ContactPersonName = ShippingCompany.ContactPersonName
                };
            }
            return Edit("CreateOrEdit", viewModel, ShippingCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShippingCompanyCreateOrEditViewModel o)
        {
            //if (db.ShippingCompanies.Where(x => x.Code.ToString().ToLower().Equals(o.Code.ToString().ToLower())).FirstOrDefault() != null)
            //{
            //    ModelState.AddModelError("", "Code is unique. Already in use by another shipping company!");
            //    return PartialView("CreateOrEdit", o);
            //}
            return UpdateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var ShippingCompany = Find(o.Id);
                //ShippingCompany.CID = o.CID;
                ShippingCompany.Code = o.Code;
                ShippingCompany.EmployerNumber = o.Code;
                ShippingCompany.Name = o.Name;
                //ShippingCompany.Description = o.Description;
                ShippingCompany.ContactCompanyName = o.ContactCompanyName;
                ShippingCompany.ContactPersonName = o.ContactPersonName;
                return ShippingCompany;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<ShippingCompany>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShippingCompanyViewModel dbObj)
        {
            return DeleteObject<ShippingCompany, ShippingCompany>(dbObj.Id);
        }

        [HttpGet]
        public ActionResult Ships(int? id)
        {
            return GetRelatedDataForParent<ShippingCompany>(id, "Ships");
        }

        public ActionResult ShippingCompanyShips_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            return Json(GetShips(id, request));
        }

        private DataSourceResult GetShips(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var parent = Find(id);
            if (parent != null)
            {
                var result = parent.Ships.Select(m => new ShipViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Code = m.Code,
                    ShipType = m.ShipType != null ? m.ShipType.Description : "",
                    ShippingCompanyId = m.ShippingCompanyId,
                    ShippingCompanyName = m.ShippingCompany != null ? m.ShippingCompany.Name : null,
                    Status = m.Status,
                    ChangeEvent = null//m.ChangeEvent
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }

        [HttpGet]
        public ActionResult HolidayPayForMembers(int? id)
        {
            return GetRelatedDataForParent<ShippingCompany>(id, "HolidayPayForMembers");
        }

        public ActionResult ShippingCompanyHolidayPayForMembers_Read([DataSourceRequest] DataSourceRequest request, int id)
        {
            return Json(GetHolidayPayForPerson(id, request), JsonRequestBehavior.AllowGet);
        }

        private DataSourceResult GetHolidayPayForPerson(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var parent = Find(id);
            if (parent != null)
            {
                var dateFilter = request.Filters.Count > 0 ? (request.Filters[0] as FilterDescriptor).Value.ToString() : new DateTime(1970, 1, 1).ToShortDateString();//.Params["filter[filters][0][value]"];
                DateTime dt = DateTime.Now;
                DateTime.TryParseExact(dateFilter, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                request.Filters.Clear();
                var result = db.HolidayPay_HOVDs.Where(x => x.EmployerNumber == parent.EmployerNumber && DateTime.Compare(x.TransferDate.Value, dt) >= 0).Select(m => new ShippingCompanyHolidayPayForMemberViewModel()
                {
                    Id = m.Id,
                    BirthDay = (m.Member != null && m.Member.Person != null) ? m.Member.Person.Birthday : null,
                    FullName = (m.Member != null && m.Member.Person != null) ? (m.Member.Person.FirstName + " " + m.Member.Person.LastName) : null,
                    Amount = m.Amount,
                    TransferDate = m.TransferDate,
                    ART = m.ART,
                    TripNumber = null,
                    REG_NR_FF = m.REG_NR_FF,
                    KONTO_FF = m.KONTO_FF,
                    ChangeEvent = m.ChangeEvent
                });
                return result.ToDataSourceResult(request);
            }
            return null;
        }
    }
}