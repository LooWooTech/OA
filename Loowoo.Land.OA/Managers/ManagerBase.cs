using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class ManagerBase
    {
        protected ManagerCore Core { get { return ManagerCore.Instance; } }

        protected Caching.ICacheService Cache
        {
            get
            {
                return new Caching.LocalCacheService();
            }
        }

        protected OADbContext DB
        {
            get
            {
                return HttpDbContextContainer.GetDbContext<OADbContext>() ?? GetDbContext();
            }
        }

        protected OADbContext GetDbContext()
        {
            return new OADbContext();
        }
    }
}
