using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public static class ETransHandler
    {
        public static ICollection<ETrans> Parse(string file)
        {
            //var regNumbers = new List<int>();
            //regNumbers.Add(6460); // BankNordik
            //regNumbers.Add(9909); // Postgirostovan
            //regNumbers.Add(9181); // Eik
            //regNumbers.Add(9865); // Norðoya Sparikassa
            //regNumbers.Add(9870); // Suðuroya Sparikassa
            var etransList = new List<ETrans>();
            string[] fileData = null;
            try
            {
                fileData = System.IO.File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, file), Encoding.Default);
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't read the file!" + Environment.NewLine + "Message: " + ex.Message);
            }
            foreach (var str in fileData)
            {
                var birthdayString = str.Substring(0, 6);
                DateTime birthday = DateTime.Now;
                if (!DateTime.TryParseExact(birthdayString, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    // if a member has moved from Faroe Islands to another country like Denmark, then elektron (www.elektron.fo) who 
                    // creates etrans files, adds a fictive number '6' to the first part of the birthday. E.g. 031158 becomes 631158.
                    var birthdayString2 = (int.Parse(birthdayString.Substring(0, 1)) - 6) + birthdayString.Substring(1, 5);
                    if(!DateTime.TryParseExact(birthdayString2, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                    {
                        throw new Exception("Couldn't parse the birthday!");
                    }
                }

                var etrans = new ETrans()
                {
                    Id = etransList.Count() + 1,
                    Birthday = birthday,
                    ETransDate = DateTime.ParseExact(str.Substring(6, 8), "dMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    EmployerNumber = int.Parse(str.Substring(14, 6)),
                    BankRegNumber = int.Parse(str.Substring(20, 4)),
                    BankAccount = int.Parse(str.Substring(24, 7)),
                    BankRegNumberFF = int.Parse(str.Substring(31, 4)),
                    BankAccountFF = int.Parse(str.Substring(35, 7)),
                    TransferDate = DateTime.ParseExact(str.Substring(42, 8), "yyyyMd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    HolidayPayAmount = decimal.Parse(str.Substring(51, 11).Trim()), // don't process from index 50 which can contain the minus char '-' used for IsReverse ('R')
                    ART = str.Substring(62, 1).Trim(),
                    IsReverse = str.Substring(62, 1).Trim().ToUpper() == "R",
                    FirstName = str.Substring(63, 30).Trim().ToUpperEachFirst(),
                    LastName = str.Substring(93, 30).Trim().ToUpperEachFirst(),
                    AddressLine1 = str.Substring(123, 30).Trim().ToUpperEachFirst(),
                    PostalCode = int.Parse(str.Substring(153, 6).Trim()),
                    Gender = str.Substring(159, 1).Trim()
                };
                etransList.Add(etrans);
            }
            return etransList;
        }
    }
}