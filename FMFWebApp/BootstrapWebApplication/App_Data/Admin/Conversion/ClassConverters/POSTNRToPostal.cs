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
    public class POSTNRToPostal //: IDbConverter<POSTNR>
    {
        //public ConverterData data { get; set; }

        //public POSTNRToPostal(ConverterData _data)
        //{
        //    this.data = _data;
        //}

        public POSTNRToPostal(BootstrapContext db, POSTNR o, ConverterData data)
        //public void Convert(BootstrapContext db, POSTNR o)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var postal = new Postal()
            {
                Id = o.Id,
                ChangeEvent = changeEvent,
                Code = System.Convert.ToInt32(o.PostalCode),
                CityName = o.BYGD,
                //BYKEY = o.BYKEY,
                Amount = o.ANTAL,
                CountryCode = o.LAND,
                //ANT_K = o.ANT_K,
                //ANT_M = o.ANT_M,
                //ANT_X = o.ANT_X,
                //KOMNR = o.KOMNR,
                OKI = o.OKI,
                FAX_SP = o.FAX_SP,
                FAX_FB = o.FAX_FB,
                FAX_SJ = o.FAX_SJ,
                FAX_F = o.FAX_F
            };
            db.Postals.Add(postal);
        }
    }
}