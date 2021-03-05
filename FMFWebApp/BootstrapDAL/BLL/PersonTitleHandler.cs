using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class PersonTitleHandler
    {
        public static void TryCreate()
        {
            var db = new BootstrapContext();
            if (db.PersonTitles.Count() == 0)
            {

                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mr",
                    Description = "Mr."
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mrs",
                    Description = "Mrs."
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "U",
                    Description = "Unknown"
                });
            }
            db.SaveChanges();
            db.Dispose();
        }
    }
}