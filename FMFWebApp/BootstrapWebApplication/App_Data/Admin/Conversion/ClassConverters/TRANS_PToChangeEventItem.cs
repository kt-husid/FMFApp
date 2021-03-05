using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.Models.Old;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public class TRANS_PToChangeEventItem
    {
        public TRANS_PToChangeEventItem(BootstrapContext db, TRANS_P o, ConverterData data)
        {
            if (!string.IsNullOrEmpty(o.FRA) || !string.IsNullOrEmpty(o.TIL))
            {
                var date = Helper.ParseDate(o.IND_DATO, o.IND_HH, o.IND_MM);
                var member = db.Members.Where(x => x.NR == o.PERSNR).FirstOrDefault();
                var entity = (member != null && member.Person != null) ? member.Person.Entity : null;
                if (entity != null)
                {
                    var tableName = "";
                    var columnName = "";
                    ChangeEventItemHelper.GetMemberTableColumnNames(o.FELT, out tableName, out columnName);

                    var type = ChangeEventType.CREATED;
                    if (o.STATUS.ToLower().Equals("r") || string.IsNullOrEmpty(o.STATUS.Trim()))
                    {
                        type = ChangeEventType.UPDATED;
                    }
                    //else if (o.STATUS.ToLower() == "")
                    //{
                    //    type = ChangeEventType.DELETED;
                    //}

                    var changeEventItem = new ChangeEventItem()
                    {
                        ChangedFrom = "" + o.FRA,
                        ChangedTo = "" + o.TIL,
                        Type = type,
                        Date = date,
                        UserIdCode = o.IND_ID,
                        ChangeEventId = null,
                        ColumnName = columnName,
                        TableName = tableName,
                        Entity = entity
                    };
                    if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(columnName))
                    {
                        db.ChangeEventItems.Add(changeEventItem);
                    }
                }
            }
        }
    }
}