using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using BootstrapWebApplication.Hubs;

namespace BootstrapWebApplication.Controllers
{
    public class ETransController : Controller<Member>
    {
        private class ETransData
        {
            public ETransSaveViewModel ETransSaveVM { get; set; }

            public Member Member { get; set; }

            public BankAccount BankAccount { get; set; }

            public BankAccount BankAccountFF { get; set; }

            public HolidayPay_HOVD LastHolidayPay { get; set; }
        }

        ETransData GetETransData(ETransSaveViewModel obj, Member member)
        {
            var bankAccount = db.BankAccounts.Where(x => x.AccountNumber == obj.ItemFromETrans.BankAccount && x.Bank != null && x.Bank.RegNumber == obj.ItemFromETrans.BankRegNumber).FirstOrDefault();
            var bankAccountFF = db.BankAccounts.Where(x => x.AccountNumber == obj.ItemFromETrans.BankAccountFF && x.Bank != null && x.Bank.RegNumber == obj.ItemFromETrans.BankRegNumberFF).FirstOrDefault();
            var lastHolidayPay = db.HolidayPay_HOVDs.Where(x => x.FRT_NR != null).OrderByDescending(x => x.Id).FirstOrDefault();
            return new ETransData()
            {
                ETransSaveVM = obj,
                Member = member,
                BankAccount = bankAccount,
                BankAccountFF = bankAccountFF,
                LastHolidayPay = lastHolidayPay
            };
        }

        public override ActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public ActionResult ChooseAlternative(ETrans item)
        {
            item.ChangeEvent = CreateChangeEvent("di"); // di = disk from elektron
            var alternatives = GetAlternativesViewModel(item);
            var postalCodes = db.Postals.Where(x=>x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue).ToList();
            //TempData["postals"] = postals;
            ViewBag.NewPostalId = new SelectList(postalCodes.Select(m => new SelectListItem
            {
                Text = m.CityName,
                Value = m.Id.ToString()
            }), "Value", "Text", (item != null && item.PostalCode != null) ? new Nullable<int>(db.Postals.Where(x => x.Code == item.PostalCode).FirstOrDefault() != null ? db.Postals.Where(x => x.Code == item.PostalCode).FirstOrDefault().Id : 100) : null);
            var vm = new ETransViewModel()
            {
                CurrentItem = item,
                Alternatives = alternatives,
                PostalCodes = postalCodes
            };
            return PartialView("ChooseAlternative", vm);
        }

        public ActionResult FileUploadResult()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            return Json(SaveETransFile(files));
        }

        //[HttpPost]
        public ActionResult SaveItem(ETransSaveViewModel obj)
        {
            var result = SaveItem2(obj);
            try
            {
                db.SaveChanges();
            }
            catch { }
            switch (result)
            {
                case HOLIDAY_PAY_ADD_STATUS.NEW:
                    return Json(new HttpStatusCodeResult(HttpStatusCode.OK));
                case HOLIDAY_PAY_ADD_STATUS.SKIPPED:
                    return Json(new HttpStatusCodeResult(HttpStatusCode.OK));
                case HOLIDAY_PAY_ADD_STATUS.BAD_REQUEST:
                    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                case HOLIDAY_PAY_ADD_STATUS.ERROR:
                    return Json(null);
                default:
                    return Json(null);
            }

            //if (obj == null || obj.ItemFromETrans == null)
            //{
            //    return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
            //}
            //Member member = null;
            //var memberFromDb = obj.ItemFromDb == null ? null : db.Members.Where(x => x.Id == obj.ItemFromDb.Id).FirstOrDefault();
            //if (memberFromDb == null)
            //{
            //    member = CreateMember(obj);
            //}
            //else
            //{
            //    member = UpdateMember(obj, memberFromDb);
            //}
            //if (member != null)
            //{
            //    var pars = GetETransData(obj, member);

            //    // check if HolidayPay has been added

            //    var isHolidayPayAddedBefore = IsHolidayPayAddedBefore(member, obj.ItemFromETrans);

            //    CreateHolidaypay(pars);
            //    return Json(new HttpStatusCodeResult(HttpStatusCode.OK));
            //}
            //return Json(null);
        }

