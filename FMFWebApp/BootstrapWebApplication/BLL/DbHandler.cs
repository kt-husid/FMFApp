using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class DbHandler<T> where T : System.Data.Entity.DbContext, new()
    {
        private static DbHandler<T> _instance;
        public static DbHandler<T> Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DbHandler<T>();
                }
                return _instance;
            }
        }

        public T DbContext { get; private set; }

        private DbHandler()
        {
            DbContext = new T();
        }
    }
}