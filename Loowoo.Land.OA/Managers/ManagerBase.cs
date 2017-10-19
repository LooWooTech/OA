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
        protected virtual OADbContext DB
        {
            get
            {
                var db = HttpDbContextContainer.GetDbContext<OADbContext>();
                if (db == null)
                {
                    if (_db == null)
                    {
                        _db = new OADbContext();
                    }
                    db = _db;
                }
                return db;
            }
        }
    }
}
