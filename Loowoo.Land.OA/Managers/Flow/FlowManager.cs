using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 流程头信息管理
    /// </summary>
    public class FlowManager : ManagerBase
    {
        public void Save(Flow model)
        {
            DB.Flows.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public Flow Get(int id)
        {
            return DB.Flows.FirstOrDefault(e => e.ID == id);
        }

        public bool Delete(int id)
        {
            var model = DB.Flows.FirstOrDefault(e => e.ID == id);
            if (model == null)
            {
                return false;
            }
            DB.Flows.Remove(model);
            DB.SaveChanges();
            return true;
        }
        public IEnumerable<Flow> GetList()
        {
            return DB.Flows;
        }
    }
}