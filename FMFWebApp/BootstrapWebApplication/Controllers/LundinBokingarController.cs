using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.DAL;
using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Model;
using System.Globalization;
using Microsoft.AspNet.SignalR;
using BootstrapWebApplication.Hubs;
using System.IO;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using BootstrapWebApplication.ViewModels;

namespace BootstrapWebApplication.Controllers
{

    public class LundinBookingsHubHelper
    {
        private IHubContext hub;

        public LundinBookingsHubHelper(IHubContext hub)
        {
            this.hub = hub;
        }

        public void UpdateProgress(double progress)
        {
            // Call the broadcastMessage method to update clients.
            hub.Clients.All.updateProgress(progress);
        }

        public void AddNewItem(BokingarViewModel boking)
        {
            // Call the broadcastMessage method to update clients.
            hub.Clients.All.addNewItem(boking);
        }

        public void AddOldItem(BokingarViewModel boking)
        {
            // Call the broadcastMessage method to update clients.
            hub.Clients.All.addOldItem(boking);
        }

        public void AddErrorItem(BootstrapWebApplication.Controllers.LundinBokingarController.ImportError errorItem)
        {
            // Call the broadcastMessage method to update clients.
            hub.Clients.All.addErrorItem(errorItem);
        }
    }
    public class LundinBokingarController : Controller<LundinAlkaPaymentBooking>
    {
        public enum ImportErrorType
        {
            MEMBERSHIP_PAYMENT,
            ALKA_PAYMENT
        }

        public ActionResult ImportAlka()
        {
            return View();
        }

