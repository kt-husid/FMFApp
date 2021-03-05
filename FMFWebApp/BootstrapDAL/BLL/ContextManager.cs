using BootstrapWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public static class ContextManager
    {
        public static BootstrapContext ResetContext(BootstrapContext context, int count, int commitCount = 100, bool recreateContext = false)
        {
            if (count % commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new BootstrapContext();
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            return context;
        }
    }
}