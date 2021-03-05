using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kthusid.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BootstrapWebApplication.Controllers
{
    [Authorize]
    public abstract class Controller<T> : System.Web.Mvc.Controller where T : class
    {
        protected BootstrapContext ResetContext(BootstrapContext db, int count, int _commitCount, bool recreateContext)
        {
            if (count % _commitCount == 0)
            {
                db.SaveChanges();
                if (recreateContext)
                {
                    db.Dispose();
                    db = new BootstrapContext();
                    //context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return db;
        }

        private Expression<Func<T, Int32>> SortExpression(string propertyName)
        {
            var param = Expression.Parameter(typeof(T));
            var exp = Expression.Property(param, propertyName);
            var sortExpression = Expression.Lambda<Func<T, Int32>>(Expression.Convert(exp, typeof(Int32)), param);
            return sortExpression;
        }

        protected virtual IOrderedQueryable<T> PagedListSort(IQueryable<T> list, string sortName = "", string sortType = "asc")
        {
            Expression<Func<T, Int32>> sortExpression = null;
            sortName = "Id";
            sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
            sortExpression = SortExpression(sortName);// System.Extensions.CreatePropSelectorExpression<T, string>(sortName);
            switch (sortType.ToLower())
            {
                case "asc":
                    return list.OrderBy(sortExpression);
                default:
                    return list.OrderByDescending(sortExpression);

            }
        }

        protected BootstrapContext db = new BootstrapContext();

        public BootstrapContext Db { get { return db; } }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cultureName = CookieHelper.GetCultureName(Request);
            var cultureInfo = new System.Globalization.CultureInfo(cultureName);
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return base.BeginExecuteCore(callback, state);
        }

        protected IPagedList<T> GetPagedList(string sortName = "", string sortType = "asc", string filterName = null, int? page = null)
        {
            // Select
            IQueryable<T> list = PagedListSelect();

            // Search & Filter
            if (filterName != null)
            {
                page = 1;
            }
            else
            {
                filterName = "";// currentFilter;
            }
            //ViewBag.CurrentFilter = filterName;
            if (!String.IsNullOrEmpty(filterName))
            {
                list = PagedListFilter(list, filterName);
            }

            //try
            //{
            //    list = list.OfType<IHasChangeEvent>().Where(s => s.ChangeEvent.IsDeleted == false).Cast<T>();
            //}
            //catch (Exception ex) { }

            // Sort
            list = PagedListSort(list, sortName, sortType);
            //sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
            //list = new GenericSorter<T>().Sort(list, sortName, sortType).AsQueryable();

            // todo: pageSize should be dynamic!
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return list.ToPagedList(pageNumber, pageSize);
        }

        protected abstract IQueryable<T> PagedListFilter(IQueryable<T> list, string filterName = null);

        //protected abstract IQueryable<T> PagedListSelect();
        protected virtual IQueryable<T> PagedListSelect()
        {
            return db.Set<T>();
        }

        protected ActionResult GetRelatedDataForParent<TParent>(int? id, string viewName)
            where TParent : class
        {
            TParent obj = db.Set<TParent>().Find(id);
            if (obj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(viewName, obj);
        }

        protected ActionResult GetRelatedData(int? id, string viewName)
        {
            T obj = Find(id);
            if (obj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(viewName, obj);
        }

        protected async Task<ActionResult> GetRelatedDataAsync(int? id, string viewName)
        {
            T obj = await FindAsync(id);
            if (obj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(viewName, obj);
        }

        //protected abstract T Find(int? id);

        protected T Find(int? id)
        {
            return db.Set<T>().Find(id);
        }

        protected async Task<T> FindAsync(int? id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        //protected abstract void Remove(T obj);

        //protected abstract void Add(T obj);

        protected virtual void Add(T obj)
        {
            db.Set<T>().Add(obj);
        }

        protected virtual void Remove(T obj)
        {
            db.Set<T>().Remove(obj);
        }

        protected abstract void AddViewBag(T obj);

        /// <summary>
        /// Implement functionality that's called before any other code in the Create (GET) method.
        /// </summary>
        protected virtual void CreateGetHook()
        {

        }

        protected virtual void EditGetHook()
        {

        }

        protected virtual void DeleteGetHook()
        {

        }

        protected virtual T DetailsGetHook(T obj)
        {
            return obj;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        ////public ActionResult List(string sortName = "", string sortType = "asc", string currentFilter = "", string filterName = null, int? page = null)
        //public ActionResult List(string sortName = "", string sortType = "asc", string filterName = null, int? page = null)
        //{
        //    //return PartialView(GetPagedList(sortName, sortType, currentFilter, filterName, page));
        //    return PartialView(GetPagedList(sortName, sortType, filterName, page));
        //}

        ////public ActionResult Index(string sortName = "", string sortType = "asc", string currentFilter = "", string filterName = null, int? page = null)
        //public ActionResult Index(string sortName = "", string sortType = "asc", string filterName = null, int? page = null)
        //{
        //    //return View(GetPagedList(sortName, sortType, currentFilter, filterName, page));
        //    return View(GetPagedList(sortName, sortType, filterName, page));
        //}

        ////public ActionResult IndexJson(string sortName = "", string sortType = "asc", string currentFilter = "", string filterName = null, int? page = null)
        //public ActionResult IndexJson(string sortName = "", string sortType = "asc", string filterName = null, int? page = null)
        //{
        //    //return Json(GetPagedList(sortName, sortType, currentFilter, filterName, page), JsonRequestBehavior.AllowGet);
        //    return Json(GetPagedList(sortName, sortType, filterName, page), JsonRequestBehavior.AllowGet);
        //}

        // GET: /{Controller}/Details/{id}
        //public async Task<ActionResult> Details(int? id, bool isPartial = false)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    T obj = await FindAsync(id);

        //    if (obj == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    obj = DetailsGetHook(obj);
        //    if (Request.IsAjaxRequest())
        //        return PartialView(obj);

        //    return View(obj);
        //}

        public virtual ActionResult Details(int? id, string type = null)
        {
            return DetailsHandler<T>(id, null, type);
        }

        protected ActionResult DetailsHandler<TViewModel>(int? id, Func<T, TViewModel> convertAction, string type = null) where TViewModel : class
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T obj = Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            obj = DetailsGetHook(obj);
            if (convertAction == null)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    if (type.ToLower().Equals("json"))
                    {
                        return Content(JsonConvert.SerializeObject(obj, Formatting.None));
                    }
                }
                if (Request.IsAjaxRequest())
                    return PartialView(obj);

                return View(obj);
            }
            TViewModel vmObj = convertAction(obj);
            if (!string.IsNullOrEmpty(type))
            {
                if (type.ToLower().Equals("json")) {
                    return Content(JsonConvert.SerializeObject(vmObj, Formatting.None));
                }
            }
            if (Request.IsAjaxRequest())
                return PartialView(vmObj);

            return View(vmObj);
        }

        // GET: /{Controller}/Create
        public ActionResult Create<TParent>(string viewName, IViewModelBase viewModel = null, TParent obj = null) where TParent : class
        {
            CreateGetHook();
            //AddViewBag<TParent>(obj);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, viewModel);

            return View(viewName, viewModel);
        }

        // GET: /{Controller}/Edit/{parentObjId}/{relObjId}
        public ActionResult Edit<TParent>(string viewName, IViewModelBase viewModel, TParent obj = null) where TParent : class
        {
            //if (parentId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //T obj = Find(parentId);
            //TRelObj relObj = db.Set<TRelObj>().Where(relObj);
            //if (obj == null || relObj == null)
            //{
            //    return HttpNotFound();
            //}
            EditGetHook();
            //AddViewBag(obj);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, viewModel);

            return View(viewName, viewModel);
        }

        // GET: /{Controller}/Delete/{parentObjId}/{relObjId}
        public ActionResult Delete<TObj>(string viewName, TObj obj, T parent = null)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //T obj = await FindAsync(id);
            //if (obj == null)
            //{
            //    return HttpNotFound();
            //}
            DeleteGetHook();
            if (Request.IsAjaxRequest())
                return PartialView(viewName, obj);

            return View(viewName, obj);
        }

        //// POST: /{Controller}/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult Edit(T obj)
        //{
        //    try
        //    {
        //        if (Update(obj))
        //        {
        //            if (Request.IsAjaxRequest())
        //                return Json(obj);
        //            return RedirectToAction("Index");
        //        }
        //        //if (ModelState.IsValid)
        //        //{
        //        //    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //        //    db.SaveChanges();
        //        //    if (Request.IsAjaxRequest())
        //        //        return Json(obj);
        //        //    return RedirectToAction("Index");
        //        //}
        //    }
        //    catch (RetryLimitExceededException ex)
        //    {
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    AddViewBag(obj);
        //    if (Request.IsAjaxRequest())
        //        return PartialView(obj);

        //    return View(obj);
        //}

        protected abstract DataSourceResult Get(int id, DataSourceRequest request);

        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request, int? id = null)
        {
            return Json(Get(id.HasValue ? id.Value : -1, request));
        }

        public bool Update<T>(T obj) where T : class
        {
            if (ModelState.IsValid)
            {
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
            return false;
        }

        // GET: /{Controller}/Delete/{id}
        //public async Task<ActionResult> Delete(int? id)//, bool isPartial = false)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    T obj = await FindAsync(id);
        //    if (obj == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    DeleteGetHook();
        //    //ViewBag.IsPartial = isPartial;
        //    if (Request.IsAjaxRequest())
        //        return PartialView(obj);

        //    return View(obj);
        //}

        // POST: {Controller}/Delete/{id}/[parentId]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> Delete(int id)
        public virtual ActionResult DeleteObject<TObject, TParent>(int id, int? parentId = null, ActionResult onError = null)
            where TObject : class
            where TParent : class
        {
            //T obj = await FindAsync(id);
            //if (obj != null)
            //{
            //    db.Entry(obj).State = EntityState.Modified;
            //    try
            //    {
            //        var ce = obj as IHasChangeEvent;
            //        new ChangeEventHandler(GetUserIdCode()).Delete(db, ce.ChangeEventId);
            //    }
            //    catch (Exception)
            //    {
            //        //Remove(obj);   
            //    }
            //    db.SaveChanges();
            //}
            //if (Request.IsAjaxRequest())
            //    return Json(obj);

            //return RedirectToAction("Index");
            TObject dbObj = null;
            TParent parent = db.Set<TParent>().Find(parentId);
            try
            {
                // Todo: BankAccount_Update works, but MemberComment_Updated doesn't (somehow). Debug this!!! ( this check is important)
                if (ModelState.IsValid)
                {
                    dbObj = db.Set<TObject>().Find(id);
                    if (dbObj != null)
                    {
                        //if (parent != null)
                        //{
                        // Create a changeEvent object
                        DeleteChangeEvent(dbObj, parent);
                        // Attach an entity
                        AddEntityToRelatedObject(dbObj, parent);
                        //// Attach the entity
                        //db.Set<TObject>().Attach(dbObj);
                        // Set object state to modified
                        db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;
                        // Save changes to the DB
                        db.SaveChanges();
                        // Return JSON object if the request is AJAX
                        if (Request.IsAjaxRequest())
                        {
                            return Json(true);
                        }
                        // Otherwise redirect
                        return RedirectToAction("Index");
                        //}
                    }
                    if (onError == null)
                    {
                        return Json(HttpStatusCode.NotFound);
                    }
                    return onError;
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //AddViewBag(parent);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(dbObj);
            }
            return View(dbObj);
        }

        //// POST: {Controller}/Delete/{id}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult Delete<TObject>(int id)
        //    where TObject : class
        //{
        //    TObject dbObj = null;
        //    try
        //    {
        //        // Todo: BankAccount_Update works, but MemberComment_Updated doesn't (somehow). Debug this!!! ( this check is important)
        //        if (ModelState.IsValid)
        //        {
        //            dbObj = db.Set<TObject>().Find(id);
        //            // Set the ChangeEvent object to deleted state
        //            DeleteChangeEvent(dbObj);
        //            // Set object state to modified
        //            db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;
        //            // Save changes to the DB
        //            db.SaveChanges();
        //            // Return JSON object if the request is AJAX
        //            if (Request.IsAjaxRequest())
        //            {
        //                return Json(true);
        //            }
        //            // Otherwise redirect
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (RetryLimitExceededException ex)
        //    {
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    //AddViewBag(parent);
        //    if (Request != null && Request.IsAjaxRequest())
        //    {
        //        return PartialView(dbObj);
        //    }
        //    return View(dbObj);
        //}


        /*protected ActionResult DeleteRelatedObject<TResult>(int? parentId, TResult dbObj) where TResult : class
        {
            T parent = null;
            try
            {
                // Todo: BankAccount_Update works, but MemberComment_Updated doesn't (somehow). Debug this!!! ( this check is important)
                if (ModelState.IsValid)
                {
                    parent = Find(parentId);
                    if (parent != null)
                    {
                        // Create a changeEvent object
                        DeleteChangeEvent(dbObj);
                        // Attach an entity
                        AddEntityToRelatedObject(dbObj, parent);
                        //// Attach the entity
                        //db.Set<TResult>().Attach(dbObj);
                        // Set object state to modified
                        db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;
                        // Save changes to the DB
                        db.SaveChanges();
                        // Return JSON object if the request is AJAX
                        if (Request.IsAjaxRequest())
                        {
                            return Json(true);
                        }
                        // Otherwise redirect
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //AddViewBag(parent);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(dbObj);
            }
            return View(dbObj);
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ChangeEvent CreateChangeEvent(string userIdCode = null)
        {
            userIdCode = string.IsNullOrEmpty(userIdCode) ? GetUserIdCode() : userIdCode;
            return new ChangeEventHandler(userIdCode).Create(db);
        }

        protected ChangeEvent UpdateChangeEvent(ChangeEvent changeEvent, string userIdCode = null)
        {
            userIdCode = string.IsNullOrEmpty(userIdCode) ? GetUserIdCode() : userIdCode;
            return new ChangeEventHandler(userIdCode).Update(db, changeEvent);
        }
        protected ChangeEvent UpdateChangeEvent(int? changeEventId, string userIdCode = null)
        {
            userIdCode = string.IsNullOrEmpty(userIdCode) ? GetUserIdCode() : userIdCode;
            return new ChangeEventHandler(userIdCode).Update(db, changeEventId);
        }

        protected ChangeEvent DeleteChangeEvent(ChangeEvent changeEvent, string userIdCode = null)
        {
            userIdCode = string.IsNullOrEmpty(userIdCode) ? GetUserIdCode() : userIdCode;
            return new ChangeEventHandler(userIdCode).Delete(db, changeEvent);
        }

        protected ChangeEvent DeleteChangeEvent(int? changeEventId, string userIdCode = null)
        {
            userIdCode = string.IsNullOrEmpty(userIdCode) ? GetUserIdCode() : userIdCode;
            return new ChangeEventHandler(userIdCode).Delete(db, changeEventId);
        }

        public string GetUserIdCode()
        {
            string userIdCode = null;
            if (AccountHandler.Instance.User == null)
            {
                userIdCode = GetUser().UserIdCode;
            }
            else
            {
                userIdCode = AccountHandler.Instance.User.UserIdCode;
            }
            System.Diagnostics.Debug.Assert(userIdCode != null);
            return userIdCode;
        }

        protected ApplicationUser GetUser()
        {
            return AccountHandler.Instance.Initialize(User.Identity.Name);
        }


        //// POST: /{Controller}/CreateUsingViewModel
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ///// <summary>
        ///// Implement CreateUsingViewModel(VM vmObj, Func<VM, T> action) within this method
        ///// </summary>
        ///// <param name="vmObj"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public abstract ActionResult CreateUsingViewModel(IViewModelBase vmObj);

        // POST: /{Controller}/CreateUsingViewModel
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        protected ActionResult CreateUsingViewModel(IViewModelBase vmObj, Func<IViewModelBase, T> convertAction, ActionResult onError = null)
        {
            try
            {
                // Convert ViewModel to Model (handled in the child class)
                if (ModelState.IsValid)
                {
                    T dbObj = convertAction(vmObj);
                    if (dbObj != null)
                    {
                        // Create a changeEvent object
                        AddChangeEvent(dbObj, dbObj);
                        // Attach an entity
                        //AddEntityToRelatedObject(dbObj, parent);
                        // Add the new DB Object to the List
                        db.Set<T>().Add(dbObj);
                        // Save changes to the DB
                        db.SaveChanges();
                        // Return JSON object if the request is AJAX
                        if (Request.IsAjaxRequest())
                        {
                            var dbObjWithId = dbObj as IHasId;
                            if (dbObjWithId != null)
                            {
                                vmObj.Id = dbObjWithId.Id;
                            }
                            return Json(vmObj);
                        }
                        // Otherwise redirect
                        return RedirectToAction("Index");
                    }
                    if (onError == null)
                    {
                        return Json(HttpStatusCode.NotFound);
                    }
                    return onError;
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            AddViewBag(null);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(vmObj);
            }
            return View(vmObj);
        }

        protected ActionResult CreateRelatedObjectUsingViewModel<TParent, TResult>(int? parentId, IViewModelBase vmObj, Func<TParent, TResult> convertAction, ActionResult onError = null)
            where TParent : class
            where TResult : class
        {
            TParent parent = null;
            try
            {
                if (ModelState.IsValid)
                {
                    //parent = Find(parentId);
                    parent = db.Set<TParent>().Find(parentId);
                    if (parent != null)
                    {
                        // Convert ViewModel to Model (handled in the child class)
                        TResult dbObj = convertAction(parent);
                        if (dbObj != null)
                        {
                            // Attach an entity
                            AddEntityToRelatedObject(dbObj, parent);
                            // Add the new DB Object to the List
                            db.Set<TResult>().Add(dbObj);
                            // Save changes to the DB
                            db.SaveChanges();
                            // Create a changeEvent object
                            AddChangeEvent(dbObj, parent);
                            // Save changes to the DB
                            db.SaveChanges();
                            // Return JSON object if the request is AJAX
                            if (Request.IsAjaxRequest())
                            {
                                var dbObjWithId = dbObj as IHasId;
                                if (dbObjWithId != null)
                                {
                                    vmObj.Id = dbObjWithId.Id;
                                }
                                return Json(vmObj);
                            }
                            // Otherwise redirect
                            return RedirectToAction("Index");
                        }
                        if (onError == null)
                        {
                            return Json(HttpStatusCode.NotFound);
                        }
                        return onError;
                    }
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //AddViewBag(parent);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(vmObj);
            }
            return View(vmObj);
        }

        // POST: /{Controller}/UpdateUsingViewModel
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Implement CreateUsingViewModel(VM vmObj, Func<VM, T> action) within this method
        /// </summary>
        /// <param name="vmObj"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult UpdateUsingViewModel(IViewModelBase vmObj)
        //{

        //}

        protected ActionResult UpdateUsingViewModel(IViewModelBase vmObj, Func<IViewModelBase, T> convertAction, ActionResult onError = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Convert ViewModel to Model (handled in the child class)
                    T dbObj = convertAction(vmObj);
                    if (dbObj != null)
                    {
                        var dbObjWithChangeEvent = dbObj as IHasChangeEvent;
                        // or if no ChangeEvent is assigned to the object for some reason, create one
                        if (dbObjWithChangeEvent != null && dbObjWithChangeEvent.ChangeEvent == null)// !hasChangeEvent)
                        {
                            AddChangeEvent(dbObj, dbObj);
                            db.SaveChanges();
                        }
                        // Update the changeEvent object
                        var hasChangeEvent = UpdateChangeEvent(dbObj, dbObj);
                        // Attach the entity
                        db.Set<T>().Attach(dbObj);
                        // Change its state to Modified so Entity Framework can update the existing product instead of creating a new one
                        db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;
                        // Save changes to the DB
                        db.SaveChanges();
                        // Return JSON object if the request is AJAX
                        if (Request.IsAjaxRequest())
                        {
                            var dbObjWithId = dbObj as IHasId;
                            if (dbObjWithId != null)
                            {
                                vmObj.Id = dbObjWithId.Id;
                            }
                            return Json(vmObj);
                        }
                        // Otherwise redirect
                        return RedirectToAction("Index");
                    }
                    if (onError == null)
                    {
                        return Json(HttpStatusCode.NotFound);
                    }
                    return onError;
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            AddViewBag(null);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(vmObj);
            }
            return View(vmObj);
        }

        protected ActionResult UpdateRelatedObjectUsingViewModel<TParent, TResult>(int? parentId, IViewModelBase vmObj, Func<TParent, TResult> convertAction, ActionResult onError = null)
            where TParent : class
            where TResult : class
        {
            TParent parent = null;
            try
            {
                // Todo: BankAccount_Update works, but MemberComment_Updated doesn't (somehow). Debug this!!! ( this check is important)
                if (ModelState.IsValid)
                {
                    parent = db.Set<TParent>().Find(parentId);
                    if (parent != null)
                    {
                        // Convert ViewModel to Model (handled in the child class)
                        TResult dbObj = convertAction(parent);
                        if (dbObj != null)
                        {
                            // Update the changeEvent object
                            var hasChangeEvent = UpdateChangeEvent<TResult, TParent>(dbObj, parent);
                            // or if no ChangeEvent is assigned to the object for some reason, create one
                            if (!hasChangeEvent)
                            {
                                AddChangeEvent(dbObj, parent);
                            }
                            // Attach an entity
                            AddEntityToRelatedObject(dbObj, parent);
                            //// Attach the entity
                            //db.Set<TResult>().Attach(dbObj);
                            // Set object state to modified
                            db.Entry(dbObj).State = System.Data.Entity.EntityState.Modified;
                            // Save changes to the DB
                            db.SaveChanges();
                            // Return JSON object if the request is AJAX
                            if (Request.IsAjaxRequest())
                            {
                                var dbObjWithId = dbObj as IHasId;
                                if (dbObjWithId != null)
                                {
                                    vmObj.Id = dbObjWithId.Id;
                                }
                                return Json(vmObj);
                            }
                            // Otherwise redirect
                            return RedirectToAction("Index");
                        }
                        if (onError == null)
                        {
                            return Json(HttpStatusCode.NotFound);
                        }
                        return onError;
                    }
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //AddViewBag(parent);
            if (Request != null && Request.IsAjaxRequest())
            {
                return PartialView(vmObj);
            }
            return View(vmObj);
        }

        private void AddEntityToRelatedObject<TObj, TParent>(TObj dbObj, TParent parent)
        {
            var dbObjWithEntity = dbObj as IHasEntity;
            if (dbObjWithEntity == null)
            {
                var dbObjWithPerson = dbObj as IHasPerson;
                if (dbObjWithPerson != null)
                {
                    dbObjWithEntity = dbObjWithPerson.Person;
                }
            }
            var parentWithPersonEntity = parent as IHasEntity;
            if (parentWithPersonEntity == null)
            {
                var parentWithPerson = parent as IHasPerson;
                parentWithPersonEntity = parentWithPerson != null ? parentWithPerson.Person : null;
            }
            if (dbObjWithEntity != null && parentWithPersonEntity != null && parentWithPersonEntity != null)
            {
                dbObjWithEntity.Entity = parentWithPersonEntity.Entity;
            }
        }

        IHasEntity GetObjectWithEntity<TObj, TParent>(TObj dbObj, TParent parent)
        {
            var objWithEntity = dbObj as IHasEntity;
            if (objWithEntity == null)
            {
                objWithEntity = parent as IHasEntity;
            }
            if (objWithEntity == null)
            {
                var objWithPerson = dbObj as IHasPerson;
                if (objWithPerson == null || objWithPerson.Person == null)
                {
                    objWithPerson = parent as IHasPerson;
                }
                if (objWithPerson != null && objWithPerson.Person != null)
                {
                    objWithEntity = objWithPerson.Person;
                }
            }
            return objWithEntity;
        }

        private List<ChangeEventItem> CreateChangeEventItems<TObj, TParent>(TObj dbObj, TParent parent, Models.ChangeEventType type) where TObj : class
        {
            var objWithEntity = GetObjectWithEntity(dbObj, parent);

            var changeEventItems = new List<ChangeEventItem>();
            Type entityType = dbObj.GetType();
            foreach (PropertyInfo entityProperty in entityType.GetProperties().ToList())
            {
                if (entityProperty.PropertyType.IsPrimitive || entityProperty.PropertyType.IsValueType || (entityProperty.PropertyType == typeof(string)))
                {
                    var property = db.Entry(dbObj).Property(entityProperty.Name);
                    object originalValue = property.IsModified ? property.OriginalValue : property.CurrentValue;
                    var columnName = property.Name;
                    var tableName = typeof(TObj) != null ? typeof(TObj).Name : "";
                    var entity = objWithEntity != null ? objWithEntity.Entity : null;

                    if (!columnName.StartsWith("ChangeEvent") && !columnName.Equals("Id") && !columnName.EndsWith("Id") && !columnName.Equals("RawNumber"))
                    {
                        //if (property.IsModified)
                        //{
                        var changeEventItem = new ChangeEventItem()
                        {
                            ChangedFrom = "" + originalValue,
                            ChangedTo = "" + property.CurrentValue,
                            Type = type,
                            Date = DateTime.UtcNow,
                            UserIdCode = GetUserIdCode(),
                            ChangeEvent = null,
                            ColumnName = columnName,
                            TableName = tableName,
                            Entity = entity
                            //EntityId = entity != null ? new Nullable<int>(entity.Id) : null
                        };
                        // only update if changed from isn't same as changed to (IsModified doesn't validate this)
                        if (objWithEntity != null)
                        {
                            if (type == ChangeEventType.CREATED || type == ChangeEventType.DELETED)
                            {
                                changeEventItems.Add(changeEventItem);
                            }
                            else if (type == ChangeEventType.UPDATED && !changeEventItem.ChangedFrom.Equals(changeEventItem.ChangedTo))
                            {
                                changeEventItems.Add(changeEventItem);
                            }
                        }
                        //}
                    }
                }
            }
            return changeEventItems;
        }

        protected void AddChangeEvent<TObj, TParent>(TObj dbObj, TParent parent) where TObj : class
        {
            var dbObjWithChangeEvent = dbObj as IHasChangeEvent;
            if (dbObjWithChangeEvent != null)
            {
                dbObjWithChangeEvent.ChangeEvent = CreateChangeEvent();
                var changeEventItems = CreateChangeEventItems(dbObj, parent, Models.ChangeEventType.CREATED);
                if (changeEventItems.Count > 0)
                {
                    dbObjWithChangeEvent.ChangeEvent.ChangeEventItems = changeEventItems;
                    //db.Entry(dbObjWithChangeEvent.ChangeEvent).Collection(x => x.ChangeEventItems).Load();
                    //dbObjWithChangeEvent.ChangeEvent.ChangeEventItems.AddRange(changeEventItems);
                }
            }
        }

        protected bool UpdateChangeEvent<TObj, TParent>(TObj dbObj, TParent parent) where TObj : class
        {
            var dbObjWithChangeEvent = dbObj as IHasChangeEvent;
            if (dbObjWithChangeEvent != null)
            {
                dbObjWithChangeEvent.ChangeEvent = UpdateChangeEvent(dbObjWithChangeEvent.ChangeEventId);
                var changeEventItems = CreateChangeEventItems(dbObj, parent, Models.ChangeEventType.UPDATED);
                if (changeEventItems != null)
                {
                    db.Entry(dbObjWithChangeEvent.ChangeEvent).Collection(x => x.ChangeEventItems).Load();
                    dbObjWithChangeEvent.ChangeEvent.ChangeEventItems.AddRange(changeEventItems);
                }
                return dbObjWithChangeEvent.ChangeEvent != null;
            }
            return true;
        }

        protected void DeleteChangeEvent<TObj, TParent>(TObj dbObj, TParent parent) where TObj : class
        {
            var dbObjWithChangeEvent = dbObj as IHasChangeEvent;
            if (dbObjWithChangeEvent != null)
            {
                dbObjWithChangeEvent.ChangeEvent = DeleteChangeEvent(dbObjWithChangeEvent.ChangeEventId);
                if (dbObjWithChangeEvent.ChangeEvent != null)
                {
                    var changeEventItems = CreateChangeEventItems(dbObj, parent, Models.ChangeEventType.DELETED);
                    if (changeEventItems != null)
                    {
                        db.Entry(dbObjWithChangeEvent.ChangeEvent).Collection(x => x.ChangeEventItems).Load();
                        dbObjWithChangeEvent.ChangeEvent.ChangeEventItems.AddRange(changeEventItems);
                    }
                }
            }
        }

        //[HttpPost]
        //public virtual ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        //{
        //    var fileContents = Convert.FromBase64String(base64);

        //    return File(fileContents, contentType, fileName);
        //}

        [HttpPost]
        public ActionResult ExportToExcel(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}