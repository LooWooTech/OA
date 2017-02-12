using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class FlowManager:ManagerBase
    {
        public Flow Get(int infoId)
        {
            using (var db = GetDbContext())
            {
                return db.Flows.FirstOrDefault(e => e.InfoID == infoId);
            }
        }
    }
}