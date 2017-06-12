using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FlowNodeDataManager : ManagerBase
    {
        public void Save(FlowNodeData model)
        {
            if (model.ID > 0)
            {
                var entity = DB.FlowNodeDatas.FirstOrDefault(e => e.ID == model.ID);
                entity.Result = model.Result;
                entity.UpdateTime = model.Result.HasValue ? DateTime.Now : default(DateTime?);
                entity.Content = model.Content;

                model = entity;
            }
            else
            {
                DB.FlowNodeDatas.Add(model);
            }
            DB.SaveChanges();
        }

        public FlowNodeData CreateNextNodeData(FlowData flowData, int toUserId)
        {
            if (toUserId == 0)
            {
                throw new Exception("没有选择发送人");
            }

            var user = Core.UserManager.GetModel(toUserId);
            var lastNodeData = flowData.GetLastNodeData();
            var flowNode = flowData.Flow.GetNextStep(lastNodeData == null ? 0 : lastNodeData.FlowNodeId);

            var model = new FlowNodeData
            {
                CreateTime = DateTime.Now,
                FlowNodeId = flowNode == null ? 0 : flowNode.ID,
                FlowNodeName = flowNode == null ? user.RealName : flowNode.Name,
                Signature = user.RealName,
                UserId = user.ID,
                FlowDataId = flowData.ID,
            };

            Core.FlowNodeDataManager.Save(model);
            return model;
        }

        public FlowNodeData GetModel(int id)
        {
            return DB.FlowNodeDatas.FirstOrDefault(e => e.ID == id);
        }

        public FlowNodeData CreateBackNodeData(FlowNodeData backNodeData)
        {
            var user = Core.UserManager.GetModel(backNodeData.UserId);
            var model = new FlowNodeData
            {
                CreateTime = DateTime.Now,
                FlowDataId = backNodeData.FlowDataId,
                Signature = user.RealName,
                UserId = user.ID,
                FlowNodeId = backNodeData.FlowNodeId,
                FlowNodeName = backNodeData.FlowNodeName,
            };
            Core.FlowNodeDataManager.Save(model);
            return model;
        }
    }
}