        public ActionResult ImportMembershipPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StartImportMembershipPayment()
        {
            // bóking av ALKA:
            //  Leitar uppá skip kotu og túr nr. um skip ella túrur ikki er til, skal hetta í error lista. 
            // Annars skal Skjal nummar og upphædd (Túrur/"Goldið") leggjast inn og VIDAR skal møguliga broytast.
            List<Bokingar> list = null;
            using (var dbBok = new Lundin_fff_fffConnection())
            {
                //konto nummar 1010 
                // T.d. FD083-1503 Kredit skal flytast yvir í Túr 1503 kolonnu Limagjald GOldið (skal upprættast).
                // Limagjald og Limagjald Goldið skulu samsvara. Limagjald Goldið skal verða ein kolonna
                //list = dbBok.Bokingar.Where(x => x.KreditKontoNummar == 1010 && x.Tekstur.Contains("-")).ToList();  Hans: gamalt
                list = dbBok.Bokingar.Where(x => (x.KreditKontolistiId == 1 || x.DebitKontolistiId == 1) && x.Tekstur.Contains("-")).ToList(); // Hans: tak allar bókingar við á kto 1010 deb/kred, KreditKontolistiId 1 er konto 1010
                //((DebitKontolistiId = 127 and  KreditKontolistiId =1) or (DebitKontolistiId = 1 and  KreditKontolistiId =127))
                dbBok.Dispose();
            }
            var errors = new List<ImportError>();
            var newlyAdded = new List<Bokingar>();
            var alreadyAdded = new List<Bokingar>();
            var hubCtx = GlobalHost.ConnectionManager.GetHubContext<LundinBookingsHub>();
            var hub = new LundinBookingsHubHelper(hubCtx);
            decimal temp_amount = 0;
            BootstrapContext db = null;
            try
            {
                db = new BootstrapContext();
                db.Configuration.AutoDetectChangesEnabled = false;
                var count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    var alka = list[i];
                    var progress = Math.Round((((i + 1) * 100.0) / (double)count), 2);
                    hub.UpdateProgress(progress);
                    // Skip text which contains '*' because it's manually entered into the system. This is usually payment for 'cargo vessels' (hj).
                    if (alka.KreditKontoNummar == 1010) // Takk hædd fyri debet ella kredit
                        temp_amount = alka.Upphadd;
                    else
                        temp_amount = -alka.Upphadd;
                    if (!alka.Tekstur.Contains("*"))
                    {
                        var descSplit = alka.Tekstur.Split('-'); // Format: [SHIP_CODE]-[TRIP_NUMBER] (e.g. FD083-1503)
                        var alkaShipCode = String.Empty;
                        var alkaTripNumber = -1;
                        if (descSplit.Length == 2)
                        {
                            alkaShipCode = descSplit[0];
                            if (int.TryParse(descSplit[1], out alkaTripNumber) && !string.IsNullOrEmpty(alkaShipCode) && alkaTripNumber > 0)
                            {
                                var trip = db.Trips.Where(x => x.Ship != null && x.Ship.Code == alkaShipCode && x.TripId == alkaTripNumber && !x.ChangeEvent.DeletedOn.HasValue).FirstOrDefault(); //  Hans, ikki taka strikaðar túrar við
                                if (trip != null)
                                {
                                    // Check if the trip contains this booking, if not, then add it to trip membership payment bookings
                                    if (db.LundinMembershipPaymentBookings.Where(x => x.BokID == alka.BokID).FirstOrDefault() == null)
                                    {
                                        var lundinMembershipPaymentBooking = new LundinMembershipPaymentBooking()
                                        {
                                            BokID = alka.BokID,
                                            Amount = temp_amount, // alka.Upphadd,
                                            Date = alka.Dato,
                                            Description = alka.Tekstur,
                                            JournalNumber = alka.Skjal,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber,
                                            Trip = trip,
                                            ChangeEvent = CreateChangeEvent()
                                        };
                                        trip.LundinMembershipPaymentBookings.Add(lundinMembershipPaymentBooking);
                                        db.LundinMembershipPaymentBookings.Add(lundinMembershipPaymentBooking);
                                        newlyAdded.Add(alka);
                                        hub.AddNewItem(new BokingarViewModel()
                                        {
                                            BokID = alka.BokID,
                                            Date = alka.Dato,
                                            Amount = temp_amount, // alka.Upphadd,
                                            JournalNumber = alka.Tekstur,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber
                                        });
                                    }
                                    else
                                    {
                                        alreadyAdded.Add(alka);
                                        hub.AddOldItem(new BokingarViewModel()
                                        {
                                            BokID = alka.BokID,
                                            Date = alka.Dato,
                                            Amount = temp_amount, // alka.Upphadd,
                                            JournalNumber = alka.Tekstur,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber
                                        });
                                    }
                                    var bokingTotal = 0m;
                                    foreach (var booking in trip.LundinMembershipPaymentBookings)
                                    {
                                        bokingTotal += booking.Amount;
                                    }
                                    trip.MembershipPaymentPaid = bokingTotal;
                                    trip.MembershipPaymentNr = alka.Skjal;
                                    db.Entry(trip).State = EntityState.Modified;
                                    db = ResetContext(db, i, 500, true);
                                }
                                else
                                {
                                    // Ship or trip (or both) has/have not yet been created in Skipast
                                    var error = new ImportError()
                                    {
                                        ShipCode = alkaShipCode,
                                        TripId = alkaTripNumber,
                                        Amount = temp_amount, // alka.Upphadd,
                                        Type = ImportErrorType.MEMBERSHIP_PAYMENT
                                    };
                                    errors.Add(error);
                                    //hub.Clients.All.addErrorItem(error);
                                    hub.AddErrorItem(error);
                                }
                            }
                        }
                    }
                }
                hub.UpdateProgress(100.0);
                db.Configuration.AutoDetectChangesEnabled = true;
                db.SaveChanges();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
            WriteToLog(newlyAdded, alreadyAdded, errors);
            return new EmptyResult();
        }
        
