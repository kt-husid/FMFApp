using BootstrapWebApplication.Controllers;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class BankAccountHandler : IHandler<MemberBankAccountViewModel, BankAccount>
    {
        public BankAccountHandler(MemberBankAccountViewModel obj)
        {
            Result = new BankAccount()
            {
                //BankId = obj.BankId,
                //EntityId = obj.EntityId,
                AccountNumber = obj.AccountNumber,
                IsPrimary = obj.IsPrimary
            };
        }
    }
}