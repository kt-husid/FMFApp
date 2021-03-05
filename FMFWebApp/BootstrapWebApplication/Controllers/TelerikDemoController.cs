using BootstrapWebApplication.DAL;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BootstrapWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BootstrapWebApplication.Controllers
{
    public class TelerikDemoController : Controller<Member>
    {
        public ActionResult AutoComplete() { return View(); }

        #region FileUpload
        public ActionResult FileUpload() { return View(); }

        public ActionResult Submit(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                TempData["UploadedFiles"] = GetFileInfo(files);
            }

            return RedirectToAction("FileUploadResult");
        }

        public ActionResult FileUploadResult()
        {
            return View();
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }
        #endregion

        protected override DataSourceResult Get(int id, DataSourceRequest request)
        {
            // Avoid the circular reference by creating a View Model object and skiping the Customer property
            var result = db.Members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                FullName = (m.Person != null) ? m.Person.FullName : "",
                Birthday = (m.Person != null) ? m.Person.Birthday : null,
                MemberType = (m.MemberType != null) ? m.MemberType.Description : "",
                JobTitle = m.JobTitle,
                PRI_OWN = m.PRI_OWN,
                BETALER = m.BETALER,
                M_STATUS = m.M_STATUS
            });
            return result.ToDataSourceResult(request);
        }

        protected override IQueryable<Member> PagedListFilter(IQueryable<Member> list, string filterName = null)
        {
            throw new NotImplementedException();
        }

        protected override void AddViewBag(Member obj)
        {

        }
    }
}