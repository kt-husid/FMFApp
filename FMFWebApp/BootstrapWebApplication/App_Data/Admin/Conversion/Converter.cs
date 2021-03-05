using BootstrapWebApplication.Admin.Conversion.ClassConverters;
using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.Models.Old;
using Kthusid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Transactions;

namespace BootstrapWebApplication.Admin.Conversion
{
    public class Converter
    {
        ConverterData converterData;
        public string DateTimeFormat { get; private set; }

        public Converter(string dateTimeFormat)
        {
            this.DateTimeFormat = dateTimeFormat;
            converterData = new ConverterData();

            // Encoding Code Page List:
            // http://msdn.microsoft.com/en-us/library/windows/desktop/dd317756(v=vs.85).aspx
            var pc8enc = Encoding.GetEncoding(850);

            //string[] formats = { 
            //                       "dMyy",
            //                       "ddMyy",
            //                       "ddMMyy",
            //                       "dd-MM-yy",
            //                       "dd-MM-yyy",
            //                       "dd-MM-yyyy",

            //                       "d/M/yy",
            //                       "d/MM/yy",
            //                       "dd/MM/yy",
            //                       "dd/M/yy",

            //                       "d/M/yyyy",
            //                       "d/MM/yyyy",

            //                       "dd/M/yyyy",
            //                       "dd/MM/yyyy",

            //                       "MM/dd/yyyy"
            //                   };
            //DateTime dt = DateTime.Now;
            //var isDateTime = DateTime.TryParseExact("29/01/10", formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);

           
            //FixTripShipTypeId();
            

            //Convert<P200, Ship>("P200.csv", (db, o) => new UpdateShip(db, o, converterData));
            //var csvFile = GetFile("P200.csv");
            //var csvReader = new CSVReader(csvFile, Encoding.Default, ';', '"');
            //var csv2json = new CSVToJSON(csvReader, this.DateTimeFormat);
            //csv2json.JsonConvertError += (o, e) =>
            //{
            //    Debug.WriteLine(e.Data);
            //    throw new Exception("Conversion from CSV to JSON contains errors!");
            //};
            //var shipsFromFile = csv2json.Parse<P200>();
            //using (var db = new BootstrapContext())
            //{
            //    foreach (var o in shipsFromFile)
            //    {
            //        var ship = db.Ships.Where(x => x.Code == o.ShipCode).FirstOrDefault();
            //        var shipType = db.ShipTypes.Where(t => t.Code == o.TYPA).FirstOrDefault();
            //        if (ship != null && shipType != null)
            //        {
            //            ship.ShipType = shipType;
            //            ship.ShipTypeId = shipType.Id;
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //var csvFile = GetFile("P100.csv");
            //var csvReader = new CSVReader(csvFile, Encoding.Default, ';', '"');
            //var csv2json = new CSVToJSON(csvReader, this.DateTimeFormat);
            //csv2json.JsonConvertError += (o, e) =>
            //{
            //    Debug.WriteLine(e.Data);
            //    throw new Exception("Conversion from CSV to JSON contains errors!");
            //};
            //var membersFromFile = csv2json.Parse<P100>();
            //using (var db = new BootstrapContext())
            //{
            //    foreach (var o in membersFromFile)
            //    {
            //        var member = db.Members.Where(x => x.NR == o.NR).FirstOrDefault();
            //        if (member != null)
            //        {
            //            if (member.Person != null)
            //            {
            //                member.Person.IsAlive = !("" + o.STRIKA).ToLower().Equals("d");
            //            }
            //            member.Status = ("" + o.STRIKA).ToLower();
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //return;

            CreatePersonTitles();
            CreatePersonGenders();
            CreateBankAccount();

            Convert<REG_NR, Bank>("REG_NR.csv", (db, o) => new REG_NRToBank(db, o, converterData));
            Convert<TYPA, ShipType>("TYPA.csv", (db, o) => new TYPAToShipType(db, o, converterData));//);//, pc8enc);
            Convert<POSTNR, Postal>("postnr.csv", (db, o) => new POSTNRToPostal(db, o, converterData));
            Convert<P300, ShippingCompany>("P300.csv", (db, o) => new P300ToShippingCompany(db, o, converterData));//, pc8enc);
            Convert<P200, Ship>("P200.csv", (db, o) => new P200ToShip(db, o, converterData));//, pc8enc);
            Convert<STXT, ShipComment>("STXT.csv", (db, o) => new STXTToShipComment(db, o, converterData));//, pc8enc);
            Convert<LSLAG, MemberType>("LSLAG.csv", (db, o) => new LSLAGToMemberType(db, o, converterData));//, pc8enc);

            // Create MemberType "UK - Unknown"
            using (var db = new BootstrapContext())
            {
                var changeEventHandler = new ChangeEventHandler("bha");
                var changeEvent = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEvent);
                db.MemberTypes.Add(new MemberType()
                {
                    Code = "uk",
                    Description = "",
                    BREV_UT = "",
                    ChangeEvent = changeEvent
                });
                db.SaveChanges();
            }

