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
    public class LSLAGToMemberType
    {
        public LSLAGToMemberType(BootstrapContext db, LSLAG o, ConverterData data)
        {
            //var db = new BootstrapContext();
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;// o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            //OLD_ID,ID,TEKSTUR,STUTT,RET_DATO,RET_HH,RET_MM,RET_ID,BREV_UT,FLYTAST,DLISTA,BLISTA,ARS_LISTI

            var memberType = new MemberType()
            {
                ChangeEvent = changeEvent,
                Code = o.Code,
                Description = o.TEKSTUR,
                //STUTT = o.STUTT,
                BREV_UT = o.BREV_UT
                //FLYTAST = o.FLYTAST,
                //DLISTA = o.DLISTA,
                //BLISTA = o.BLISTA,
                //ARS_LISTI = o.ARS_LISTI
            };
            db.MemberTypes.Add(memberType);
            //db.SaveChanges();
            //db.Dispose();
        }
    }
}