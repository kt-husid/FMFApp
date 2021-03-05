using BootstrapWebApplication.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BootstrapWebApplication
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        public BaseController()
        {
            
        }


        ////class GenericSorter
        ////{
        ////    public IQueryable<T> Sort(IQueryable<T> source, string sortBy, string sortDirection)
        ////    {
        ////        var param = Expression.Parameter(typeof(T), "item");

        ////        var sortExpression = Expression.Lambda<Func<T, object>>
        ////            (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

        ////        switch (sortDirection.ToLower())
        ////        {
        ////            case "asc":
        ////                return source.AsQueryable<T>().OrderBy<T, object>(sortExpression);
        ////            default:
        ////                return source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression);

        ////        }
        ////    }
        ////}

        //private Expression<Func<T, Int32>> SortExpression(string propertyName)
        //{
        //    var param = Expression.Parameter(typeof(T));
        //    var exp = Expression.Property(param, propertyName);

        //    var sortExpression = Expression.Lambda<Func<T, Int32>>(Expression.Convert(exp, typeof(Int32)), param);

        //    return sortExpression;
        //    // return System.Extensions.CreatePropSelectorExpression<T, S>(propertyName);
        //}

        ////protected abstract IQueryable<T> PagedListSort(IQueryable<T> list, string sortOrder = "", string sortType = "asc");
        //protected virtual IOrderedQueryable<T> PagedListSort(IQueryable<T> list, string sortName = "", string sortType = "asc")
        //{
        //    Expression<Func<T, Int32>> sortExpression = null;
        //    sortName = "Id";
        //    sortName = !string.IsNullOrEmpty(sortName) ? sortName : "Id";
        //    ////List<Expression<Func<T, object>>> expressions = new List<Expression<Func<T, object>>>();
        //    //var properties = typeof(T).GetProperties();
        //    //foreach (var property in properties)
        //    //{
        //    //    Expression<Func<T, int>> exp = u =>
        //    //    (
        //    //         u.GetType().InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null)
        //    //    );
        //    //    if (property.Name == sortName)
        //    //    {
        //    //        orderByExpression = exp;
        //    //        break;
        //    //    }
        //    //    //expressions.Add(exp);
        //    //}

        //    sortExpression = SortExpression(sortName);// System.Extensions.CreatePropSelectorExpression<T, string>(sortName);
        //    switch (sortType.ToLower())
        //    {
        //        case "asc":
        //            return list.OrderBy(sortExpression);
        //        default:
        //            return list.OrderByDescending(sortExpression);

        //    }
        //}
    }
}