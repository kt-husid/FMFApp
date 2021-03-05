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
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Globalization;
using BootstrapWebApplication.BLL;
using AutoMapper;
using BootstrapWebApplication.Admin.Conversion;
using System.Data.Entity.Infrastructure;

namespace BootstrapWebApplication.Controllers
{
    public class ShipController : Controller<Ship>
    {

        public JsonResult GetAll()
        {
            var result = db.Ships.FilterDeleted().Select(m => new ShipViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                ShipType = m.ShipType != null ? m.ShipType.Description : null,
                Status = m.Status,
                //ChangeEvent = m.ChangeEvent
            });
            return Json(new DataSourceResult() { Data = result, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LabelFind([DataSourceRequest] DataSourceRequest request, string filter)
        {
            return Find(request, filter, true);
        }

        public JsonResult Find([DataSourceRequest] DataSourceRequest request, string filter, bool filterStatus = false)
        {
            filter = Request.Params["filter[filters][0][value]"];
            var listFiltered = db.Set<Ship>() as IQueryable<Ship>;
            if (filter.Length >= 2)
            {
                listFiltered = listFiltered.Where(s => s.Code.ToLower().Contains(filter.ToLower()) || s.Name.ToLower().Contains(filter.ToLower()));
            }
            if (filterStatus)
            {
                listFiltered = listFiltered.Where(x => ("" + x.Status).ToLower().Equals("OK") || string.IsNullOrEmpty(x.Status));
            }
            var result = listFiltered.FilterDeleted().Select(m => new ShipViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Code = m.Code,
                ShippingCompanyId = m.ShippingCompanyId,
                ShippingCompanyName = m.ShippingCompany.Name,
                ShipType = m.ShipType != null ? m.ShipType.Description : "",
                Status = m.Status,
                ChangeEvent = m.ChangeEvent
            });
            return Json(result.ToDataSourceResult(request).Data, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var filter = "";
            if (request.Filters.Count > 0)
            {
                filter = (request.Filters[0] as Kendo.Mvc.FilterDescriptor).Value as string;
            }
            request.Filters.Clear();
            return Json(GetShipsFiltered(filter, request));
        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            throw new NotImplementedException();
        }

        private DataSourceResult GetShipsFiltered(string filter, DataSourceRequest request)
        {
            var shipsFiltered = db.Ships as IQueryable<Ship>;
            if (filter.Length >= 2)
            {
                //DateTime dt = DateTime.Now;
                //DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                shipsFiltered = shipsFiltered.Where(s => s.Id.ToString().ToLower().Contains(filter.ToLower())
                                        || s.Code.ToString().ToLower().Contains(filter.ToLower())
                                        || (s.ShippingCompany != null && s.ShippingCompany.Name.ToString().ToLower().Contains(filter.ToLower()))
                                       || s.Name.ToLower().Contains(filter.ToLower())
                );
            }
            return GetShips(request, shipsFiltered);
        }


        private DataSourceResult GetShips(DataSourceRequest request, IQueryable<Ship> ships)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = ships.Select(m => new ShipViewModel()
            {
                Id = m.Id,
                Code = m.Code,
                ShipType = m.ShipType != null ? m.ShipType.Description : "",
                Name = m.Name,
                Status = m.Status,
                ShippingCompanyId = m.ShippingCompanyId,
                ShippingCompanyName = m.ShippingCompany != null ? m.ShippingCompany.Name : ""
            });
            return result.ToDataSourceResult(request);
        }

        protected override IQueryable<Ship> PagedListFilter(IQueryable<Ship> list, string filterName = null)
        {
            return list.Where(s => s.Id.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Code.ToString().ToUpper().Contains(filterName.ToUpper())
                                   || s.Name.ToString().ToUpper().Contains(filterName.ToUpper())
                                   );
        }

        [HttpGet]
        public ActionResult LabelInfo(int? id)
        {
            return GetRelatedDataForParent<Ship>(id, "LabelInfo");
        }