        private HOLIDAY_PAY_ADD_STATUS SaveItem2(ETransSaveViewModel obj, string userIdCode = null)
        {
            if (obj == null || obj.ItemFromETrans == null)
            {
                return HOLIDAY_PAY_ADD_STATUS.BAD_REQUEST;
            }
            Member member = null;
            var memberFromDb = obj.ItemFromDb != null ? db.Members.Where(x => x.Id == obj.ItemFromDb.Id).FirstOrDefault() : null;
            if (memberFromDb == null)
            {
                member = CreateMember(obj);
            }
            else
            {
                // check if HolidayPay has been added before to an existing member from db
                var isHolidayPayAddedBefore = IsHolidayPayAddedBefore(memberFromDb, obj.ItemFromETrans);
                if (isHolidayPayAddedBefore)
                {
                    return HOLIDAY_PAY_ADD_STATUS.SKIPPED;
                }
                member = UpdateMember(obj, memberFromDb, userIdCode);
            }
            if (member != null)
            {
                var pars = GetETransData(obj, member);
                CreateHolidaypay(pars);
                return HOLIDAY_PAY_ADD_STATUS.NEW;

            }
            return HOLIDAY_PAY_ADD_STATUS.ERROR;
        }

        enum HOLIDAY_PAY_ADD_STATUS
        {
            NEW,
            SKIPPED,
            BAD_REQUEST,
            ERROR
        }

        //public ActionResult UpdateItem(ETransSaveViewModel obj)
        //{
        //    if (obj == null || obj.ItemFromETrans == null)
        //    {
        //        return Json(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
        //    }
        //    Member member = null;
        //    var memberFromDb = obj.ItemFromDb == null ? null : db.Members.Where(x => x.Id == obj.ItemFromDb.Id).FirstOrDefault();
        //    if (memberFromDb == null)
        //    {
        //        member = CreateMember(obj);
        //    }
        //    else
        //    {
        //        member = UpdateMember(obj, memberFromDb);
        //    }
        //    if (member != null)
        //    {
        //        var pars = GetETransData(obj, member);
        //        UpdateHolidaypay(pars);
        //        return Json(new HttpStatusCodeResult(HttpStatusCode.OK));
        //    }
        //    return Json(null);
        //}