        [HttpPost]
        public ActionResult StartImportAlka()
        {
            // bóking av ALKA:
            //  Leitar uppá skip kotu og túr nr. um skip ella túrur ikki er til, skal hetta í error lista. 
            // Annars skal Skjal nummar og upphædd (Túrur/"Goldið") leggjast inn og VIDAR skal møguliga broytast.
            List<Bokingar> list = null;
            using (var dbBok = new lundin_fff_alkaEntities())
            {
                //konto nummur 1010-1013
//                list = dbBok.Bokingar.Where(x => x.KreditKontoNummar >= 1010 && x.KreditKontoNummar <= 1013 && x.Tekstur.Contains("-")).ToList(); //
                list = dbBok.Bokingar.Where(x => ((x.KreditKontolistiId >= 3 && x.KreditKontolistiId <= 6) || (x.DebitKontolistiId >= 3 && x.DebitKontolistiId <= 6)) && x.Tekstur.Contains("-")).ToList(); // Hans: tak allar bókingar við á kto 1010, 1011, 1012, 1013 deb/kred, KreditKontolistiId 3,4,5,6 er konto 1010-13
                dbBok.Dispose();
            }
            var errors = new List<ImportError>();
            var newlyAdded = new List<Bokingar>();
            var alreadyAdded = new List<Bokingar>();
            var hubCtx = GlobalHost.ConnectionManager.GetHubContext<LundinBookingsHub>();
            var hub = new LundinBookingsHubHelper(hubCtx);
            decimal temp_amount = 0;
            BootstrapContext db = null;
            try
            {
                db = new BootstrapContext();
                db.Configuration.AutoDetectChangesEnabled = false;
                var count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    var alka = list[i];
                    var progress = Math.Round((((i + 1) * 100.0) / (double)count), 2);
                    //hub.Clients.All.updateProgress(progress);
                    hub.UpdateProgress(progress);
                    //hub.Clients.All.broadcastProgress(progress);
                    // Skip text which contains '*' because it's manually entered into the system. This is usually payment for 'cargo vessels' (hj).
                    if (alka.KreditKontoNummar >= 1010 && alka.KreditKontoNummar <= 1013) // Takk hædd fyri debet ella kredit
                        temp_amount = alka.Upphadd;
                    else
                        temp_amount = -alka.Upphadd;

                    if (!alka.Tekstur.Contains("*"))
                    {
                        var descSplit = alka.Tekstur.Split('-'); // Format: [SHIP_CODE]-[TRIP_NUMBER] (e.g. FD083-1503)
                        var alkaShipCode = String.Empty;
                        var alkaTripNumber = -1;
                        if (descSplit.Length == 2)
                        {
                            alkaShipCode = descSplit[0];
                            if (int.TryParse(descSplit[1], out alkaTripNumber) && !string.IsNullOrEmpty(alkaShipCode) && alkaTripNumber > 0)
                            {
                                var trip = db.Trips.Where(x => x.Ship != null && x.Ship.Code == alkaShipCode && x.TripId == alkaTripNumber).FirstOrDefault();
                                if (trip != null)
                                {
                                    // Check if the trip contains this booking, if not, then add it to trip membership payment bookings
                                    if (db.LundinAlkaPaymentBookings.Where(x => x.BokID == alka.BokID).FirstOrDefault() == null)
                                    {
                                        var lundinAlkaPaymentBooking = new LundinAlkaPaymentBooking()
                                        {
                                            BokID = alka.BokID,
                                            Amount = temp_amount, //alka.Upphadd,
                                            Date = alka.Dato,
                                            Description = alka.Tekstur,
                                            JournalNumber = alka.Skjal,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber,
                                            Trip = trip,
                                            ChangeEvent = CreateChangeEvent()
                                        };
                                        trip.LundinAlkaPaymentBookings.Add(lundinAlkaPaymentBooking);
                                        db.LundinAlkaPaymentBookings.Add(lundinAlkaPaymentBooking);
                                        newlyAdded.Add(alka);
                                        hub.AddNewItem(new BokingarViewModel()
                                        {
                                            BokID = alka.BokID,
                                            Date = alka.Dato,
                                            Amount = temp_amount, //alka.Upphadd,
                                            JournalNumber = alka.Tekstur,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber
                                        });
                                    }
                                    else
                                    {
                                        alreadyAdded.Add(alka);
                                        hub.AddOldItem(new BokingarViewModel()
                                        {
                                            BokID = alka.BokID,
                                            Date = alka.Dato,
                                            Amount = temp_amount, // alka.Upphadd,
                                            JournalNumber = alka.Tekstur,
                                            ShipCode = alkaShipCode,
                                            TripNumber = alkaTripNumber
                                        });
                                    }
                                    var bokingTotal = 0m;
                                    foreach (var booking in trip.LundinAlkaPaymentBookings)
                                    {
                                        bokingTotal += booking.Amount;
                                    }
                                    trip.TRYG_KRHB = bokingTotal;
                                    // Override the journal number and date
                                    trip.TRYG_BILAG = alka.Skjal;
                                    trip.TRYG_DATO = alka.Dato;
                                    db.Entry(trip).State = EntityState.Modified;
                                    db = ResetContext(db, i, 500, true);
                                }
                                else
                                {
                                    // Ship or trip (or both) has/have not yet been created in Skipast
                                    var error = new ImportError()
                                    {
                                        ShipCode = alkaShipCode,
                                        TripId = alkaTripNumber,
                                        Amount = temp_amount, // alka.Upphadd,
                                        Type = ImportErrorType.ALKA_PAYMENT
                                    };
                                    errors.Add(error);
                                    //hub.Clients.All.addErrorItem(error);
                                    hub.AddErrorItem(error);
                                }
                            }
                        }
                    }
                }
                hub.UpdateProgress(100.0);
                //hub.Clients.All.broadcastProgress("100.00");
                db.Configuration.AutoDetectChangesEnabled = true;
                db.SaveChanges();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
            WriteToLog(newlyAdded, alreadyAdded, errors);
            return new EmptyResult();
        }

