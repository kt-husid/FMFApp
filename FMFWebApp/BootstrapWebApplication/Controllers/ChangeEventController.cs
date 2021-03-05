using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers
{
    public class ChangeEventController : Controller
    {
        private BootstrapContext db = new BootstrapContext();
     
        // GET: /ChangeEvent/
        public ActionResult Index()
        {
            return View(db.ChangeEvents.ToList());
        }

        // GET: /ChangeEvent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeEvent changeEvent = db.ChangeEvents.Find(id);
            if (changeEvent == null)
            {
                return HttpNotFound();
            }
            return View(changeEvent);
        }

        public ActionResult DetailsCompact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeEvent changeEvent = db.ChangeEvents.Find(id);
            if (changeEvent == null)
            {
                return HttpNotFound();
            }
            return PartialView(changeEvent);
        }

        // GET: /ChangeEvent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ChangeEvent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,CreatedOn,DeletedOn,UpdatedOn,CreatedById,DeletedById,LastUpdatedById")] ChangeEvent changeEvent)
        public ChangeEvent Create([Bind(Include = "Id,CreatedOn,DeletedOn,UpdatedOn")] ChangeEvent changeEvent)
        {
            if (ModelState.IsValid)
            {
                db.ChangeEvents.Add(changeEvent);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return changeEvent;
            //return View(changeEvent);
        }

        // GET: /ChangeEvent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeEvent changeEvent = db.ChangeEvents.Find(id);
            if (changeEvent == null)
            {
                return HttpNotFound();
            }
            return View(changeEvent);
        }

        // POST: /ChangeEvent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,CreatedOn,DeletedOn,UpdatedOn,CreatedById,DeletedById,LastUpdatedById")] ChangeEvent changeEvent)
        public ChangeEvent Edit([Bind(Include = "Id,CreatedOn,DeletedOn,UpdatedOn")] ChangeEvent changeEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(changeEvent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            //return View(changeEvent);
            return changeEvent;
        }

        // GET: /ChangeEvent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeEvent changeEvent = db.ChangeEvents.Find(id);
            if (changeEvent == null)
            {
                return HttpNotFound();
            }
            //return View(changeEvent);
            return DeleteConfirmed(changeEvent.Id);
        }

        // POST: /ChangeEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChangeEvent changeEvent = db.ChangeEvents.Find(id);
            db.ChangeEvents.Remove(changeEvent);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