            Convert<STARV, Job>("starv.csv", (db, o) => new STARVToJob(db, o, converterData));
            Convert<STAT, Status>("stat.csv", (db, o) => new StatToStatus(db, o, converterData));
            Convert<P600, Company>("P600.csv", (db, o) => new P600ToCompany(db, o, converterData));

            // Create company "Uttan avreiðingarstað"
            using (var db = new BootstrapContext())
            {
                var changeEventHandler = new ChangeEventHandler("bha");
                var entity = new Entity();
                db.Entities.Add(entity);
                //var changeEvent = new ChangeEvent();
                //db.ChangeEvents.Add(changeEvent);
                var changeEvent = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEvent);
                db.Companies.Add(new Company()
                {
                    CID = null,
                    Code = "ua",
                    Name = "Uttan avreiðingarstað",
                    Description = "",
                    ContactCompanyName = "",
                    ContactPersonName = "",
                    //AVR_IALT = ?,
                    //KG_IALT = ?,
                    //LAST_DATO = ?,
                    //LAST_ID = ?,
                    Entity = entity,
                    ChangeEvent = changeEvent
                });
                db.SaveChanges();
            }

            Convert<P100, Member>("p100.csv", (db, o) => new P100ToMember(db, o, converterData));//, pc8enc);
            Convert<PTXT, MemberComment>("ptxt.csv", (db, o) => new PTXTToMemberComment(db, o, converterData));//, pc8enc);
            Convert<LGJALD, MemberPayment>("LGJALD.csv", (db, o) => new LGJALDToMemberPayment(db, o, converterData));//, pc8enc);
            Convert<STUDART, AidType>("STUDART.csv", (db, o) => new STUDARTToAidType(db, o, converterData));//, pc8enc);
            Convert<MINLON, MinimumWage>("MINLON.csv", (db, o) => new MINLONTOMinimumWage(db, o, converterData));//, pc8enc);
            Convert<FISKART, FishSpecies>("FISKART.csv", (db, o) => new FISKARTToFishSpecies(db, o, converterData));

            UpdateFishCodes("FishSpeciesNew.csv", converterData);

            Convert<FISKPRI, FishPrice>("FISKPRIS.csv", (db, o) => new FISKPRISToFishPrice(db, o, converterData));
            Convert<STUDPRI, AidPrice>("STUDPRIS.csv", (db, o) => new STUDPRIToAidPrice(db, o, converterData));
            Convert<TR_HOVD, Trip>("TR_HOVD.csv", (db, o) => new TR_HOVDToTrip(db, o, converterData));
            Convert<TR_LINE, TripLine>("TR_LINE.csv", (db, o) => new TR_LINEToTripLine(db, o, converterData));

            ////TripShipAid and TripCrewAid are not used anymore. Don't uncomment !!
            ////Convert<TR_STUDS, TripShipAid>("TR_STUDS.csv", (db, o) => new TR_STUDSToTripShipAid(db, o, converterData));
            ////Convert<TR_STUDM, TripCrewAid>("TR_STUDM.csv", (db, o) => new TR_STUDMToTripCrewAid(db, o, converterData));

            Convert<FRT_LON, HolidayPay>("frt_lon.csv", (db, o) => new FRT_LONToHolidayPay(db, o, converterData));
            Convert<FRT_HOVD, HolidayPay_HOVD>("FRT_HOVD.csv", (db, o) => new FRT_HOVDToFRT_HolidayPay_HOVD(db, o, converterData));//, pc8enc);
            Convert<MI_HOVD, SignOn>("MI_HOVD.csv", (db, o) => new MI_HOVDToSignOn(db, o, converterData));//, pc8enc);

