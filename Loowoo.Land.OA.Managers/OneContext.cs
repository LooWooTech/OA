using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public static class OneContext
    {
        private static string _contextName { get; set; }
        public static OADbContext Current
        {
            get { return HttpContext.Current.Items[_contextName] as OADbContext; }
        }
        static OneContext()
        {
            _contextName = "_entityContext";
        }

        public static void Begin()
        {
            HttpContext.Current.Items[_contextName] = new OADbContext();
        }

        public static void End()
        {
            var entity = HttpContext.Current.Items[_contextName] as OADbContext;
            if (entity != null)
            {
                entity.Dispose();
            }
        }
    }
}
