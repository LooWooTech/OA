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
        protected OADbContext db { get { return OneContext.Current; } }
        protected OADbContext GetDbContext()
        {
            return new OADbContext();
        }
    }
}