        void WriteToLog(List<Bokingar> newlyAdded, List<Bokingar> alreadyAdded, List<ImportError> errors)
        {
            var sbNewlyAdded = new StringBuilder();
            foreach (var item in newlyAdded)
            {
                sbNewlyAdded.AppendLine("BokID: " + item.BokID + ", Date: " + item.Dato.ToShortDateString() + ", JNR: " + item.Skjal + ", Desc: " + item.Tekstur + ", Amount: " + item.Upphadd);
            }
            System.IO.File.WriteAllText(GetPath("membership_payment_new.txt"), sbNewlyAdded.ToString());

            var sbAlreadyAdded = new StringBuilder();
            foreach (var item in alreadyAdded)
            {
                sbAlreadyAdded.AppendLine("BokID: " + item.BokID + ", Date: " + item.Dato.ToShortDateString() + ", JNR: " + item.Skjal + ", Desc: " + item.Tekstur + ", Amount: " + item.Upphadd);
            }
            System.IO.File.WriteAllText(GetPath("membership_payment_old.txt"), sbAlreadyAdded.ToString());

            var sbError = new StringBuilder();
            foreach (var item in errors)
            {
                sbError.AppendLine("Trip ID: " + item.TripId + ", Ship Code: " + item.ShipCode + ", Amount: " + item.Amount);
            }
            System.IO.File.WriteAllText(GetPath("membership_payment_errors.txt"), sbError.ToString());
        }

        //[HttpPost]
        //public ActionResult StartImportAlka()
        //{
        //    // bóking av ALKA:
        //    //  Leitar uppá skip kotu og túr nr. um skip ella túrur ikki er til, skal hetta í error lista. Annars skal Skjal nummar og upphædd (Túrur/"Goldið") leggjast inn og VIDAR skal møguliga broytast.

        //    //5703,28.05.2015,13173,"KG652-1512",736.00


        //    var alkaList = Parse("finjour.csv").ToList();
        //    var alkaErrors = new List<ImportError>();
        //    var alkaNewlyAdded = new List<ALKA>();
        //    var alkaAlreadyAdded = new List<ALKA>();
        //    var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();

        //    // 1010-1013

        //    BootstrapContext db = null;
        //    try
        //    {
        //        db = new BootstrapContext();
        //        db.Configuration.AutoDetectChangesEnabled = false;
        //        var count = alkaList.Count;
        //        for (int i = 0; i < count; i++)
        //        {
        //            var alka = alkaList[i];
        //            var progress = Math.Round((((i + 1) * 100.0) / (double)count), 2);
        //            hub.Clients.All.broadcastProgress(progress);
        //            if (!alka.Description.Contains("*"))
        //            {
        //                var descSplit = alka.Description.Split('-');
        //                var alkaShipCode = String.Empty;
        //                var alkaTripNumber = -1;
        //                if (descSplit.Length == 2)
        //                {
        //                    alkaShipCode = descSplit[0];
        //                    if (int.TryParse(descSplit[1], out alkaTripNumber) && !string.IsNullOrEmpty(alkaShipCode) && alkaTripNumber > 0)
        //                    {
        //                        var trip = db.Trips.Where(x => x.Ship != null && x.Ship.Code == alkaShipCode && x.TripId == alkaTripNumber).FirstOrDefault();
        //                        if (trip != null)
        //                        {
        //                            if (!trip.TRYG_BILAG.HasValue && !trip.TRYG_DATO.HasValue && !trip.TRYG_KRHB.HasValue)
        //                            {
        //                                alkaNewlyAdded.Add(alka);
        //                                // Increment the amount
        //                                trip.TRYG_KRHB += alka.Amount;
        //                                // Override the journal number
        //                                trip.TRYG_BILAG = alka.JournalNumber;
        //                                trip.TRYG_DATO = alka.Date;
        //                                db.Entry(trip).State = EntityState.Modified;
        //                                db = ResetContext(db, i, 500, true);
        //                            }
        //                            else
        //                            {
        //                                alkaAlreadyAdded.Add(alka);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            alkaErrors.Add(new ImportError()
        //                            {
        //                                ShipCode = alkaShipCode,
        //                                TripId = alkaTripNumber,
        //                                Amount = alka.Amount,
        //                                Type = ImportErrorType.ALKA_PAYMENT
        //                            });
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        hub.Clients.All.broadcastProgress("100.00");
        //        db.Configuration.AutoDetectChangesEnabled = true;
        //        db.SaveChanges();
        //    }
        //    finally
        //    {
        //        if (db != null)
        //        {
        //            db.Dispose();
        //        }
        //    }

