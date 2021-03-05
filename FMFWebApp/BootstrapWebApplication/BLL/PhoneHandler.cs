using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapWebApplication.BLL
{
    public class PhoneHandler
    {
        private static PhoneHandler _instance = null;
        public static PhoneHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhoneHandler();
                }
                return _instance;
            }
        }

        private PhoneHandler() { }

        public Phone Parse(string rawNumber, string defaultRegion = "FO")
        {
            if (string.IsNullOrEmpty(rawNumber) || rawNumber.Trim().Length < 4)
            {
                return null;
            }

            Phone phone = new Phone();
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumberParsed = phoneUtil.Parse(rawNumber, defaultRegion);
                phone.RawNumber = rawNumber;
                phone.CountryCode = phoneNumberParsed.CountryCode;
                phone.Extension = phoneNumberParsed.Extension;
                phone.Number = phoneNumberParsed.NationalNumber.ToString();
                //Console.WriteLine("Extension: " + phoneNumber.Extension);
                //Console.WriteLine("Country code: " + phoneNumber.CountryCode);
                //Console.WriteLine("NationalNumber: " + phoneNumber.NationalNumber);
            }
            catch (NumberParseException e)
            {
                Debug.WriteLine("NumberParseException was thrown: " + e.ToString());
            }
            return phone;
        }

        //public Phone Parse(Phone obj, string defaultRegion = "FO")
        //{
        //    if (obj == null)
        //    {
        //        return null;
        //    }

        //    Phone phone = obj;
        //    PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        //    try
        //    {
        //        var phoneNumberParsed = phoneUtil.Parse(obj.RawNumber, defaultRegion);
        //        phone.RawNumber = obj.RawNumber;
        //        phone.CountryCode = phoneNumberParsed.CountryCode;
        //        phone.Extension = phoneNumberParsed.Extension;
        //        phone.Number = phoneNumberParsed.NationalNumber.ToString();
        //        //Console.WriteLine("Extension: " + phoneNumber.Extension);
        //        //Console.WriteLine("Country code: " + phoneNumber.CountryCode);
        //        //Console.WriteLine("NationalNumber: " + phoneNumber.NationalNumber);
        //    }
        //    catch (NumberParseException e)
        //    {
        //        Debug.WriteLine("NumberParseException was thrown: " + e.ToString());
        //    }
        //    return phone;
        //}

        //public Phone UpdateIsPrimary(BootstrapContext db, Phone obj)
        //{
        //    /**
        //     * if editing a phone which is currently IsPrimary and has been set to IsPrimary = false,
        //     * then set IsPrimary = true 
        //     * */
        //    if (obj != null)
        //    {
        //        var primaryPhones = db.Phones.Where(p => p.IsPrimary);
        //        // There should only be 0 or 1 primary phone. If there's more than 1, then it's an error.
        //        var count = primaryPhones.Count();
        //        //System.Diagnostics.Debug.Assert(count == 0 || count == 1);
        //        var oldPrimaryPhone = primaryPhones.FirstOrDefault();
        //        if (oldPrimaryPhone != null)
        //        {
        //            // There's already 1 phone which is set as primary.
        //            // Change IsPrimary on both phones, but...
        //            // If this phone is the same as the one being edited then
        //            // set the other 
        //            if (obj.Id == oldPrimaryPhone.Id && oldPrimaryPhone.IsPrimary)
        //            {
        //                obj.IsPrimary = true; // Make sure it's still set to TRUE
        //            }
        //            else
        //            {
        //                // otherwise continue updating
        //                oldPrimaryPhone.IsPrimary = false;
        //                //db.Entry(oldPrimaryPhone).State = System.Data.Entity.EntityState.Modified;
        //                //db.SaveChanges();
        //            }
        //        }
        //    }
        //    return obj;
        //}

        public Phone CreatePhone<TParent>(TParent parent, PhoneCreateOrEditViewModel vmObj)
            where TParent : IHasStuff
        {
            var dbObj = PhoneHandler.Instance.Parse(vmObj.PhoneNumber);

            var primaryPhones = parent.Phones.FilterDeleted().Where(x => x.IsPrimary);
            if (primaryPhones.Count() == 0)
            {
                // if there's no primary phone, then set the new one as primary
                dbObj.IsPrimary = true;
            }
            else
            {
                // if there's a primary phone and the new one is set to primary, then
                // set the new one to be primary and old to be not
                // AND if the new phone is set to primary
                if (vmObj.IsPrimary)
                {
                    foreach (var primaryPhone in primaryPhones)
                    {
                        primaryPhone.IsPrimary = false;
                    }
                    dbObj.IsPrimary = true;
                }
            }
            return dbObj;
        }

        internal Phone UpdatePhone<TParent>(TParent parent, PhoneCreateOrEditViewModel vmObj)
            where TParent : IHasStuff
        {

            //Convert the ViewModel to DB Object (Model)
            var dbObj = parent.Phones.Where(x => x.Id == vmObj.Id).FirstOrDefault();
            var newObj = PhoneHandler.Instance.Parse(vmObj.PhoneNumber);
            dbObj.Number = newObj.Number;
            dbObj.AreaCode = newObj.AreaCode;
            dbObj.CountryCode = newObj.CountryCode;
            dbObj.IsPrimary = newObj.IsPrimary;
            dbObj.RawNumber = newObj.RawNumber;
            var primaryPhones = parent.Phones.FilterDeleted().Where(x => x.IsPrimary);
            //if (primaryPhone != null && vmObj.IsPrimary)
            //{
            //    primaryPhone.IsPrimary = false;
            //    dbObj.IsPrimary = true;
            //}
            if (primaryPhones.Count() == 0)
            {
                // if there's no primary phone, then set the new one as primary
                dbObj.IsPrimary = true;
            }
            else
            {
                // if there's a primary phone and the new one is set to primary, then
                // set the new one to be primary and old to be not
                // AND the primaryPhone isn't the same object as being edited
                foreach (var primaryPhone in primaryPhones)
                {
                    primaryPhone.IsPrimary = false;
                    //if (primaryPhone.Id != dbObj.Id)
                    //{
                    //}
                }
                dbObj.IsPrimary = true;
            }
            return dbObj;
        }

        /// <summary>
        /// Returns TRUE if phone can be deleted (if it's not primary)
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="vmObj"></param>
        /// <returns></returns>
        public bool CheckIfCanDelete<TParent>(TParent parent, PhoneViewModel vmObj)
            where TParent : IHasStuff
        {
            var dbObj = parent.Phones.Where(x => x.Id == vmObj.Id).FirstOrDefault();
            return dbObj != null && !dbObj.IsPrimary;
            //var secudaryPhone = parent.Phones.Where(x => !x.IsPrimary).FirstOrDefault();
            //if (secudaryPhone != null && primaryPhone != null && primaryPhone.Id == vmObj.Id)
            //{
            //    secudaryPhone.IsPrimary = true;
            //}
        }

        public IEnumerable<PhoneViewModel> GetPhones<TParent>(TParent parent)
            where TParent : IHasStuff
        {
            return parent.Phones.FilterDeleted().Select(m => new PhoneViewModel()
            {
                Id = m.Id,
                ParentId = parent.Id,
                AreaCode = m.AreaCode,
                Country = null,// db.Countries.Where(x => x.CountryCode.ToLower() == Convert.ToString(m.CountryCode).ToLower()).FirstOrDefault(),
                CountryCode = m.CountryCode,
                IsPrimary = m.IsPrimary,
                PhoneNumber = m.RawNumber
            });
        }
    }
}