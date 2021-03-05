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
    public class CompanyController : Controller<Company>
    {
        public JsonResult GetAll()
        {
            var result = db.Companies.FilterDeleted().Select(m => new CompanyViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description
                //ChangeEvent = m.ChangeEvent
            });
            return Json(new DataSourceResult() { Data = result, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter)
        {
            filter = !string.IsNullOrEmpty(filter) ? filter : Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Companies as IQueryable<Company>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Equals(filter.ToLower()) || ("" + s.Name).ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new CompanyViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description
                //ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Find2(string filter)
        {
            var listFiltered = db.Companies as IQueryable<Company>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => ("" + s.Code).ToLower().Contains(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower()));
            }
            var result = listFiltered.Select(m => new CompanyViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description,
                ChangeEvent = m.ChangeEvent
            });
            return Json(new DataSourceResult() { Data = result, Total = result.Count() }, JsonRequestBehavior.AllowGet);
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
            var companiesFiltered = db.Companies as IQueryable<Company>;
            if (filter.Length >= 2)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                companiesFiltered = companiesFiltered.Where(s => s.Id.ToString().ToLower().Contains(filter.ToLower())
                                        || s.Code.ToString().ToLower().Contains(filter.ToLower())
                                        | s.Name.ToLower().Contains(filter.ToLower())
                );
            }
            return GetCompanies(request, companiesFiltered);
        }


        private DataSourceResult GetCompanies(DataSourceRequest request, IQueryable<Company> items)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = items.FilterDeleted().Select(m => new CompanyViewModel()
            {
                Id = m.Id,
                //CID = m.CID,
                Code = m.Code,
                Name = m.Name,
                City = (m.PrimaryAddress != null && m.PrimaryAddress.Postal != null) ? m.PrimaryAddress.Postal.CityName : "-",
                Postal = (m.PrimaryAddress != null && m.PrimaryAddress.Postal != null) ? m.PrimaryAddress.Postal.Code : -1,
                Description = m.Description,
                ContactCompanyName = m.ContactCompanyName,
                ContactPersonName = m.ContactPersonName
            });
            return result.ToDataSourceResult(request);
        }

        protected override IQueryable<Company> PagedListFilter(IQueryable<Company> list, string filterName = null)
        {
            return list.Where(s => s.Code.ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToUpper().Contains(filterName.ToUpper()));
        }

        //[HttpGet]
        //public ActionResult LabelInfo(int? id)
        //{
        //    return GetRelatedDataForParent<Ship>(id, "LabelInfo");
        //}s

        protected override void AddViewBag(Company obj)
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

        private void AddViewBagForCreateEdit(Company obj)
        {
            //ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies.ToList().Select(m => new SelectListItem
            //{
            //    Text = m.Name,
            //    Value = m.Id.ToString()
            //}), "Value", "Text", obj != null ? obj.ShippingCompanyId : null);

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
            IViewModelBase viewModel = new CompanyCreateOrEditViewModel()
            {

            };
            return Create<Company>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyCreateOrEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                var entity = new Entity();
                db.Entities.Add(entity);

                //var shippingCompany = db.ShippingCompanies.Where(x => x.Id == o.ShippingCompanyId).FirstOrDefault();

                // Convert the ViewModel to DB Object (Model)
                var dbObj = new Company()
                {
                    CID = o.CID,
                    Code = o.Code,
                    Name = o.Name,
                    Description = o.Description,
                    ContactCompanyName = o.ContactCompanyName,
                    ContactPersonName = o.ContactPersonName,
                    //AVR_IALT = ?,
                    //KG_IALT = ?,
                    //LAST_DATO = ?,
                    //LAST_ID = ?,
                    Entity = entity
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var company = Find(id);
            AddViewBagForCreateEdit(company);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (company != null)
            {
                viewModel = new CompanyCreateOrEditViewModel()
                {
                    CID = company.CID,
                    Code = company.Code,
                    Name = company.Name,
                    Description = company.Description,
                    ContactCompanyName = company.ContactCompanyName,
                    ContactPersonName = company.ContactPersonName
                    //AVR_IALT = ?,
                    //KG_IALT = ?,
                    //LAST_DATO = ?,
                    //LAST_ID = ?
                };
            }
            return Edit("CreateOrEdit", viewModel, company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyCreateOrEditViewModel o)
        {
            return UpdateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var company = Find(o.Id);
                //var entity = ship.Entity;
                company.CID = o.CID;
                company.Code = o.Code;
                company.Name = o.Name;
                company.Description = o.Description;
                company.ContactCompanyName = o.ContactCompanyName;
                company.ContactPersonName = o.ContactPersonName;
                //company.AVR_IALT = ?,
                //company.KG_IALT = ?,
                //company.LAST_DATO = ?,
                //company.LAST_ID = ?,
                return company;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Company>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CompanyViewModel dbObj)
        {
            return DeleteObject<Company, Company>(dbObj.Id);
        }
        
        [HttpPost]
        public ActionResult ExportToExcel(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }
    }
}