        private Member UpdateMember(ETransSaveViewModel obj, Member member, string userIdCode = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Update member if information has changed. save old information 
                    // update fields, which have changed - if any
                    var gender = db.PersonGenders.FilterDeleted().Where(x => x.Code.ToLower().Equals(obj.ItemFromETrans.Gender.ToLower())).FirstOrDefault();
                    var firstName = obj.ItemFromETrans.FirstName;
                    var lastName = obj.ItemFromETrans.LastName;
                    member.Person.FirstName = firstName;
                    member.Person.LastName = lastName;
                    member.Person.FullName = String.IsNullOrEmpty(("" + member.Person.MiddleName).Trim()) ? (firstName + " " + lastName) : (firstName + " " + member.Person.MiddleName + " " + lastName);

                    //member.Person.Birthday = obj.ItemFromETrans.Birthday;
                    member.Person.Gender = gender != null ? gender : member.Person.Gender;
                    member.GenderCode = gender != null ? gender.Code : member.Person.Gender.Code;
                    var address = member.Addresses.FilterDeleted().Where(x => x.IsPrimary).FirstOrDefault();
                    if (address != null)
                    {
                        address.AddressLine1 = obj.ItemFromETrans.AddressLine1;
                        if (address.Postal != null && !string.IsNullOrEmpty(("" + obj.ItemFromETrans.PostalCode).Trim()))
                        {
                            //address.Postal.Code = obj.ItemFromETrans.PostalCode;
                            var newPostal = db.Postals.Where(x => x.Code == obj.ItemFromETrans.PostalCode).FirstOrDefault();
                            if (newPostal != null)
                            {
                                address.PostalId = newPostal.Id;
                                //address.Postal.Code = newPostal.Code;
                                //address.Postal.CityName = newPostal.CityName;
                                //db.Addresses.Attach(address);
                            }
                            else
                            {
                                address.PostalId = null;  // Hans, nullstilla postnr um tað ikki er funnið
                            }
                        }

                        //member.AddressLine = address.AddressLine1 + " " + address.AddressLine2;
                        //member.CityName = address.Postal != null ? address.Postal.CityName : member.CityName;
                        //member.PostalCode = address.Postal != null ? address.Postal.Code : member.PostalCode;
                        member.AddressLine = obj.ItemFromETrans.AddressLine1;  //Hans, tak adressuna frá etrans
                        member.CityName = address.Postal != null ? address.Postal.CityName : "";
                        member.PostalCode = obj.ItemFromETrans.PostalCode;

                    }
                    member.ChangeEvent = UpdateChangeEvent(member.ChangeEvent, userIdCode);
                    //// Update the changeEvent object
                    //var hasChangeEvent = UpdateChangeEvent(member, member);
                    //// or if no ChangeEvent is assigned to the object for some reason, create one
                    //if (!hasChangeEvent)
                    //{
                    //    AddChangeEvent(member, member);
                    //}
                    // Attach the entity
                    //db.Set<Member>().Attach(member);
                    // Change its state to Modified so Entity Framework can update the existing product instead of creating a new one
                    db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    // Save changes to the DB
                    //db.SaveChanges();

                    //CreateMemberPayment(pars); // DONT USE THIS

                    return member;
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        private Member CreateMember(ETransSaveViewModel obj, string userIdCode = null)
        {
            var personTitleCode = obj.ItemFromETrans.Gender.ToLower().Equals("m") ? "mr" : "mrs";
            var vm = new MemberCreateViewModel()
            {
                FirstName = obj.ItemFromETrans.FirstName,
                MiddleName = "",
                LastName = obj.ItemFromETrans.LastName,
                Birthday = obj.ItemFromETrans.Birthday,
                PhoneNumber = "",
                AddressLine1 = obj.ItemFromETrans.AddressLine1,
                AddressLine2 = "",
                Postal = db.Postals.Where(x => x.Code == obj.ItemFromETrans.PostalCode).FirstOrDefault(),
                Title = db.PersonTitles.Where(x => x.Code.ToLower().Equals(personTitleCode)).FirstOrDefault(),
                SSN = "",
                Gender = db.PersonGenders.Where(x => x.Code.ToLower().Equals(obj.ItemFromETrans.Gender.ToLower())).FirstOrDefault(),
                Country = db.Countries.Where(x => x.Code.ToLower().Equals("fo")).FirstOrDefault(),
                MemberType = db.MemberTypes.Where(x => x.Code.ToLower().Equals("ff")).FirstOrDefault(),
                Job = db.Jobs.Where(x => String.IsNullOrEmpty(x.Code)).FirstOrDefault(),
                BankId = null,
                AccountNumber = null
            };
            Member member = null;
            var result = CreateUsingViewModel(vm, (parent) =>
            {
                member = new MemberHandler().CreateFromViewModel(vm, db, new ChangeEvent[] { CreateChangeEvent(userIdCode), CreateChangeEvent(userIdCode), CreateChangeEvent(userIdCode) });
                return member;
            });
            //var pars = GetETransData(obj, member);
            //CreateHolidaypay(pars);
            //CreateMemberPayment(pars); // DONT USE THIS
            //return true;
            return member;
        }

        //The INSERT statement conflicted with the FOREIGN KEY constraint "FK_dbo.Address_dbo.Country_CountryId". The conflict occurred in database "fmfwebapp", table "dbo.Country", column 'Id'.The statement has been terminated.
        private void CreateHolidaypay(ETransData data)
        {
            var obj = data.ETransSaveVM;
            var member = data.Member;
            var lastHolidayPay = data.LastHolidayPay;

            var employer = db.ShippingCompanies.FilterDeleted().Where(x => x.EmployerNumber.Value == obj.ItemFromETrans.EmployerNumber).FirstOrDefault();
            if (employer == null)
            {
                var newEmployer = new ShippingCompanyHandler().Create(db, obj.ItemFromETrans.EmployerNumber, "tekstur manglar", "tekstur manglar", "tekstur manglar", CreateChangeEvent("di"));
                db.Set<ShippingCompany>().Add(newEmployer);
            }

            // todo: set TR_NR and X
            var holidayPay = new HolidayPay_HOVD()
            {
                EmployerNumber = obj.ItemFromETrans.EmployerNumber,
                TransferDate = obj.ItemFromETrans.TransferDate,
                Amount = obj.ItemFromETrans.HolidayPayAmount,
                ART = obj.ItemFromETrans.ART,
                KOYR_DATO = obj.ItemFromETrans.ETransDate,
                FRT_NR = lastHolidayPay != null ? lastHolidayPay.FRT_NR + 1 : 1000,
                REG_NR = obj.ItemFromETrans.BankRegNumber,
                KONTO = obj.ItemFromETrans.BankAccount,
                REG_NR_FF = obj.ItemFromETrans.BankRegNumberFF,
                KONTO_FF = obj.ItemFromETrans.BankAccountFF,
                PLUS = obj.ItemFromETrans.IsReverse ? "-" : "",
                TR_NR = null,
                X = null,
                MemberId = member.Id,
                BankAccount = data.BankAccount,
                BankAccountFF = data.BankAccountFF,
                ChangeEvent = CreateChangeEvent("di") // di = disk from electron
            };
            // Attach the entity
            db.Set<HolidayPay_HOVD>().Add(holidayPay);
            // Save changes to the DB
            //db.SaveChanges();
        }

        //private void CreateMemberPayment(ETransData data)
        //{
        //    var obj = data.ETransSaveVM;
        //    var member = data.Member;
        //    var lastHolidayPay = data.LastHolidayPay;

        //    var etrans = obj.ItemFromETrans;

        //    var paidOn = etrans.TransferDate.HasValue ? etrans.TransferDate.Value : DateTime.Now;

        //    var vmObj = new MemberPaymentCreateViewModel()
        //    {
        //         MemberId = member.Id,
        //         Year = paidOn.Year.ToString(),
        //         PaidOn = paidOn,
        //         PaidById = "di",
        //         BankId = null,
        //         RegNumber = etrans.BankRegNumber, // or etrans.BankRegNumberFF ???
        //         BankAccountNumber = etrans.BankAccount, // or etrans.BankAccountFF ???
        //         HolidayPay = etrans.HolidayPayAmount,
        //         MembershipPayment = etrans.HolidayPayAmount,

        //    };

        //    //// todo: what is code???
        //    //var vmObj = obj.ItemFromETrans;
        //    //var paidOn = vmObj.TransferDate.HasValue ? vmObj.TransferDate.Value : DateTime.Now;
        //    //var memberPayment = new MemberPayment()
        //    //{
        //    //    Year = paidOn.Year.ToString(),
        //    //    PaidOn = paidOn,
        //    //    HolidayPay = vmObj.HolidayPayAmount,
        //    //    MembershipPayment = vmObj.HolidayPayAmount,
        //    //    Code = null,
        //    //    Price = vmObj.,
        //    //    MemberId = vmObj.MemberId
        //    //};

        //    //var bankAccount = db.BankAccounts.Where(x => x.Bank.Id == vmObj.BankId && x.AccountNumber == vmObj.BankAccountNumber).FirstOrDefault();
        //    //if (bankAccount == null)
        //    //{
        //    //    bankAccount = new BankAccount()
        //    //    {
        //    //        AccountNumber = vmObj.BankAccountNumber,
        //    //        BankId = vmObj.BankId,
        //    //        Entity = parent.Person.Entity,
        //    //        IsPrimary = true
        //    //    };
        //    //}
        //    //memberPayment.BankAccount = bankAccount;

        //    //// Create a changeEvent object
        //    //memberPayment.ChangeEvent = CreateChangeEvent();


        //    // todo: set TR_NR and X
        //    //var memberPayment = new MemberPayment()
        //    //{
        //    //    ChangeEvent = changeEvent,
        //    //    MemberPaymentNR = o.LG_NR,
        //    //    //todo: remove MemberNR? (low priority)
        //    //    MemberNR = o.NR,
        //    //    Year = o.AR,
        //    //    Price = o.PRIS,
        //    //    PaidOn = Helper.ParseDate(o.ING_DATO, o.ING_HH, o.ING_MM),
        //    //    PaidById = o.ING_ID,
        //    //    BankAccount = bankAccount,
        //    //    HolidayPay = o.FRT_LON,
        //    //    MembershipPayment = o.LGJ,
        //    //    Code = o.KOTA,
        //    //    TRANSP_IALT = o.TRANSP_IALT,
        //    //    K1 = o.K1,
        //    //    Member = member
        //    //};
        //    // Attach the entity
        //    //db.Set<MemberPayment>().Add(memberPayment);
        //    //// Save changes to the DB
        //    //db.SaveChanges();
        //}


        //public ActionResult Remove(string[] fileNames)
        //{
        //    // The parameter of the Remove action must be called "fileNames"

        //    if (fileNames != null)
        //    {
        //        foreach (var fullName in fileNames)
        //        {
        //            var fileName = Path.GetFileName(fullName);
        //            var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

        //            // TODO: Verify user permissions

        //            if (System.IO.File.Exists(physicalPath))
        //            {
        //                // The files are not actually removed in this demo
        //                // System.IO.File.Delete(physicalPath);
        //            }
        //        }
        //    }

        //    // Return an empty string to signify success
        //    return Content("");
        //}

        //public ActionResult Submit(IEnumerable<HttpPostedFileBase> files)
        //{
        //    SaveETransFile(files);
        //    return RedirectToAction("FileUploadResult");
        //}

        private ETransUploadResultViewModel SaveETransFile(IEnumerable<HttpPostedFileBase> files)
        {
            var uploadResult = new ETransUploadResultViewModel()
            {
                ItemsToValidate = new List<ETrans>(),
                ItemsSavedToDb = new List<ETrans>(),
                ItemsAlreadyAddedToDb = new List<ETrans>(),
                ItemCount = 0,
                StatusCode = 0,
                Message = "OK"
            };
            if (files != null)
            {
                var file = files.FirstOrDefault();
                if (file != null)
                {

                    var path = GetPath(file);
                    try
                    {
                        file.SaveAs(path);
                        //WriteFileFromStream(file.InputStream, path);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    //file.SaveAs(path);
                    try
                    {
                        var tempETransList = ETransHandler.Parse(path);
                        if (tempETransList != null && tempETransList.Count > 0)
                        {
                            uploadResult.ItemCount = tempETransList.Count();
                            var etransKOYR_DATO = tempETransList.ToList()[0].ETransDate;
                            var holidayPays = db.HolidayPay_HOVDs.Where(x => x.KOYR_DATO.HasValue && etransKOYR_DATO.HasValue && DateTime.Compare(x.KOYR_DATO.Value, etransKOYR_DATO.Value) == 0);
                            // File has been converted before
                            //if (holidayPays.Count() > 0)
                            //{
                            //    //uploadResult.ItemsToValidate = null;
                            //    //uploadResult.ItemsSavedToDb = null;
                            //    //uploadResult.StatusCode = 1;
                            //    //uploadResult.Message = "File " + file.FileName + " has been converted before.";

                            //    // Continue with the operation but instead of adding HolidayPay then update it.
                            //    // If the HolidayPay hasn't been added, then add it. The upload might have been aborted half way last time.
                            //}
                            //else
                            //{
                            // File has not been converted before
                            // Add HolidayPay and update member info
                            var count = 0;
                            string userIdCode = "di";
                            db = db == null ? db = new BootstrapContext() : db;
                            try
                            {
                                var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
                                foreach (var etrans in tempETransList)
                                {
                                    count++;
                                    var progress = Math.Round(((count * 100.0) / (double)tempETransList.Count), 2);
                                    hub.Clients.All.broadcastProgress(progress);
                                    var membersFoundInDb = FindMemberByBirthday(etrans);
                                    // Member exists already in db
                                    if (membersFoundInDb != null)
                                    {
                                        // There's more than 1 member with the same birthday. 
                                        if (membersFoundInDb.Count() > 1)
                                        {
                                            var memberFoundInDb = membersFoundInDb.Where(x => x.Birthday.HasValue && etrans.Birthday.HasValue && DateTime.Compare(x.Birthday.Value, etrans.Birthday.Value) == 0 && x.Person != null && ("" + x.Person.FirstName).ToLower().Equals(("" + etrans.FirstName).ToLower()) && ("" + x.Person.LastName).ToLower().Equals(("" + etrans.LastName).ToLower())).FirstOrDefault();
                                            if (memberFoundInDb != null)
                                            {
                                                var result = SaveItem2(new ETransSaveViewModel()
                                                {
                                                    ItemFromDb = memberFoundInDb != null ? new ETrans() { Id = memberFoundInDb.Id } : null,
                                                    ItemFromETrans = etrans
                                                }, userIdCode);
                                                if (result == HOLIDAY_PAY_ADD_STATUS.NEW)
                                                {
                                                    uploadResult.ItemsSavedToDb.Add(etrans);
                                                }
                                                else if (result == HOLIDAY_PAY_ADD_STATUS.SKIPPED)
                                                {
                                                    uploadResult.ItemsAlreadyAddedToDb.Add(etrans);
                                                }
                                            }
                                            else
                                            {
                                                var canAdd = true;
                                                // Check each member. If holiday has not been added before (canAdd = true), then add the member to be validated (list).
                                                foreach (var member in membersFoundInDb)
                                                {
                                                    var isHolidayPayAddedBefore = IsHolidayPayAddedBefore(member, etrans);
                                                    if (isHolidayPayAddedBefore)
                                                    {
                                                        uploadResult.ItemsAlreadyAddedToDb.Add(etrans);
                                                        canAdd = false;
                                                        break;
                                                    }
                                                }
                                                if (canAdd)
                                                {
                                                    uploadResult.ItemsToValidate.Add(etrans);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Only 1 member found with this birthday. 
                                            var memberFoundInDb = membersFoundInDb.Count() == 1 ? membersFoundInDb.ToList()[0] : null;
                                            // If the member found has the same first name as in etrans file, 
                                            // then assume it's the right member and add holidaypay (etrans)
                                            var isMemberInDbSameAsInETrans = membersFoundInDb != null && membersFoundInDb.Where(x => x.Birthday.HasValue && etrans.Birthday.HasValue && DateTime.Compare(x.Birthday.Value, etrans.Birthday.Value) == 0 && x.Person != null && ("" + x.Person.FirstName).ToLower().Equals(("" + etrans.FirstName).ToLower()) && ("" + x.Person.LastName).ToLower().Equals(("" + etrans.LastName).ToLower())).FirstOrDefault() != null;

                                            if (isMemberInDbSameAsInETrans)
                                            {
                                                // SaveItem2:
                                                //  - Creates a member if member is null
                                                //  - Else if not null, then adds holidaypay, if it's not added before to the member
                                                var result = SaveItem2(new ETransSaveViewModel()
                                                {
                                                    ItemFromDb = memberFoundInDb != null ? new ETrans() { Id = memberFoundInDb.Id } : null, //membersFoundInDb.Count() == 1 ? new ETrans() { Id = membersFoundInDb.ToList()[0].Id } : null,
                                                    ItemFromETrans = etrans
                                                }, userIdCode);
                                                if (result == HOLIDAY_PAY_ADD_STATUS.NEW)
                                                {
                                                    uploadResult.ItemsSavedToDb.Add(etrans);
                                                }
                                                else if (result == HOLIDAY_PAY_ADD_STATUS.SKIPPED)
                                                {
                                                    uploadResult.ItemsAlreadyAddedToDb.Add(etrans);
                                                }
                                            }
                                            else
                                            {
                                                uploadResult.ItemsToValidate.Add(etrans);
                                            }

                                            //var isHolidayPayAddedBefore = IsHolidayPayAddedBefore(memberFoundInDb, etrans);

                                            //if (!isHolidayPayAddedBefore)
                                            //{
                                            //    var result = SaveItem(new ETransSaveViewModel()
                                            //    {
                                            //        ItemFromDb = memberFoundInDb != null ? new ETrans() { Id = memberFoundInDb.Id } : null, //membersFoundInDb.Count() == 1 ? new ETrans() { Id = membersFoundInDb.ToList()[0].Id } : null,
                                            //        ItemFromETrans = etrans
                                            //    });
                                            //    uploadResult.ItemsSavedToDb.Add(etrans);
                                            //}
                                            //else
                                            //{
                                            //    // do nothing...
                                            //    uploadResult.ItemsAlreadyAddedToDb.Add(etrans);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // create the member?
                                    }

                                    //db.Configuration.AutoDetectChangesEnabled = false;
                                    db = ResetContext(db, count, 500, true);
                                    Debug.WriteLine(count + " / " + tempETransList.Count);
                                }
                                db.SaveChanges();
                            }
                            finally
                            {
                                if (db != null)
                                    db.Dispose();
                            }
                        }
                        //TempData["ETransList"] = uploadResult.ItemsToValidate;
                    }
                    catch (Exception ex)
                    {
                        uploadResult.ItemsToValidate = null;
                        uploadResult.ItemsSavedToDb = null;
                        uploadResult.StatusCode = 2;
                        uploadResult.Message = ex.Message;
                    }
                }
            }
            return uploadResult;
        }

        private bool IsHolidayPayAddedBefore(Member member, ETrans etrans)
        {
            if (member != null)
            {
                // check if the item has been added before
                var holidayPay = member.HolidayPay_HOVD.Where(x =>
                {
                    return x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue && x.ART == etrans.ART && x.TransferDate == etrans.TransferDate && x.Amount == etrans.HolidayPayAmount && x.EmployerNumber == etrans.EmployerNumber && x.REG_NR == etrans.BankRegNumber && x.KONTO == etrans.BankAccount;
                }).FirstOrDefault();
                return holidayPay != null;
            }
            return false;
        }

        private bool CompareBirthday(DateTime? dt1, DateTime? dt2)
        {
            return dt1.HasValue && dt2.HasValue && DateTime.Compare(dt1.Value, dt2.Value) == 0;
        }

        private IEnumerable<Member> FindMemberByBirthday(ETrans item)
        {
            if (item == null)
            {
                return null;
            }
            if (item.FirstName == null || item.LastName == null || !item.Birthday.HasValue)// || !item.PostalCode.HasValue)
            {
                return null;
            }

            // Find a person by his birthdate
            var results = db.Members.Where(x => x.Person != null && !x.ChangeEvent.DeletedOn.HasValue && x.Person.Birthday.HasValue && DateTime.Compare(x.Person.Birthday.Value, item.Birthday.Value) == 0); //CompareBirthday(x.Person.Birthday, item.Birthday));// 
            // If more than 1 result, then try filter by lastname
            //if (results.Count() > 0)
            //{
            //    results = db.Members.Where(x => x.Person != null && DateTime.Compare(x.Person.Birthday.Value, item.Birthday.Value) == 0 && ("" + x.Person.LastName).ToLower().Contains(("" + item.LastName).ToLower()));
            //}
            //// Find a person by his first and last name
            //var results = db.Members.Where(x => x.Person != null && (x.Person.FirstName.ToLower().Contains(item.FirstName.ToLower()) || x.Person.LastName.ToLower().Contains(item.LastName.ToLower())));
            //// If no results, then try find the member by his birthday
            //if (results.Count() == 0)
            //{
            //    results = db.Members.Where(x => x.Person != null && DateTime.Compare(x.Person.Birthday.Value, item.Birthday.Value) == 0);
            //}
            //else if (results.Count() >= 1)
            //{
            //    var result = new List<Member>();
            //    // otherwise find by address
            //    var members = results.ToList();
            //    foreach (var member in members)
            //    {
            //        var addresses = member.Person.Entity.Addresses.Where(x => x.IsPrimary);
            //        foreach (var address in addresses)
            //        {
            //            if (address != null && address.Postal != null && address.Postal.Code == item.PostalCode)
            //            {
            //                result.Add(member);
            //                //results = results.Where(x => x.Person.Entity.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Id == address.Id);
            //            }
            //        }
            //    }
            //    return result;
            //}
            return results;
        }


        private IEnumerable<ETrans> GetAlternativesViewModel(ETrans item)
        //private IEnumerable<ETransMemberViewModel> GetAlternativesViewModel(ETransLookupViewModel item)
        {
            if (item == null) return null;
            if (item.FirstName == null
                || item.LastName == null
                //|| item.PostalCode == null
                //|| item.Birthday == null
                ) return null;

            //var list = FindMemberByBirthday(item).ToList();
            var list = db.Members.Where(x => x.Person != null && x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue && x.Person.Birthday.HasValue && (DateTime.Compare(x.Person.Birthday.Value, item.Birthday.Value) == 0 || x.Person.FirstName.ToLower().Equals(item.FirstName.ToLower()) || x.Person.LastName.ToLower().Equals(item.LastName.ToLower())));

            return list.OrderBy(x=>x.Person.FullName).Select(x => new ETrans()
            //return Lookup(item.FirstName, item.LastName, item.Birthday).Select(x => new ETransMemberViewModel()
            //return Json(Lookup(firstName, lastName, birthday).Select(x => new ETransMemberViewModel()
            //return Json(Lookup(item).Select(x => new ETransMemberViewModel()
            {
                Id = x.Id,
                FirstName = x.Person != null ? x.Person.FirstName : "",
                //MiddleName = x.Person != null ? x.Person.LastName : "",
                LastName = x.Person != null ? x.Person.LastName : "",
                FullName = x.Person != null ? (String.IsNullOrEmpty(x.Person.MiddleName) ? (x.Person.FirstName + " " + x.Person.LastName) : (x.Person.FirstName + " " + x.Person.MiddleName + " " + x.Person.LastName)) : "",
                Birthday = x.Person != null ? x.Person.Birthday : null,
                //MemberType = (x.MemberType != null) ? x.MemberType.Description : "",
                PostalCode = x.PostalCode.HasValue ? x.PostalCode.Value : 0,// (x.Addresses.Where(y => y.IsPrimary).FirstOrDefault() != null && x.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal != null) ? x.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal.Code : -1,
                CityName = x.CityName,// (x.Addresses.Where(y => y.IsPrimary).FirstOrDefault() != null && x.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal != null) ? x.Addresses.Where(y => y.IsPrimary).FirstOrDefault().Postal.CityName : "",
                AddressLine1 = x.AddressLine,// x.Addresses.Where(y => y.IsPrimary).FirstOrDefault() != null ? x.Addresses.Where(y => y.IsPrimary).FirstOrDefault().AddressLine1 : null,
                Gender = x.GenderCode,// (x.Person != null && x.Person.Gender != null) ? x.Person.Gender.Code : "-",
                SSN = x.Person != null ? x.Person.SSN : "-",
                ChangeEvent = x.ChangeEvent
            });
        }

        //[ResponseType(typeof(MemberCreateEditGetRelatedDataViewModel))]
        [HttpGet]
        public ActionResult GetPostals()
        {
            return Json(db.Postals.FilterDeleted().ToList(), JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetAlternatives(ETrans item)
        ////public ActionResult GetAlternatives(ETransLookupViewModel item)// string firstName, string lastName, DateTime? birthday)
        //{
        //    return Json(GetAlternativesViewModel(item), JsonRequestBehavior.AllowGet);
        //}

        //private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        //{
        //    return
        //        from a in files
        //        where a != null
        //        select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        //}

        //public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        //{
        //    foreach (var file in files)
        //    {
        //        file.SaveAs(GetPath(file));
        //    }
        //    // Return an empty string to signify success
        //    return Content("");
        //}

        static void WriteFileFromStream(Stream stream, string toFile)
        {
            using (FileStream fileToSave = new FileStream(toFile, FileMode.Create))
            {
                stream.CopyTo(fileToSave);
            }
        }

        private string GetPath(HttpPostedFileBase file)
        {
            return GetPath(file.FileName);
        }

        private string GetPath(string path)
        {
            // Some browsers send file names with full path. We only care about the file name.
            return Path.Combine(Server.MapPath("~/App_Data/E_Trans"), Path.GetFileName(path));
        }

        protected override IQueryable<Member> PagedListFilter(IQueryable<Member> list, string filterName = null)
        {
            return null;
            //throw new NotImplementedException();
        }

        protected override void AddViewBag(Member obj)
        {
            //throw new NotImplementedException();
        }

        protected override Kendo.Mvc.UI.DataSourceResult Get(int id, Kendo.Mvc.UI.DataSourceRequest request)
        {
            return null;
            //throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult PrintReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Report()
        {
            return View();
        }
    }
}