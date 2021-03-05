using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapWebApplication.Interfaces;

namespace System
{
    public static class ControllerExtensions
    {
        public static IEnumerable<TObject> FilterDeleted<TObject>(this IEnumerable<TObject> list) where TObject : class
        {
            return list.Where(x => FilterOutDeleted(x));
        }

        public static bool FilterOutDeleted<TObject>(TObject input) where TObject : class
        {
            var changeEvent = input as IHasChangeEvent;
            if (changeEvent != null)
                return changeEvent.ChangeEventDeletedOn == null;
            return true;
        }
    }
}