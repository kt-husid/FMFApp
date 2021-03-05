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
    public class DEB_CONConvert
    {
        public DEB_CONConvert(BootstrapContext db, BootstrapWebApplication.Models.Old.DEB_CON o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;// o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var job = db.Jobs.Where(m => m.Code == o.StarvId).FirstOrDefault();
            var postal = db.Postals.Where(m => m.Code == o.PostNrPostnr).FirstOrDefault();
            var memberType = db.MemberTypes.Where(m => m.Code == o.LslagId).FirstOrDefault();
            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault();

            var deb_con = new BootstrapWebApplication.Models.DEB_CON()
            {
                ChangeEvent = changeEvent,
                NR = o.NR,
                NR_DEB = o.NR_DEB,
                SALDO = o.SALDO,
                Job = job,
                Postal = postal,
                MemberType = memberType,
                Member = member
            };
            db.DEB_CONs.Add(deb_con);
        }
    }
}