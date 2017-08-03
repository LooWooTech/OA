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

        private OADbContext _db;
        protected OADbContext DB
        {
            get
            {
                if (_db == null)
                {
                    _db = HttpDbContextContainer.GetDbContext<OADbContext>() ?? GetDbContext();
                }
                return _db;
            }
        }

        protected OADbContext GetDbContext()
        {
            return new OADbContext();
        }
    }
}
