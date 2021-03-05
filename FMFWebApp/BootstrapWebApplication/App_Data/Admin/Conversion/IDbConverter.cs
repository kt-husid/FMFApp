using BootstrapWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public interface IDbConverter<T>
    {
        void Convert(BootstrapContext db, T o);

        //void Setup(BootstrapContext db);

    }
}