            // Member ChangeEventItems
            Convert<TRANS_P, ChangeEventItem>("TRANS_P.csv", (db, o) => new TRANS_PToChangeEventItem(db, o, converterData));
            // Company ChangeEventItems
            Convert<TRANS_V, ChangeEventItem>("TRANS_V.csv", (db, o) => new TRANS_VToChangeEventItem(db, o, converterData));
            // ShippingCompany ChangeEventItems
            Convert<TRANS_R, ChangeEventItem>("TRANS_R.csv", (db, o) => new TRANS_RToChangeEventItem(db, o, converterData));
            // Ship ChangeEventItems
            Convert<TRANS_S, ChangeEventItem>("TRANS_S.csv", (db, o) => new TRANS_SToChangeEventItem(db, o, converterData));
            // ???? ChangeEventItems
            //Convert<TRANS_TR, ChangeEventItem>("TRANS_TR.csv", (db, o) => new TRANS_TRToChangeEventItem(db, o, converterData));
            // Trans_MI isn't used because its data are very old (last with change are from 1992)
            // Trans_TP isn't used because it's related to Transport (p122)

            //Convert<BootstrapWebApplication.Models.Old.MI2_HOVD, BootstrapWebApplication.Models.MI2_HOVD>("MI2_HOVD.csv", (db, o) => new MI2_HOVDConverter(db, o, converterData));//, pc8enc);
            Convert<FRAD_ART, DeductionType>("FRAD_ART.csv", (db, o) => new FRAD_ARTToDeductionType(db, o, converterData));
            Convert<TR_KRED, TripDeduction>("TR_KRED.csv", (db, o) => new TR_KREDToTripDeduction(db, o, converterData));
            Convert<UM_LINE, MemberFinancialAid>("UM_LINE.csv", (db, o) => new UM_LINEToMemberFinancialAid(db, o, converterData));
            Convert<BootstrapWebApplication.Models.Old.DEB_CON, BootstrapWebApplication.Models.DEB_CON>("DEB_CON.csv", (db, o) => new DEB_CONConvert(db, o, converterData));
            Convert<LGJALD2, MemberPayment2>("LGJALD2.csv", (db, o) => new LGJALD2ToMemberPayment2(db, o, converterData));

            //AddLastSignOnToMember();

