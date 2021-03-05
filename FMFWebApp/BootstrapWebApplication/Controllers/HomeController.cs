using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BootstrapWebApplication.Controllers
{
    //[RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {   
        [HttpGet]
        public ActionResult KeyboardShortcut()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Member");
            //return View();
        }

        public ActionResult TestMail()
        {
            ViewBag.MSG = "OK";
            try
            {
                var subject = "Test Mail";
                var body = "Hetta er bert ein roynd";
                var rcptList = new List<string>() { "benjamin@kthusid.com", "hammerbenjamin@gmail.com" };
                var msg = new MailMessage();
                msg.From = new MailAddress("noreply@bustadir.fo");
                foreach (var rcpt in rcptList)
                {
                    msg.To.Add(rcpt);
                }
                //if (files != null)
                //{
                //    foreach (var file in files)
                //    {
                //        if (file != null)
                //        {
                //            msg.Attachments.Add(new Attachment(file.FullName, MediaTypeNames.Application.Pdf));
                //        }
                //    }
                //}
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body;
                var smtp = new SmtpClient("192.168.100.23", 25);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //var smtp = new SmtpClient();
                //smtp.EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableSSLOnMail"]);

                //smtp.UseDefaultCredentials = true;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //var credentials = new System.Net.NetworkCredential("YourSMTPServerUserName", "YourSMTPServerPassword");
                //smtp.Send("noreply@bustadir.fo", "benjamin@kthusid.com", "test 2", "body 2" );
                smtp.Send(msg);

            }
            catch (Exception ex)
            {
                ViewBag.MSG = ex.Message;
            }
            return View();
        }

        public ActionResult Apitester()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BrowserInfo()
        {
            return View();
        }

        public ActionResult Mobile()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = Kthusid.Helpers.CultureHelper.Instance.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies.Get("_culture");
            if (cookie != null)
            {
                //cookie.Expires = DateTime.UtcNow.AddYears(-1);
                cookie.Value = culture;   // update cookie value
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Shareable = true;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            var returnTo = Request.UrlReferrer;
            if (returnTo == null)
                return Redirect("/");
            return Redirect(returnTo.ToString());
        }
    }
}