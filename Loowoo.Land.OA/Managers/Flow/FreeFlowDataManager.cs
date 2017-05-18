using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class FreeFlowDataManager : ManagerBase
    {
        public void Save(FreeFlowData model)
        {
            if (model.ID > 0)
            {
                model.UpdateTime = DateTime.Now;
            }
            DB.FreeFlowDatas.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public FreeFlowData GetModel(int id)
        {
            return DB.FreeFlowDatas.FirstOrDefault(e => e.ID == id);
        }

        public void Complete(int freeFlowDataId, int currentUserId, bool? completed = null)
        {
            var model = GetModel(freeFlowDataId);
            if (completed.HasValue)
            {
                model.Completed = completed.Value;
            }
            else
            {
                model.Completed = model.Nodes.All(e => e.Submited);
            }
            model.CompletedUserId = currentUserId;
            model.UpdateTime = DateTime.Now;
            DB.SaveChanges();
        }
    }
}
