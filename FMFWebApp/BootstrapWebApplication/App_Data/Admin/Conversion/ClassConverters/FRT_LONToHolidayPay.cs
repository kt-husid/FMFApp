using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.Models.Old;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public class FRT_LONToHolidayPay
    {
        public FRT_LONToHolidayPay(BootstrapContext db, FRT_LON o, ConverterData data)
        {
            //ID,NR,AR,FRTLON,TRSP,FRT_DATO,TRS_DATO,LO_KR_A,LO_KR_F,LO_KR_FRT,LO_DATO,DG_AR,TU_AR,DATO_AR,ID_AR,SAML_KR,SAML_KR2
            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault(); //OK
            var holidayPay = new HolidayPay()
            {
                Year = o.AR,
                Amount = o.FRTLON,
                TRSP = o.TRSP,
                FRT_DATO = o.FRT_DATO,
                TRS_DATO = o.TRS_DATO,
                LO_KR_A = o.LO_KR_A,
                LO_KR_F = o.LO_KR_F,
                LO_KR_FRT = o.LO_KR_FRT,
                LO_DATO = o.LO_DATO,
                DG_AR = o.DG_AR,
                TU_AR = o.TU_AR,
                DATO_AR = o.DATO_AR,
                ID_AR = o.ID_AR,
                SAML_KR = o.SAML_KR,
                SAML_KR2 = o.SAML_KR2,
                Member = member
            };
            db.HolidayPays.Add(holidayPay);
        }
    }
}