using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 流程步骤管理
    /// </summary>
    public class FlowNodeManager : ManagerBase
    {
        public int Save(FlowNode model)
        {
            if (model.FreeFlow != null && model.FreeFlow.LimitMode > 0)
            {
                Core.FreeFlowManager.Save(model.FreeFlow);
                model.FreeFlowId = model.FreeFlow.ID;
            }
            if (model.ID > 0)
            {
                var entity = DB.FlowNodes.FirstOrDefault(e => e.ID == model.ID);
                DB.Entry(entity).CurrentValues.SetValues(model);
            }
            else
            {
                DB.FlowNodes.Add(model);
            }
            DB.SaveChanges();
            return model.ID;
        }

        public void Delete(int id)
        {
            var model = DB.FlowNodes.Find(id);
            if (model != null)
            {
                //检查有没有在此节点的流程，如果有，则不能删除
                if (DB.FlowNodeDatas.Any(e => e.FlowNodeId == model.ID && e.Result == null))
                {
                    throw new Exception("该节点还有流程在审批，暂时无法删除");
                }

                var next = DB.FlowNodes.FirstOrDefault(e => e.PrevId == model.ID);
                if (next != null)
                {
                    next.PrevId = model.PrevId;
                }
                DB.FlowNodes.Remove(model);
                DB.SaveChanges();
            }
        }

        public FlowNode Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return DB.FlowNodes.Find(id);
        }

    }
}