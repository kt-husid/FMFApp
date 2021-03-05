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
    public class MemberCommentHandler : IHandler<MemberCommentViewModel,MemberComment>
    {
        public MemberCommentHandler(MemberCommentViewModel o)
        {
            Result = new MemberComment()
            {
                Id = o.Id,
                MemberId = o.MemberId,
                Text = o.Text
            };
        }
    }
}