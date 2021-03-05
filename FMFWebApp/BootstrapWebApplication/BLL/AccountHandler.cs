using BootstrapWebApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class AccountHandler
    {
        private static AccountHandler _instance = null;
        public static AccountHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountHandler();
                }
                return _instance;
            }
        }
        private AccountHandler() { }

        public ApplicationUser Initialize(string userName)
        {
            if (userName != null)// && User == null)
            {
                var store = new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                //ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
                //User = userManager.FindById(userId);
                User = userManager.FindByName(userName);
                return User;
            }
            return null;
        }

        public ApplicationUser User { get; private set; }
    }
}