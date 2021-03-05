using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class PersonGenderHandler
    {
        public static void TryCreate()
        {
            var db = new BootstrapContext();
            if (db.PersonGenders.Count() == 0)
            {

                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "M" // Male
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "F" // Female
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "U" // Unknown
                });
            }
            db.SaveChanges();
            db.Dispose();
        }
    }
}