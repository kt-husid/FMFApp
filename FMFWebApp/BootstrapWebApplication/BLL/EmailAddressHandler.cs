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
    public class EmailAddressHandler
    {
        private static EmailAddressHandler _instance = null;
        public static EmailAddressHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmailAddressHandler();
                }
                return _instance;
            }
        }

        private EmailAddressHandler() { }

        /// <summary>
        /// Returns TRUE if address can be deleted (if it's not primary)
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="vmObj"></param>
        /// <returns></returns>
        public bool CheckIfCanDelete<TParent>(TParent parent, EmailAddressViewModel vmObj)
            where TParent : IHasStuff
        {
            var dbObj = parent.EmailAddresses.Where(x => x.Id == vmObj.Id).FirstOrDefault();
            return dbObj != null && !dbObj.IsPrimary;
        }
    }
}