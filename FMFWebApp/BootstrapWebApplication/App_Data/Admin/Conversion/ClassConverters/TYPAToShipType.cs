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
    public class TYPAToShipType //: IDbConverter<TYPA>
    {
        //public ConverterData data { get; set; }

        //public TYPAToType(ConverterData _data)
        //{
        //    this.data = _data;
        //}

        //ID,TypaCode,TEKSTUR,STUTT,RET_DATO,RET_HH,RET_MM,RET_ID,BREV_UT,FLYTAST,DLISTA,BLISTA,ARS_LISTI,TIL_SKIP;TIL_MANN
        public TYPAToShipType(BootstrapContext db, TYPA o, ConverterData data)
        //public void Convert(BootstrapContext db, TYPA o)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var type = new ShipType()
            {
                ChangeEvent = changeEvent,
                Code = o.TypaCode,
                Description = o.TEKSTUR,
                //STUTT = o.STUTT,
                YearList = o.ARS_LISTI.ToLower().Equals("j"),
                PctToShip = o.TIL_SKIP.Value,
                PctToCrewMember = o.TIL_MANN.Value
            };
            db.ShipTypes.Add(type);
        }
    }
}