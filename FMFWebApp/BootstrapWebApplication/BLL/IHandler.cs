using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapWebApplication.BLL
{
    public class IHandler<S_ViewModelInput, T_ModelOuput>
    {
        public T_ModelOuput Result { get; set; }

        //public IHandler(S o) { }

        protected string GetUserId()
        {
            return AccountHandler.Instance.User.UserIdCode;
        }

    }
}
