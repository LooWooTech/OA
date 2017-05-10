using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class FreeFlowManager : ManagerBase
    {
        public void Save(FreeFlow model)
        {
            DB.FreeFlows.AddOrUpdate(model);
            DB.SaveChanges();
        }

    }
}
