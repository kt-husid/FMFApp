using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public static class CountryHandler
    {
        public static void TryCreate(string jsonFile)
        {
            JsonSerializer serializer = new JsonSerializer();
            var countries = serializer.Deserialize<List<TempCountry>>(new JsonTextReader(new StreamReader(jsonFile)));
            var changeEventHandler = new ChangeEventHandler("bha");
            using (var db = new BootstrapContext())
            {
                if (db.Countries.Count() == countries.Count) return;

                foreach (var country in countries)
                {
                    var changeEvent = changeEventHandler.Create(db);
                    db.ChangeEvents.Add(changeEvent);

                    db.Countries.Add(new Country()
                    {
                        Code = country.Alpha2,
                        Name = country.Name,
                        ChangeEvent = changeEvent
                    });
                    Console.WriteLine("Added country " + country.Name);
                }
                try
                {
                    db.SaveChanges();
                    Console.WriteLine("Done adding countries. Please uncomment the method CreateCountries() in Global.asax.cs!!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while saving countries. Message: " + ex.Message);
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        internal class TempCountry
        {
            public string Name { get; set; }
            public string Alpha2 { get; set; }
            public string Alpha3 { get; set; }
            public string CountryCode { get; set; }
            public string ISO31662 { get; set; }
            public string RegionCode { get; set; }
            public string SubRegionCode { get; set; }
        }
    }
}