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
    public class LGJALD2ToMemberPayment2
    {
        public LGJALD2ToMemberPayment2(BootstrapContext db, LGJALD2 o, ConverterData data)
        {
            //var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            //var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            //var updatedById = o.RET_ID;
            //var createdById = o.OPR_ID;
            //var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var job = db.Jobs.Where(m => m.Code == o.StarvId).FirstOrDefault();
            var postal = db.Postals.Where(m => m.Code == o.PostNrPostnr).FirstOrDefault();
            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault();

            var memberPayment2 = new MemberPayment2()
            {
                //ChangeEvent = changeEvent,
                MemberPaymentNR = o.LG_NR,
                U1 = o.U1,
                U2 = o.U2,
                U3 = o.U3,
                U4 = o.U4,
                U5 = o.U5,
                U6 = o.U6,
                U7 = o.U7,
                Job = job,
                Postal = postal,
                Member = member
            };
            db.MemberPayments2.Add(memberPayment2);
        }
    }
}