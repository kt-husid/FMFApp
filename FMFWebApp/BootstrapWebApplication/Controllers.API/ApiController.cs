using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Kthusid.Web;
using BootstrapWebApplication.Interfaces;
using BootstrapWebApplication.BLL;

namespace BootstrapWebApplication.Controllers.API
{
    public abstract class ApiController<T> : System.Web.Http.ApiController where T : class
    {
        protected HttpResponseMessage GetRelatedData(int? id, string viewName, string type = null)
        {
            T obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            RedirectToRoute("/api/" + viewName, obj);
            /*return RedirectToRoute(
                 "Default", 
                 new { controller = "MyController", 
                        action = "Index", 
                        id = itemId, 
                        task="add", 
                        status="1"
                      }
            );
            */
            return Request.GetResponse(obj, type);
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
            sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
            var param = Expression.Parameter(typeof(T));
            MemberExpression exp = null;
            try
            {
                exp = Expression.Property(param, sortName);
            }
            catch
            {
                exp = Expression.Property(param, "Id");
            }
            var sortExpression = Expression.Lambda<Func<T, dynamic>>(Expression.Convert(exp, exp.Type), param);
            //var sortExpression = GetSortExpression<T>(exp, param);
            switch (sortType.ToLower())
            {
                case "asc":
                    return list.OrderBy(sortExpression);
                default:
                    return list.OrderByDescending(sortExpression);

            }
        }

        /// <summary>
        /// Source: http://stackoverflow.com/questions/3752305/declaring-funcin-t-out-result-dynamically
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortName"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        private IQueryable<T> PagedListSort<T>(IQueryable<T> list, string sortName, bool ascending)
        {
            var p = Expression.Parameter(typeof(T));
            sortName = string.IsNullOrEmpty(sortName) ? "Id" : sortName;
            var x = Expression.Lambda(GetMemberExpression(p, sortName), p);
            return list.Provider.CreateQuery<T>(
                                    Expression.Call(typeof(Queryable),
                                       ascending ? "OrderBy" : "OrderByDescending",
                                       new Type[] { list.ElementType, x.Body.Type },
                                       list.Expression,
                                       x));
        }

        private MemberExpression GetMemberExpression(ParameterExpression p, string sortName)
        {
            MemberExpression memberExpression;
            try
            {
                memberExpression = Expression.Property(p, sortName);
            }
            catch
            {
                memberExpression = Expression.Property(p, "Id");
            }
            if (memberExpression.Member.MemberType == System.Reflection.MemberTypes.Property)
            {
                memberExpression = Expression.Property(p, "Id");
            }
            return memberExpression;
        }
        /* protected virtual IOrderedQueryable<T> PagedListSort(IQueryable<T> list, string sortName = "", string sortType = "asc")
        {
            var param = Expression.Parameter(typeof(T));
            MemberExpression exp;
            try {
                exp = Expression.Property(param, sortName);
            }
            catch
            {
                exp = Expression.Property(param, "Id");
            }
            Type expType = exp.Type;
            Expression<Func<T, Int32>> sortExpression = null;
            sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
            //sortExpression = SortExpression(sortName);// System.Extensions.CreatePropSelectorExpression<T, string>(sortName);
            try
            {
                sortExpression = Expression.Lambda<Func<T, dynamic>>(Expression.Convert(exp, expType), param);
            }
            catch
            {
                sortExpression = Expression.Lambda<Func<T, Int32>>(Expression.Convert(exp, Int32), param);
            }

            switch (sortType.ToLower())
            {
                case "asc":
                    return list.OrderBy(sortExpression);
                default:
                    return list.OrderByDescending(sortExpression);

            }
        }*/

        //private Expression<Func<T, Int32>> SortExpression(string propertyName)
        //{
        //    var param = Expression.Parameter(typeof(T));
        //    var exp = Expression.Property(param, propertyName);
        //    var sortExpression = Expression.Lambda<Func<T, Int32>>(Expression.Convert(exp, typeof(Int32)), param);
        //    return sortExpression;
        //}

