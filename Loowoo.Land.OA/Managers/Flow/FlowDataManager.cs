using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FlowDataManager : ManagerBase
    {

        public int Save(FlowData flowData)
        {
            using (var db = GetDbContext())
            {
                db.FlowDatas.Add(flowData);
                db.SaveChanges();
                return flowData.ID;
            }
        }

        private FlowData CreateFlowData(int flowId, FormInfo info)
        {
            var entity = DB.FlowDatas.FirstOrDefault(e => e.InfoId == info.ID && e.FormId == info.FormId && e.FlowId == flowId);
            if (entity == null)
            {
                entity = new FlowData
                {
                    FlowId = flowId,
                    InfoId = info.ID,
                    FormId = info.FormId,
                };
                DB.FlowDatas.Add(entity);
                DB.SaveChanges();
            }
            return entity;
        }

        public void Create(int flowId, FormInfo info)
        {
            info.FlowData = CreateFlowData(flowId, info);
            info.FlowDataId = info.FlowData.ID;

            //如果第一次提交，则先创建flowdata
            var currentUser = Core.UserManager.GetModel(info.PostUserId);
            //创建第一个节点
            Core.FlowNodeDataManager.CreateNextNodeData(info, currentUser, 0);
            //创建状态
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = info.ID,
                UserId = info.PostUserId,
                Status = FlowStatus.Doing,
                FormId = info.FormId,
            });
        }

        public void Cancel(FormInfo info, int userId)
        {
            if (info.FlowData.Completed) return;

            var lastNodedata = info.FlowData.GetLastNodeData();
            DB.FlowNodeDatas.Remove(lastNodedata);
            var cancelFlowData = info.FlowData.GetLastNodeData(userId);
            cancelFlowData.Result = null;
            cancelFlowData.UpdateTime = null;
            info.FlowStep = cancelFlowData.FlowNodeName;
            DB.SaveChanges();

            Core.UserFormInfoManager.Delete(new UserFormInfo
            {
                InfoId = info.ID,
                FormId = info.FormId,
                UserId = lastNodedata.UserId
            });
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = info.ID,
                FormId = info.FormId,
                Status = FlowStatus.Doing,
                UserId = cancelFlowData.UserId,
            });

        }

        public void Complete(FormInfo info)
        {
            //更新所有参与的人，状态改为已完成
            var list = DB.UserFormInfos.Where(e => e.InfoId == info.ID && e.FormId == info.FormId);
            foreach (var item in list)
            {
                item.Status = FlowStatus.Completed;
            }

            info.FlowData.Completed = true;
            info.FlowStep = "完结";
            info.UpdateTime = DateTime.Now;

            DB.SaveChanges();
        }

        public FlowData Get(int flowDataId)
        {
            return DB.FlowDatas.FirstOrDefault(e => e.ID == flowDataId);
        }
    }
}