        protected override void AddViewBag(Ship ship)
        {
            ViewBag.PortOfLandingId = new SelectList(db.Companies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text");

            ViewBag.PairShipId = new SelectList(db.Ships.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text");
        }

        private void AddViewBagForCreateEditShip(Ship obj)
        {
            ViewBag.ShippingCompanyId = new SelectList(db.ShippingCompanies.ToList().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.ShippingCompanyId : null);

            ViewBag.ShipTypeId = new SelectList(db.ShipTypes.ToList().Select(m => new SelectListItem
            {
                Text = m.Description,
                Value = m.Id.ToString()
            }), "Value", "Text", obj != null ? obj.ShipTypeId : null);

            ViewBag.Status = new SelectList(db.Statuses.ToList().Select(m => new SelectListItem
            {
                Text = m.Description,
                Value = m.Description
            }), "Value", "Text", obj != null ? obj.Status : null);
        }

        protected override Ship DetailsGetHook(Ship obj)
        {
            var status = db.Statuses.Where(x => x.Code == obj.Status).FirstOrDefault();
            if (status != null)
            {
                ViewBag.StatusText = status.Description;
            }
            return base.DetailsGetHook(obj);
        }

        [HttpGet]
        public ActionResult Create(int? shippingCompanyId)
        {
            AddViewBagForCreateEditShip(null);
            var shippingCompany = db.ShippingCompanies.Where(x => x.Id == shippingCompanyId).FirstOrDefault();
            IViewModelBase viewModel = new ShipCreateOrEditViewModel()
            {
                ShippingCompanyCode = shippingCompany != null ? new Nullable<int>(shippingCompany.Code) : null,
                ShippingCompany = shippingCompany,
                ShippingCompanyId = shippingCompany != null ? new Nullable<int>(shippingCompany.Id) : null
            };
            return Create<Ship>("CreateOrEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipCreateOrEditViewModel o)
        {
            return CreateUsingViewModel(o, (parent) =>
            {
                var entity = new Entity();
                db.Entities.Add(entity);

                var shippingCompany = db.ShippingCompanies.Where(x => x.Id == o.ShippingCompanyId).FirstOrDefault();

                // Convert the ViewModel to DB Object (Model)
                var dbObj = new Ship()
                {
                    Code = o.ShipCode,
                    Name = o.Name,
                    ContactCompanyName = o.ContactCompanyName,
                    ContactPersonName = o.ContactPersonName,
                    //IND_REG = o.IND_REG,
                    //UD_REG = o.UD_REG,
                    Status = "",// o.Status,
                    //LN_IALT = o.LN_IALT,
                    SK_TYPA = "",
                    ShippingCompanyId = o.ShippingCompanyId,
                    Tonnage = o.Tonnage != null ? o.Tonnage : 0m,
                    HK = o.HK != null ? o.HK : 0m,
                    //TXT_LN = o.TXT_LN,
                    //BURT_DG = o.BURT_DG,
                    //TUR_IALT = o.TUR_IALT,
                    //LAST_DATO = o.LAST_DATO,
                    ShipTypeId = o.ShipTypeId,
                    //AV_IALT = o.AV_IALT,
                    //KG_IALT = o.KG_IALT,
                    //DG_IALT = o.DG_IALT,
                    ALT_ID = "",
                    Group = 0, // todo: 0 or 10 - what's this
                    ETI_DATO = null,
                    ETI_ID = null,
                    EmployerNumber = shippingCompany != null ? shippingCompany.EmployerNumber : null,
                    Entity = entity,

                    CountryCode = "",
                    CountryName = "",
                    AddressLine = "",
                    PostalCode =null,
                    CityName = "",
                    PhoneCountryCode = null,
                    PhoneNumber = ""
                };
                return dbObj;
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ship = Find(id);
            AddViewBagForCreateEditShip(ship);

            ViewModelBaseWithChangeEvent viewModel = null;
            if (ship != null)
            {
                viewModel = new ShipCreateOrEditViewModel()
                {
                    Name = ship.Name,
                    ShipCode = ship.Code,
                    ContactCompanyName = ship.ContactCompanyName,
                    ContactPersonName = ship.ContactPersonName,
                    ShippingCompany = ship.ShippingCompany,
                    ShippingCompanyId = ship.ShippingCompany != null ? new Nullable<int>(ship.ShippingCompany.Id) : null,
                    ShippingCompanyCode = ship.ShippingCompany != null ? new Nullable<int>(ship.ShippingCompany.Code) : null,
                    ShipTypeCode = ship.ShipType != null ? ship.ShipType.Code : null,
                    ShipType = ship.ShipType,
                    ShipTypeId = ship.ShipTypeId.HasValue ? ship.ShipTypeId.Value : 1,
                    Tonnage = ship.Tonnage,
                    HK = ship.HK,
                    Status = ship.Status
                };
            }
            return Edit("CreateOrEdit", viewModel, ship);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShipCreateOrEditViewModel o)
        {
            return UpdateUsingViewModel(o, (parent) =>
            {
                //Convert the ViewModel to DB Object (Model)
                var ship = Find(o.Id);
                //var entity = ship.Entity;
                ship.Code = o.ShipCode;
                ship.Name = o.Name;
                ship.ContactCompanyName = o.ContactCompanyName;
                ship.ContactPersonName = o.ContactPersonName;
                ship.ShippingCompanyId = o.ShippingCompanyId;
                ship.ShipTypeId = o.ShipTypeId;
                ship.Tonnage = o.Tonnage;
                ship.HK = o.HK;
                ship.Status = !string.IsNullOrEmpty(o.Status) ? o.Status : string.Empty;
                return ship;
            });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var obj = db.Set<Ship>().Find(id);
            return Delete("Delete", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipViewModel dbObj)
        {
            return DeleteObject<Ship, Ship>(dbObj.Id);
        }

        public ActionResult PrintLabel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LabelReport()//bool isPartialView = false)
        {
            //PostalFrom=' + PostalFrom + '&PostalTo=' + PostalTo + '&MemberTypeCode=' + MemberTypeCode + '&FromBirthday=' + FromBirthday
            //if (isPartialView)
            //    return PartialView();
            //return View();
            TempData["CanOpenReport"] = labelReportStatus.CanOpenReport;
            return View();
        }

        static LabelReportStatus labelReportStatus = new LabelReportStatus()
        {
            ItemsDone = 0,
            ItemsTotal = 0,
            TimeSpent = 0,
            CanOpenReport = true
        };

        [HttpGet]
        public ActionResult GetTimeElapsedForLabelReportPrintJob()
        {
            return Json(labelReportStatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PrintLabelReport(ShipLabelReportParameters obj, bool isPartialView = false, string type = "json")
        {
            db.Configuration.AutoDetectChangesEnabled = false;
            var result = new LabelReportStatusCode()
            {
                StatusCode = LabelReportStatusCode.Code.OK,
                Message = BootstrapResources.Resources.Success
            };
            labelReportStatus = new LabelReportStatus()
            {
                ItemsDone = 0,
                ItemsTotal = 0,
                TimeSpent = 0,
                CanOpenReport = true
            };
            if (obj != null)
            {
                /*SELECT
	ship.Code as ShipCode, 
	ship.Name as ShipName,
	ship.ContactCompanyName,
	sc.Name as ShippingCompanyName,
	dbo.Postal.CountryCode as CountryCode, 
	dbo.Postal.Code as PostalCode, 
	dbo.Postal.CityName, 
	dbo.Address.AddressLine1
FROM            
	dbo.Ship as ship INNER JOIN
	dbo.ShipType ON dbo.ShipType.Id = ship.ShipTypeId INNER JOIN
	dbo.ShippingCompany as sc ON sc.Id = ship.ShippingCompanyId INNER JOIN
	dbo.Address ON ship.EntityId = dbo.Address.EntityId LEFT OUTER JOIN
	dbo.Postal ON dbo.Address.PostalId = dbo.Postal.Id
WHERE 
	(ship.Id >= @shipIdFrom) AND (ship.Id <= @shipIdTo) AND (dbo.[Address].IsPrimary = 'TRUE') */

                var ships = db.Ships.FilterDeleted().AsQueryable();
                labelReportStatus.ItemsTotal = 0;
                ships = ships.Where(x => FilterShipForLabels(x, obj)).OrderBy(x => x.Code);

                if (ModelState.IsValid)
                {
                    var i = 0;
                    var eti_id = GetUserIdCode();
                    var eti_dato = DateTime.UtcNow;
                    var s = DateTime.UtcNow;
                    //memberLabelReportStatus.ItemsTotal = members CountMembers(members).Result; // members.Count(); list.Count;// 
                    //try
                    //{
                    //    if (db == null)
                    //    {
                    //        db = new BootstrapContext();
                    //    }
                    //    foreach (var ship in ships)
                    //    {
                    //        ship.ETI_ID = eti_id;
                    //        ship.ETI_DATO = eti_dato;
                    //        db = ContextManager.ResetContext(db, i, 100, true);
                    //        i++;
                    //        //if (i % 10 == 0)
                    //        {
                    //            System.Diagnostics.Debug.WriteLine(i);
                    //            labelReportStatus.ItemsDone = i;
                    //            labelReportStatus.TimeSpent = DateTime.UtcNow.Subtract(s).Seconds;
                    //        }
                    //    }
                    //    db.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    result.StatusCode = LabelReportStatusCode.Code.FAILED;
                    //    result.Message = ex.Message;
                    //}
                    //finally
                    //{
                    //    labelReportStatus.CanOpenReport = false;
                    //    if (db != null)
                    //        db.Dispose();
                    //}


                    foreach (var ship in ships)
                    {
                        ship.ETI_ID = eti_id;
                        ship.ETI_DATO = eti_dato;
                        //// attach the entity
                        db.Set<Ship>().Attach(ship);
                        // change its state to modified so entity framework can update the existing product instead of creating a new one
                        db.Entry(ship).State = System.Data.Entity.EntityState.Modified;
                        i++;
                        //if (i % 10 == 0)
                        {
                            //system.diagnostics.debug.writeline(i);
                            labelReportStatus.ItemsDone = i;
                            labelReportStatus.TimeSpent = DateTime.UtcNow.Subtract(s).Seconds;
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
            }
            db.Configuration.AutoDetectChangesEnabled = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        static bool FilterShipForLabels(Ship x, ShipLabelReportParameters obj)
        {
            return
                string.Compare(x.Code, obj.ShipCodeFrom) >= 0 &&
                string.Compare(x.Code, obj.ShipCodeTo) <= 0 &&
                //x.Entity != null &&
                //x.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault() != null &&
                (!("" + x.Status).ToLower().Equals("iv"));
        }

        [HttpGet]
        public ActionResult GetShipLabelReportParameters()
        {
            var ships = db.Ships.FilterDeleted().Where(x => !string.IsNullOrEmpty(x.Code.Trim())).OrderBy(x => x.Code).ToList();
            var firstShip = ships.FirstOrDefault();
            var lastShip = ships.LastOrDefault();
            return Json(new
            {
                FirstShipId = firstShip.Id,
                FirstShipCode = firstShip.Code,
                LastShipId = lastShip.Id,
                LastShipCode = lastShip.Code
            }, JsonRequestBehavior.AllowGet);
        }

    }
}