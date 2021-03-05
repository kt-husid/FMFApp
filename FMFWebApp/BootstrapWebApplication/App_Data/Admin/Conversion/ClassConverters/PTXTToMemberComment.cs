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
    public class PTXTToMemberComment
    {
        public PTXTToMemberComment(BootstrapContext db, BootstrapWebApplication.Models.Old.PTXT o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = dateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = updatedById;//o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var member = db.Members.Where(m => m.NR == o.NR).FirstOrDefault();
            if (member != null)
            {
                var memberComment = new MemberComment()
                {
                    ChangeEvent = changeEvent,
                    Text = o.TEKST,
                    Member = member
                };
                db.MemberComments.Add(memberComment);
            }
        }
    }
}