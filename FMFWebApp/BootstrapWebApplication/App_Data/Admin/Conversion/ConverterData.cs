using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion
{
    public class ConverterData
    {
        //public PersonGender genderMale { get; private set; }
        //public PersonGender genderFemale { get; private set; }
        //public PersonGender genderUnknown { get; private set; }
        //public PersonTitle titleMale { get; private set; }
        //public PersonTitle titleFemale { get; private set; }
        //public PersonTitle titleUnknown { get; private set; }

        public ChangeEventHandler ChangeEventHandler { get; private set; }

        public ConverterData()
        {
            var userIdCode = AccountHandler.Instance.User != null ? AccountHandler.Instance.User.UserIdCode : "bha";
            ChangeEventHandler = new ChangeEventHandler(userIdCode);
            
            //using (var db = new BootstrapContext())
            //{
            //    genderMale = db.PersonGenders.Where(g => g.Type.ToLower().Equals("male")).FirstOrDefault();
            //    genderFemale = db.PersonGenders.Where(g => g.Type.ToLower().Equals("female")).FirstOrDefault();
            //    genderUnknown = db.PersonGenders.Where(g => g.Type.ToLower().Equals("unknown")).FirstOrDefault();

            //    titleMale = db.PersonTitles.Where(g => g.Name.ToLower().Equals("mr.")).FirstOrDefault();
            //    titleFemale = db.PersonTitles.Where(g => g.Name.ToLower().Equals("mrs.")).FirstOrDefault();
            //    titleUnknown = db.PersonTitles.Where(g => g.Name.ToLower().Equals("unknown")).FirstOrDefault();
            //    db.Dispose();
            //}
        }
    }
}