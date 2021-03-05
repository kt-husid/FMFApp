//using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BootstrapWebApplication.Models.Old;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System.Globalization;

namespace CodeTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            
            var jsonObject = "{'ID':'1','TypaCode':'3','TEKSTUR':'Partrolarar','STUTT':'Partrolar','RET_DATO':'02/03/2011','RET_HH':'14','RET_MM':'17','RET_ID':'hj','BREV_UT':'','FLYTAST':'','DLISTA':'','BLISTA':'','ARS_LISTI':'','TIL_SKIP':'73,5','TIL_MANN':'26,5'}";
            var jss = new JsonSerializerSettings();
            jss.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "dd'/'MM'/'yyyy"
            });
            jss.Culture = new CultureInfo("en-US", false)
            {
                NumberFormat = new NumberFormatInfo
                {
                    //CurrencyDecimalSeparator = ",",
                    NumberDecimalSeparator = ",",
                    //PercentDecimalSeparator = ","
                }
            };
            //jss.FloatParseHandling = FloatParseHandling.Decimal;
            var jsonObjectSerialized = JsonConvert.DeserializeObject<TYPA>(jsonObject, jss);
            //new IsoDateTimeConverter()
            //{

            //    //Culture = new CultureInfo(string.Empty)
            //    //{
            //    //    NumberFormat = new NumberFormatInfo
            //    //    {
            //    //        CurrencyDecimalSeparator
            //    //    }
            //    //},

            //    DateTimeFormat = "dd'/'MM'/'yyyy"
            //});

            //Console.WriteLine(Convert.ToDateTime("a07/07/1971"));
            //Console.Read();
            //TestConvertNotWorking();
            TestConvertCSVToJSON(args);
            //Console.ReadLine();
            //var dbConvert = new DatabaseConvert();
            //dbConvert.Convert<P100>(Environment.CurrentDirectory + @"\P100.json", (o) =>
            //{
            //    Console.WriteLine(o);
            //    Console.ReadLine();
            //    //foreach (var c in json)
            //    //{
            //    //    //if (ModelState.IsValid)
            //    //    {
            //    //        db.Countries.Add(new BootstrapWebApplication.Models.Country() { CountryCode = c.Alpha2, CountryName = c.Name });
            //    //        Console.WriteLine("Added country " + c.Name);
            //    //    }
            //    //}
            //    //try
            //    //{
            //    //    db.SaveChanges();
            //    //    Console.WriteLine("Done adding countries. Please uncomment the method CreateCountries() in Global.asax.cs!!!");
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Console.WriteLine("Error while saving countries. Message: " + ex.Message);
            //    //    Console.WriteLine(ex.ToString());
            //    //}
            //});

            //TestTryCatch();
            Console.WriteLine("Press ANY KEY to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Converts a CSV string to a Json array format.
        /// </summary>
        /// <remarks>First line in CSV must be a header with field name columns.</remarks>
        /// <param name="value"></param>
        /// <returns></returns>

        private static void TestConvertCSVToJSON(string[] args)
        {
            var pName = "csv2json";
            var pCMD = "file1.csv [file2.csv fileN.csv]";
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Usage: " + pName + ".exe " + pCMD);
            }
            else
            {
                var files = args.Select(s => s.Split(' ')).ToList();
                files.ForEach(f =>
                {
                    var csvFile = f[0];
                    var delimiter = ',';// args.Length == 2 ? args[1] : ",";
                    var hasError = false;
                    var json = ConvertCSVToJSON(csvFile, out hasError, delimiter);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (hasError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        using (var db = new BootstrapContext())
                        {
                            List<PersonGender> genders = db.PersonGenders.ToList();
                            var male = genders[0];
                            var female = genders[1];

                            List<PersonTitle> titles = db.PersonTitles.ToList();
                            var maleTitle = titles[0];
                            var femaleTitle = titles[1];

                            foreach (var obj in json)
                            {
                                var o = JsonConvert.DeserializeObject<P100>(obj, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" });
                                //var person = new Person() 
                                //{
                                //    SSN = ""+o.CPR,
                                //    FirstName = o.FNAVN,
                                //    MiddleName = o.MNAVN,
                                //    LastName = o.ENAVN,
                                //    Birthday = o.FODT,
                                //    GenderId = o.KYN == "m" ? male.Id : female.Id,
                                //    Gender = o.KYN == "m" ? male : female,
                                //    TitleId = o.KYN == "m" ? maleTitle.Id : femaleTitle.Id,
                                //    Title = o.KYN == "m" ? maleTitle : femaleTitle
                                //};
                                //db.Persons.Add(person);
                                //db.SaveChanges();
                                //db.Members.Add(new Member()
                                //{
                                //    PersonId = person.Id,
                                //    Comment = "",//o.TXT_LN,
                                //    JobTitle = o.STARV,
                                //    LastSignOnId = null,
                                //    LastSignOn = null,
                                //    JobId = null,
                                //    Job = null,
                                //    StatId = null,
                                //    Stat = null,
                                //    PostNrId = null,
                                //    PostNr = null,
                                //    MemberTypeId = null,
                                //    MemberType = null,
                                //    PRI_OWN = null,
                                //    BETALER = null,
                                //    M_STATUS = null
                                //});
                            }
                            //try
                            //{
                            //    db.SaveChanges();
                            //    Console.WriteLine("Done adding countries. Please uncomment the method CreateCountries() in Global.asax.cs!!!");
                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine("Error while saving countries. Message: " + ex.Message);
                            //    Console.WriteLine(ex.ToString());
                            //}
                        }
                    }
                    Console.WriteLine((hasError ? "Failed to convert " : "Successfully converted file ") + csvFile);
                    Console.ForegroundColor = ConsoleColor.White;
                });
            }
        }

        /// <summary>
        /// Convert CSV (header required!) to JSON
        /// </summary>
        /// <param name="csvFile"></param>
        /// <param name="delimiter"></param>
        /// <param name="quotationChar"></param>
        /// <returns></returns>
        private static List<string> ConvertCSVToJSON(string csvFile, out bool hasError, char delimiter = ',', char quotationChar = '"')
        {
            hasError = false;
            var json = new CSVToJSON(new CSVReader(csvFile));
            json.JsonConvertError += (o, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Debug.WriteLine(e.Data);
                Console.ForegroundColor = ConsoleColor.White;
                //Console.WriteLine("Error: " + e.Data);
            };
            var results = json.Parse();
            //json.Save();
            return results;
        }

        private static void TestTryCatch()
        {
            DummyClass dc = new DummyClass();

            Extensions.TryCatch(() =>
            {
                Console.WriteLine(dc.DummyMethod(5, 0));
            }, (ex) =>
            {
                Console.WriteLine(ex.Message);
            }, () =>
            {
                Console.WriteLine("finally...");
            });
        }

        public class DummyClass
        {
            public double DummyMethod(double a, double b)
            {
                return a / b;
            }
        }

        static void TestConvertNotWorking()
        {
            var data = "a,\"b\",\"c\"";//,\"\"c\",\"d\"";
            bool isEscStart = false;
            bool isEscEnd = false;
            bool isEscStartInString = false;
            bool isEscEndInString = false;
            char quotationChar = '"';
            char delimiter = ',';
            var count = 0;
            var isEndOfColValue = true;
            var colVal = String.Empty;
            var results = new List<string>();
            for (int i = 0; i < data.Length; i++)
            {
                //if ((i == data.Length - 1 || data[i] == delimiter) && isEndOfColValue)
                ////if (isEndOfColValue)
                //{
                //    results.Add(colVal.Replace("\"", ""));
                //    colVal = "";
                //}
                //else
                //{
                var currentChar = data[i];
                var nextChar = currentChar;
                if (i < data.Length - 1)
                    nextChar = data[i + 1];

                // "a","b1,b2","c"
                var str1 = (char.ToString(currentChar) + char.ToString(nextChar));



                if (!isEscStart) //"
                    isEscStart = i == 0 && currentChar == quotationChar;
                if (!isEscEnd) //"
                    isEscEnd = i == data.Length && currentChar == quotationChar;
                if (!isEscStartInString) //",
                    isEscStartInString = i > 0 && i < data.Length && str1 == (char.ToString(quotationChar) + char.ToString(delimiter));
                if (!isEscEndInString) //,"
                    isEscEndInString = i > 0 && i < data.Length && str1 == (char.ToString(delimiter) + char.ToString(quotationChar));

                if (!isEscStart && nextChar == delimiter)
                {
                    isEndOfColValue = true;
                }
                else
                {
                    // "a",
                    if (isEscStart && isEscStartInString)
                    {
                        count++;
                        isEndOfColValue = true;
                        isEscStart = false;
                        isEscStartInString = false;
                    }
                    // ,"a",
                    if (isEscEndInString && isEscStartInString)
                    {
                        count++;
                        isEndOfColValue = true;
                        isEscStartInString = false;
                        isEscEndInString = false;
                    }
                    // ,"a"
                    if (isEscEndInString && isEscEnd)
                    {
                        count++;
                        isEndOfColValue = true;
                        isEscEndInString = false;
                        isEscEnd = false;
                    }
                    // ","
                    //if (isEscStartInString && isEscEnd)
                    //{
                    //    count++;
                    //    isEscStartInString = false;
                    //    isEscEnd = false;
                    //}
                }
                //isEndOfColValue = count % 2 == 0;
                if (isEndOfColValue)
                {
                    colVal += currentChar;
                    results.Add(colVal);
                    colVal = String.Empty;
                    isEndOfColValue = false;
                }
                //}
            }
            results.ForEach(r => Console.WriteLine(r));
            Console.ReadLine();
        }
    }
}