            //FixMemberSignOns();
            // Shouldn't be nessecary:
            //FixBankAccounts();
        }

        private void CreateBankAccount()
        {
            var db = new BootstrapContext();
            var changeEventHandler = new ChangeEventHandler("bha");
            if (db.Banks.Where(x => x.RegNumber == 0).FirstOrDefault() == null)
            {
                var changeEvent = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEvent);

                db.Banks.Add(new Bank()
                {
                    RegNumber = 0,
                    Name = "UNKNOWN",
                    ChangeEvent = changeEvent
                });
                db.SaveChanges();
            }
        }

        private void CreatePersonGenders()
        {
            var db = new BootstrapContext();
            var changeEventHandler = new ChangeEventHandler("bha");
            if (db.PersonGenders.Count() == 0)
            {
                var changeEventMale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventFemale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventUnknown = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "M", // Male
                    Description = "M",
                    ChangeEvent = changeEventMale
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    // Female
                    Code = "F",
                    Description = "F",
                    ChangeEvent = changeEventFemale
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "U", // Unknown
                    Description = "U",
                    ChangeEvent = changeEventUnknown
                });
                db.SaveChanges();
            }
        }

        private void CreatePersonTitles()
        {
            var db = new BootstrapContext();
            var changeEventHandler = new ChangeEventHandler("bha");

            if (db.PersonTitles.Count() == 0)
            {
                var changeEventMale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventFemale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventUnknown = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mr",
                    Description = "Mr.",
                    ChangeEvent = changeEventMale
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mrs",
                    Description = "Mrs.",
                    ChangeEvent = changeEventFemale
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "U",
                    Description = "Unknown",
                    ChangeEvent = changeEventUnknown
                });
                db.SaveChanges();
            }
        }

        private void FixTripShipTypeId()
        {
            var csvFile = GetFile("TR_HOVD.csv");
            var csvReader = new CSVReader(csvFile, Encoding.Default, ';', '"');
            var csv2json = new CSVToJSON(csvReader, this.DateTimeFormat);
            csv2json.JsonConvertError += (o, e) =>
            {
                Debug.WriteLine(e.Data);
                throw new Exception("Conversion from CSV to JSON contains errors!");
            };
            var tripFromFile = csv2json.Parse<TR_HOVD>();
            BootstrapContext db = null;
            try
            {
                db = new BootstrapContext();
                db.Configuration.AutoDetectChangesEnabled = false;
                for (int i = 0; i < tripFromFile.Count; i++)
                {
                    var o = tripFromFile[i];
                    var trip = db.Trips.Where(x => x.TripId == o.ID_TUR && x.TripNumber == o.TR_NR).FirstOrDefault();
                    var shipType = db.ShipTypes.Where(x => x.Code == o.SK_ID).FirstOrDefault();
                    if (trip != null && shipType != null)
                    {
                        trip.ShipTypeCode = shipType.Code;
                        trip.ShipTypeId = shipType.Id;
                        db.Entry(trip).State = EntityState.Modified;
                        db = ResetContext(db, i, 500, true);
                    }
                    Debug.WriteLine(i + " / " + tripFromFile.Count);
                }
                db.SaveChanges();
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        private void UpdateFishCodes(string csvFileName, ConverterData converterData, Encoding encoding = null, char delimiter = ';', char quotationChar = '"')
        {
            encoding = encoding != null ? encoding : Encoding.Default;
            var csvFile = GetFile(csvFileName);
            Debug.WriteLine("Reading CSV file " + csvFileName + "...");
            var csv2json = new CSVToJSON(new CSVReader(csvFile, encoding, delimiter, quotationChar), this.DateTimeFormat);
            csv2json.JsonConvertError += (o, e) =>
            {
                Debug.WriteLine(e.Data);
                throw new Exception("Conversion from CSV to JSON contains errors!");
            };
            var fishSpeciesFromFile = csv2json.Parse<FishSpeciesFromFile>();
            var itemsToBeUpdated = new List<FishSpecies>();
            var itemsToBeAdded = new List<FishSpecies>();
            using (var db = new BootstrapContext())
            {
                var allItems = db.FishSpecies.ToList();
                var totalItems = 0;
                var activeItems = 0;
                /*
                 * The new FishSpecies (excel file from HJ) are now used instead of the old.
                 * The new values should be mapped to the old data.
                 * Loop through the items in the new fish species file
                 * 
                 */
                var changeEventHandler = new ChangeEventHandler("bha");
                foreach (var item in fishSpeciesFromFile)
                {
                    var foundItem = allItems.Where(x => x.Code.ToLower().Equals(item.FiskaslagKota.ToLower()) || x.Name.ToLower().Equals(item.Fiskaheiti.ToLower())).FirstOrDefault();
                    // only update or add if not already has done this
                    if (!itemsToBeUpdated.Contains(foundItem) && !itemsToBeAdded.Contains(foundItem))
                    {
                        if (foundItem != null && foundItem.Id != 0)
                        {
                            // This item already exists in the db. Update it's value with the new data
                            foundItem.FishSpeciesNumber = item.FiskaslagNr;
                            foundItem.OldCode = foundItem.Code;
                            foundItem.Code = item.FiskaslagKota;
                            foundItem.Description = item.Fiskaheiti2;
                            foundItem.IsActive = true;
                            itemsToBeUpdated.Add(foundItem);
                            allItems.Add(foundItem);
                        }
                        else
                        {
                            var changeEvent = changeEventHandler.Create(db);
                            db.ChangeEvents.Add(changeEvent);
                            // the new item doesn't exist in the db. Create and add it.
                            var newItem = new FishSpecies()
                            {
                                FishSpeciesNumber = item.FiskaslagNr,
                                Code = item.FiskaslagKota,
                                OldCode = null,
                                Name = item.Fiskaheiti,
                                OldName = null,
                                Description = item.Fiskaheiti2,
                                ALIAS = item.FiskaslagKota,
                                LINK = null,
                                RAD = null,
                                IsActive = true,
                                ChangeEvent = changeEvent
                            };
                            db.FishSpecies.Add(newItem);
                            itemsToBeAdded.Add(newItem);
                            allItems.Add(newItem);
                        }
                    }
                }
                db.SaveChanges();
                // Loop throught all the fish species in the db.
                // If the item isn't found in the new file (means its an old/unused FishSpecies) and set it to be inactive.
                //foreach (var item in allItems)
                foreach (var item in allItems)// db.FishSpecies)
                {
                    totalItems++;
                    activeItems++;
                    var itemFromFile = fishSpeciesFromFile.Where(x => item.OldCode == null || x.FiskaslagKota.ToLower().Equals((item.OldCode != null ? item.OldCode : "").ToLower()) || x.FiskaslagKota.ToLower().Equals((item.Code).ToLower())).FirstOrDefault();
                    if (itemFromFile == null)
                    {
                        activeItems--;
                        item.IsActive = false;
                    }
                }
                db.SaveChanges();
            }
        }

        void AddLastSignOnToMember()
        {
            BootstrapContext db = null;
            try
            {
                db = new BootstrapContext();
                var c = 0;
                var members = db.Members.ToList();
                var count = members.Count;
                db.Configuration.AutoDetectChangesEnabled = false;
                foreach(var member in members)
                {
                    // load SignOns explicitly
                    db.Members.Attach(member);
                    db.Entry(member).Collection(x => x.SignOns).Load();
                    var lastSignOn = member.SignOns.OrderByDescending(x => x.SignOnNumber).FirstOrDefault();
                    if (lastSignOn != null)
                    {
                        // update the member last signon id
                        member.LastSignOnId = lastSignOn.Id;
                        db.Entry(member).State = EntityState.Modified;
                        db = ResetContext(db, c, 250, true);
                    }
                    c++;
                    Debug.WriteLine(c + " / " + count);
                }
                db.SaveChanges();
            }
            finally
            {
                if (db != null)
                    db.Dispose();
            }
        }

        private void FixMemberSignOns()
        {
            using (var db = new BootstrapContext())
            {
                var signOns = db.SignOns.OrderBy(x => x.PersonNumber).ToList();
                var members = db.Members.ToList();
                var c = 0;
                var tc = members.Count;
                foreach (var m in members)
                {
                    if (m.SignOns.Count() == 0)
                    {
                        var memberSignOns = signOns.Where(s => s.PersonNumber == m.NR);
                        if (memberSignOns.Count() > 0)
                        {
                            foreach (var s in memberSignOns)
                            {
                                s.MemberId = m.Id;
                                m.SignOns.Add(s);
                            }
                        }
                    }
                    c++;
                    Debug.WriteLine(c + " / " + tc);
                }
                db.SaveChanges();
                db.Dispose();
            }
        }

        private void FixBankAccounts()
        {
            BootstrapContext context = null;
            try
            {
                context = new BootstrapContext();
                context.Configuration.AutoDetectChangesEnabled = false;
                var bankAccounts = context.BankAccounts.ToList();
                for (int i = 0; i < bankAccounts.Count(); i++)
                {
                    var bankAccount = bankAccounts[i];
                    var bank = context.Banks.Where(x => x.RegNumber == bankAccount.Bank.RegNumber).FirstOrDefault();
                    bankAccount.Bank = bank;
                    bankAccount.BankId = bank.Id;
                    context.Entry(bankAccount).State = EntityState.Modified;
                    context = ResetContext(context, i, 100, true);
                }
                context.SaveChanges();
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }
        }

        private void Check()
        {
            using (var db = new BootstrapContext())
            {
                int ptc = db.PersonTitles.Count();
                if (ptc != 3) throw new Exception("PersonTitles exception");

                int ptg = db.PersonGenders.Count();
                if (ptg != 3) throw new Exception("PersonGenders exception");

                db.Dispose();
            }
        }

        private string GetFile(string file)
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/Admin/Conversion/DataV3/" + file);
        }

        /// <summary>
        /// Converts a CSV string to a Json array format.
        /// </summary>
        /// <remarks>First line in CSV must be a header with field name columns.</remarks>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="csvFileName"></param>
        /// <param name="encoding"></param>
        /// <param name="delimiter"></param>
        /// <param name="quotationChar"></param>
        /// <param name="convertAction"></param>
        private void Convert<TSource, TTarget>(string csvFileName, Action<BootstrapContext, TSource> convertAction, Encoding encoding = null, char delimiter = ';', char quotationChar = '"')
            //private void Convert<TSource, TTarget>(string csvFileName, IDbConverter<TSource> dbConverter, Encoding encoding = null, char delimiter = ';', char quotationChar = '"')
            where TSource : class, new()
            where TTarget : class, new()
        {
            Check();

            encoding = encoding != null ? encoding : Encoding.Default;
            var csvFile = GetFile(csvFileName);
            var fileNameToConvert = "";
            try
            {
                fileNameToConvert = new FileInfo(csvFile).Name;
            }
            catch { }
            Debug.WriteLine("Reading CSV file " + csvFileName + "...");
            var csvReader = new CSVReader(csvFile, encoding, delimiter, quotationChar);
            var csv2json = new CSVToJSON(csvReader, this.DateTimeFormat);
            csv2json.JsonConvertError += (o, e) =>
            {
                Debug.WriteLine(e.Data);
                throw new Exception("Conversion from CSV to JSON contains errors!");
            };
            //var jsonParsed = csv2json.Parse();

            int itemsInDb = 0;
            //using (var db = new BootstrapContext())
            //{
            //    itemsInDb = db.Set<TTarget>().Count();
            //    // DB probably contains all items
            //    var itemsInFile = csvReader.Rows.Count();
            //    if (itemsInDb == itemsInFile)// convertedObjects.Count)// jsonParsed.Count)
            //    {
            //        Debug.WriteLine("Already converted: " + fileNameToConvert);
            //        return;
            //    }
            //}
            Debug.WriteLine("Parsing CSV file " + csvFileName + " to JSON...");
            var convertedObjects = csv2json.Parse<TSource>();

            //var convertedObjects = new List<TSource>();
            ////var objectsWithError = new List<dynamic>();
            //foreach (var obj in jsonParsed)
            //{
            //    var co = default(TSource);
            //    try
            //    {
            //        co = JsonConvert.DeserializeObject<TSource>(obj, new JsonSerializerSettings() { DateFormatString = DateTimeFormat });
            //        convertedObjects.Add(co);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("CSV to JSON contains invalid data.");
            //        //objectsWithError.Add(new { Object = obj, Exception = ex });
            //    }
            //}

            var totalCount = convertedObjects.Count;
            //return;
            Debug.WriteLine("Inserting data into DB: " + fileNameToConvert);
            int div = totalCount / 100;
            //using (var scope = new TransactionScope())
            //{
            BootstrapContext context = null;
            try
            {
                context = new BootstrapContext();
                context.Configuration.AutoDetectChangesEnabled = false;
                for (int i = itemsInDb; i < totalCount; i++)
                //for (int i = 0; i < totalCount; i++)
                {
                    //dbConverter.Convert(context, convertedObjects[i]);
                    convertAction(context, convertedObjects[i]);
                    context = ResetContext(context, i, commitCount, true);
                    PrintInfo(i, totalCount);
                }
                context.SaveChanges();
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }
            //   scope.Complete();
            //}
        }

        Int32 commitCount = 10;
        DateTime? lastTime;
        double insertionsPerSecondAverage;
        double maxInsertionsPerSecond = 0;
        double totalTimeSpent = 0;
        double[] insertionsPerSecondBuffer = new double[10];
        int insertionsPerSecondBufferCount = 0;

        private double GetAverageInsertionsPerSecond()
        {
            double result = 0.0;
            int length = insertionsPerSecondBuffer.Length;
            for (int i = 0; i < length; i++)
            {
                result += insertionsPerSecondBuffer[i];
            }
            return result / (double)length;
        }

        private void PrintInfo(int i, int totalCount)
        {
            var n = (i + 1);
            lastTime = !lastTime.HasValue ? DateTime.Now : lastTime;
            if (n % commitCount == 0 || n == totalCount)
            {
                TimeSpan timeSpent = DateTime.Now.Subtract(lastTime.Value);
                totalTimeSpent += timeSpent.TotalSeconds;
                //Reset lastTime
                lastTime = DateTime.Now;
                double insertionsPerSecond = (double)commitCount / timeSpent.TotalSeconds;
                insertionsPerSecondBuffer[insertionsPerSecondBufferCount] = insertionsPerSecond;
                insertionsPerSecondBufferCount = ++insertionsPerSecondBufferCount % insertionsPerSecondBuffer.Length;
                if (insertionsPerSecondBufferCount == 0)
                {
                    insertionsPerSecondAverage = GetAverageInsertionsPerSecond();
                }
                if (totalTimeSpent > 5.0 && insertionsPerSecondAverage > maxInsertionsPerSecond)
                {
                    maxInsertionsPerSecond = insertionsPerSecondAverage;
                    commitCount += 10;// = (int)Math.Floor(insertionsPerSecond);
                    totalTimeSpent = 0;
                }
                var pct = (double)n / (double)totalCount;
                pct = Math.Round(pct * 100.0, 2);
                double estTimeLeft = Math.Round((totalCount - n) / insertionsPerSecondAverage, 2);
                var estTimeLeftStr = estTimeLeft > 60 ? Math.Round((estTimeLeft / 60), 0) + " minutes" : estTimeLeft + " seconds";
                Debug.WriteLine("Progress: " + i + "/" + totalCount + " (" + pct + "%). Estimated time left: " + estTimeLeftStr + ". Ins. per sec.: " + insertionsPerSecondAverage + ". CommitCount: " + commitCount);
            }
        }

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
    }
}