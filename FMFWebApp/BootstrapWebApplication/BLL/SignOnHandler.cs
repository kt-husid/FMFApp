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
    public class SignOnHandler
    {
        BootstrapContext db = new BootstrapContext();

        public SignOn SignOn { get; set; }

        public SignOnHandler(SignOn signOn)
        {
            this.SignOn = signOn;
        }

        public SignOn Create(SignOn obj)
        {
            //var controller = new SignOnController();
            //controller.Create(obj);
            //return obj;
            new NotImplementedException();
            return null;
        }

        public Job GetJobWhileSignedOn(string jobCode)
        {
            return db.Jobs.Where(x => x.Code == jobCode).FirstOrDefault();
        }


        public static decimal? CalcKR_IALT(Trip trip, TripSignOnCreateEditViewModel vmObj)
        {
            //return 0.83m * vmObj.PART + 7m * vmObj.TIL_PR_DG + vmObj.TIL_PR_TUR;
            if (vmObj.From.HasValue && vmObj.To.HasValue)
            {
                return trip.CrewSharePerCrewMember + vmObj.TIL_PR_DG * vmObj.To.Value.Subtract(vmObj.From.Value).Days + vmObj.TIL_PR_TUR;
            }
            return trip.CrewSharePerCrewMember;
        }
    }
}