        //protected virtual IOrderedQueryable<T> PagedListSort(IQueryable<T> list, string sortName = "", string sortType = "asc")
        //{
        //    Expression<Func<T, Int32>> sortExpression = null;
        //    sortName = "Id";
        //    sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
        //    sortExpression = SortExpression(sortName);// System.Extensions.CreatePropSelectorExpression<T, string>(sortName);
        //    switch (sortType.ToLower())
        //    {
        //        case "asc":
        //            return list.OrderBy(sortExpression);
        //        default:
        //            return list.OrderByDescending(sortExpression);

        //    }
        //}

        private class ObjectWithId
        {
            public int Id { get; private set; }
            public ObjectWithId(T obj)
                : base()
            {
                Id = ((dynamic)obj).Id;
            }
        }

        protected BootstrapContext db = new BootstrapContext();

        protected IPagedList<T> GetPagedList(string sortName, string sortType, string filterName = null, int? page = null, int? pageSize = null)
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
                filterName = "";
            }
            if (!String.IsNullOrEmpty(filterName))
            {
                list = PagedListFilter(list, filterName);
            }

            //try
            //{
            //    list = list.Where(s => (s as IHasChangeEvent).ChangeEvent.IsDeleted == false);
            //}
            //catch (Exception ex) { }

            // Sort
            //list = PagedListSort(list, sortName, sortType);
            list = PagedListSort(list, sortName, sortType == "asc");

            //int pageSize = Core.Settings.Instance.PagingResultsPerPage;
            pageSize = pageSize < 1 || pageSize > 50 ? 10 : pageSize;
            int pageNumber = (page ?? 1);
            IPagedList<T> pagedList = list.ToPagedList(pageNumber, (int)pageSize);
            return pagedList;
        }

        //protected abstract IQueryable<T> PagedListSelect();

        protected virtual IQueryable<T> PagedListSelect()
        {
            return db.Set<T>();
        }

        protected abstract IQueryable<T> PagedListFilter(IQueryable<T> list, string filterName = null);

        //protected abstract IQueryable<T> PagedListSort(IQueryable<T> list, string sortOrder = "");

        //protected abstract void Add(T obj);

        //protected abstract void Remove(T obj);

        protected virtual void Add(T obj)
        {
            db.Set<T>().Add(obj);
        }

        protected virtual void Remove(T obj)
        {
            //db.Set<T>().Remove(obj);
            try
            {
                var ce = obj as IHasChangeEvent;
                new ChangeEventHandler(GetUserId()).Delete(db, ce.ChangeEventId);
            }
            catch (Exception)
            {
                //Remove(obj);   
            }
        }

        //protected abstract bool ObjectExists(int id);

        protected bool ObjectExists(int id)
        {
            return db.Set<T>().Count(e => new ObjectWithId(e).Id == id) > 0;
        }

        protected T Find(int? id)
        {
            return db.Set<T>().Find(id);
        }

        // GET: api/{controller}
        public virtual HttpResponseMessage Get(string sortName, string sortType, string filterName = null, int? page = null, int? pageSize = null, string type = null)
        {
            return Request.GetResponse(GetPagedList(sortName, sortType, filterName, page, pageSize), type);
        }

        // GET: api/{controller}/{id}
        public virtual HttpResponseMessage Get(int id, string type = null)
        {
            T obj = Find(id);
            if (obj == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Request.GetResponse(obj, type);
        }

        // PUT: api/{controller}/{id}
        [ResponseType(typeof(void))]
        public virtual IHttpActionResult Put(int id, T obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ((dynamic)obj).Id)
            {
                return BadRequest();
            }

            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/{controller}
        public virtual IHttpActionResult Post(T obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Add(obj);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ((dynamic)obj).Id }, obj);
        }

        public virtual IHttpActionResult Delete(int id)
        {
            T obj = Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            Remove(obj);
            db.SaveChanges();

            return Ok(obj);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected string GetUserId()
        {
            return GetUser().UserIdCode;
            //return AccountHandler.Instance.UserIdCode;
            //return AccountHandler.Instance.User != null ? AccountHandler.Instance.User.UserIdCode : "bha";
        }

        protected ApplicationUser GetUser()
        {
            return AccountHandler.Instance.Initialize(User.Identity.Name);
        }

        //protected IHandler<> GetHandler(int id)
        //{
        //    new Exception("implement");
        //    return null;
        //}

        protected bool Update(T obj)
        {
            if (ModelState.IsValid)
            {
                //db.Set<T>().Attach(obj);
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
    }
}