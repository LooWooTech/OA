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

        public bool CanSubmit(FlowData flowData, int userId)
        {
            if (flowData.Completed) { return false; }

            var flowNodeData = flowData.GetLastNodeData();

            if (flowNodeData.FlowNode.FreeFlowId == 0) return false;

            var freeFlowData = flowNodeData.FreeFlowData;
            if (freeFlowData == null)
            {
                //如果用户是该主流程的处理人，则可以submit
                if (userId == flowNodeData.UserId)
                {
                    return true;
                }
            }
            else
            {
                if (freeFlowData.Completed)
                {
                    return flowNodeData.UserId == userId;
                }
                //就算提交过，也可以转发给其他人
                return freeFlowData.Nodes.Any(e => e.UserId == userId);
            }
            return false;
        }

        public bool CanComplete(FlowData flowData, User user)
        {
            if (flowData.Completed) return false;

            var lastNodeData = flowData.GetLastNodeData();
            if (lastNodeData.FlowNode.FreeFlowId == 0) return false;
            if (lastNodeData.FreeFlowData == null || lastNodeData.FreeFlowData.Completed) return false;
            return lastNodeData.FlowNode.FreeFlow.IsCompleteUser(user);
        }

        public void TryComplete(int freeFlowDataId, int currentUserId, bool? completed = null)
        {
            var model = GetModel(freeFlowDataId);
            if (completed.HasValue)
            {
                model.Completed = completed.Value;
            }
            else
            {
                model.Completed = model.AllNodesHasSubmited;
            }
            model.CompletedUserId = currentUserId;
            model.UpdateTime = DateTime.Now;
            DB.SaveChanges();
        }
    }
}