        //    var sbAlkaNewlyAdded = new StringBuilder();
        //    foreach (var item in alkaNewlyAdded)
        //    {
        //        sbAlkaNewlyAdded.AppendLine("NR: " + item.NR + ", Date: " + item.Date.ToShortDateString() + ", JNR: " + item.JournalNumber + ", Desc: " + item.Description + ", Amount: " + item.Amount);
        //    }
        //    System.IO.File.WriteAllText(GetPath("alka_new.txt"), sbAlkaNewlyAdded.ToString());

        //    var sbAlkaAlreadyAdded = new StringBuilder();
        //    foreach (var item in alkaAlreadyAdded)
        //    {
        //        sbAlkaAlreadyAdded.AppendLine("NR: " + item.NR + ", Date: " + item.Date.ToShortDateString() + ", JNR: " + item.JournalNumber + ", Desc: " + item.Description + ", Amount: " + item.Amount);
        //    }
        //    System.IO.File.WriteAllText(GetPath("alka_old.txt"), sbAlkaAlreadyAdded.ToString());

        //    var sbAlkaError = new StringBuilder();
        //    foreach (var item in alkaErrors)
        //    {
        //        sbAlkaError.AppendLine("Trip ID: " + item.TripId + ", Ship Code: " + item.ShipCode + ", Amount: " + item.Amount);
        //    }
        //    System.IO.File.WriteAllText(GetPath("alka_errors.txt"), sbAlkaError.ToString());

        //    return View(alkaList);
        //}

        private BootstrapContext ResetContext(BootstrapContext context, int count, int _commitCount, bool recreateContext)
        {
            if (count % _commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new BootstrapContext();
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return context;
        }
        private string GetPath(string path)
        {
            // Some browsers send file names with full path. We only care about the file name.
            return System.IO.Path.Combine(Server.MapPath("~/App_Data/ALKA"), System.IO.Path.GetFileName(path));
        }

        public ICollection<ALKA> Parse(string fileName)
        {
            var file = GetPath(fileName);
            var alkaList = new List<ALKA>();
            string[] fileData = null;
            try
            {
                fileData = System.IO.File.ReadAllLines(file, System.Text.Encoding.Default);
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't read the file!" + Environment.NewLine + "Message: " + ex.Message);
            }
            foreach (var str in fileData)
            {
                var strSplit = str.Split(';');
                var alka = new ALKA();
                int alkaNR = -1;
                if (int.TryParse(strSplit[0], out alkaNR))
                {
                    alka.NR = alkaNR;
                }
                else
                {
                    throw new Exception("Parsing ALKA NR: " + strSplit[0] + " failed!");
                }
                DateTime alkaDate = DateTime.Now;
                if (DateTime.TryParseExact(strSplit[1], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out alkaDate))
                {
                    alka.Date = alkaDate;
                }
                else
                {
                    throw new Exception("Parsing ALKA DateTime: " + strSplit[1] + " failed!");
                }
                int alkaJournalNumber = -1;
                if (int.TryParse(strSplit[2], out alkaJournalNumber))
                {
                    alka.JournalNumber = alkaJournalNumber;
                }
                else
                {
                    throw new Exception("Parsing ALKA Journal NR: " + strSplit[2] + " failed!");
                }
                alka.Description = strSplit[3];
                decimal alkaAmount = -1;
                if (decimal.TryParse(strSplit[4], NumberStyles.Number, CultureInfo.InvariantCulture, out alkaAmount))
                {
                    alka.Amount = alkaAmount;
                }
                else
                {
                    throw new Exception("Parsing ALKA Amount: " + strSplit[4] + " failed!");
                }
                alkaList.Add(alka);
            }
            return alkaList;
        }

        public class ImportError
        {
            public string ShipCode { get; set; }
            public int TripId { get; set; }
            public decimal Amount { get; set; }
            public ImportErrorType Type { get; set; }
        }

        protected override IQueryable<LundinAlkaPaymentBooking> PagedListFilter(IQueryable<LundinAlkaPaymentBooking> list, string filterName = null)
        {
            return list;
        }

        protected override void AddViewBag(LundinAlkaPaymentBooking obj)
        {

        }

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            return null;
        }
    }
}