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

        public FlowNodeData CreateNextNodeData(FlowNodeData currentNodeData, int toUserId, int extendId = 0)
        {
            var flowNode = Core.FlowNodeManager.GetNextNode(currentNodeData.FlowNodeId);
            return CreateNodeData(currentNodeData.FlowDataId, flowNode, toUserId, extendId);
        }

        public FlowNodeData CreateNodeData(int flowDataId, FlowNode flowNode, int toUserId, int extendId = 0)
        {
            if (flowNode == null)
            {
                throw new Exception("流程配置不正确，请停止提交并联系管理员");
            }
            var toUser = Core.UserManager.GetModel(toUserId) ?? new User();
            var model = new FlowNodeData
            {
                FlowNodeId = flowNode.ID,
                FlowNodeName = flowNode.Name,
                Signature = toUser.RealName,
                UserId = toUser.ID,
                FlowDataId = flowDataId,
                ExtendId = extendId
            };

            Core.FlowNodeDataManager.Save(model);
            return model;
        }

        public FlowNodeData CreateNextNodeData(FlowData flowData, int toUserId, int extendId = 0)
        {
            var lastNodeData = flowData.GetLastNodeData();
            var flowNode = flowData.Flow.GetNextStep(lastNodeData == null ? 0 : lastNodeData.FlowNodeId);
            return CreateNodeData(flowData.ID, flowNode, toUserId, extendId);
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

        public FlowNodeData CreateChildNodeData(FlowNodeData parent, int toUserId, int extendId = 0)
        {
            if (toUserId == 0)
            {
                throw new Exception("没有选择发送人");
            }
            var user = Core.UserManager.GetModel(toUserId);
            var model = new FlowNodeData
            {
                FlowNodeId = parent.FlowNodeId,
                FlowNodeName = parent.FlowNodeName,
                Signature = user.RealName,
                UserId = user.ID,
                FlowDataId = parent.FlowDataId,
                ParentId = parent.ID,
                ExtendId = extendId
            };
            Core.FlowNodeDataManager.Save(model);
            return model;
        }

        public bool CanSubmit(FlowNodeData model)
        {
            var result = !model.Result.HasValue;
            if (!result && model.FreeFlowData != null)
            {
                result = model.FreeFlowData.Completed;
            }
            if (!result)
            {
                var children = DB.FlowNodeDatas.Where(e => e.ParentId == model.ID);
                foreach (var child in children)
                {
                    result = CanSubmit(child);
                    if (!result) break;
                }
            }
            return result;
        }

        public void Submit(FlowNodeData model)
        {
            var entity = DB.FlowNodeDatas.FirstOrDefault(e => e.ID == model.ID);
            entity.Content = model.Content;
            entity.UpdateTime = DateTime.Now;
            entity.Result = model.Result;
            DB.SaveChanges();
        }

        public void Delete(FlowNodeData model)
        {
            if (model == null) return;
            DB.FlowNodeDatas.Remove(model);
            DB.SaveChanges();
        }

        public FlowNodeData GetModelByExtendId(int extendId, int userId)
        {
            return DB.FlowNodeDatas.Where(e => e.ExtendId == extendId && e.UserId == userId).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public IEnumerable<FlowNodeData> GetList(int infoId, int userId)
        {
            var flowData = Core.FlowDataManager.GetModelByInfoid(infoId);
            var list = flowData.Nodes.AsQueryable();
            if (userId > 0)
            {
                list = list.Where(e => e.UserId == userId);
            }
            return list;
        }
    }
}