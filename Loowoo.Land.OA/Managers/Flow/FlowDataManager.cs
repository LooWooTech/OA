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

        public FlowData CreateFlowData(FormInfo info)
        {
            info.FlowData = CreateFlowData(info.Form.FLowId, info);
            info.FlowDataId = info.FlowData.ID;
            info.FlowData.Flow = Core.FlowManager.Get(info.Form.FLowId);
            //创建第一个节点
            var nodeData = Core.FlowNodeDataManager.CreateNextNodeData(info.FlowData, info.PostUserId);
            //创建状态
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                FlowNodeDataId = nodeData.ID,
                FormId = info.FormId,
                InfoId = info.ID,
                UserId = info.PostUserId,
                Status = FlowStatus.Doing,
            });
            return info.FlowData;
        }

        public FlowNodeData SubmitToUser(FlowData flowData, int toUserId)
        {
            return Core.FlowNodeDataManager.CreateNextNodeData(flowData, toUserId);
        }

        public FlowNodeData SubmitToBack(FlowData flowData)
        {
            var firstNodeData = flowData.GetFirstNodeData();
            return Core.FlowNodeDataManager.CreateBackNodeData(firstNodeData);
        }

        //public void Submit(int infoId, int userId, int toUserId, bool result, string content)
        //{
        //    var info = Core.FormInfoManager.GetModel(infoId);
        //    if (info == null)
        //    {
        //        throw new ArgumentException("参数错误，找不到FormInfo");
        //    }
        //    if (info.FlowDataId == 0)
        //    {
        //        var form = Core.FormManager.GetModel(info.FormId);
        //        //创建流程
        //        info.FlowData = Create(form.FLowId, info);
        //        info.FlowDataId = info.FlowData.ID;
        //    }

        //    var currentNodeData = info.FlowData.GetLastNodeData(userId);
        //    if (currentNodeData.Result.HasValue)
        //    {
        //        throw new Exception("无法提交");
        //    }
        //    currentNodeData.Content = content;
        //    currentNodeData.Result = result;
        //    Core.FlowNodeDataManager.Save(currentNodeData);

        //    //更新userforminfo的状态
        //    Core.UserFormInfoManager.Save(new UserFormInfo
        //    {
        //        FormId = info.FormId,
        //        InfoId = info.ID,
        //        Status = FlowStatus.Done,
        //        UserId = userId,
        //        FlowNodeDataId = currentNodeData.ID
        //    });

        //    //创建下一步节点
        //    var flow = Core.FlowManager.Get(info.Form.FLowId);
        //    if (currentNodeData.Result == true)
        //    {
        //        //判断是否流程结束
        //        if (!info.FlowData.CanComplete(currentNodeData))
        //        {
        //            var toUser = Core.UserManager.GetModel(toUserId);
        //            var nextNodedata = Core.FlowNodeDataManager.CreateNextNodeData(info, toUser, currentNodeData.FlowNodeId);
        //            info.FlowStep = nextNodedata.FlowNodeName;
        //            Core.UserFormInfoManager.Save(new UserFormInfo
        //            {
        //                FormId = info.FormId,
        //                InfoId = info.ID,
        //                Status = FlowStatus.Doing,
        //                UserId = nextNodedata.UserId,
        //                FlowNodeDataId = nextNodedata.ID,
        //            });
        //            Core.FeedManager.Save(new Feed
        //            {
        //                InfoId = info.ID,
        //                Action = UserAction.Submit,
        //                FromUserId = currentNodeData.UserId,
        //                ToUserId = nextNodedata.UserId,
        //                Type = FeedType.Flow,
        //                Title = info.Title,
        //                Description = currentNodeData.Content
        //            });
        //        }
        //        else
        //        {
        //            Core.FlowDataManager.Complete(info);
        //            var userIds = Core.UserFormInfoManager.GetUserIds(infoId);
        //            foreach (var uid in userIds)
        //            {
        //                Core.FeedManager.Save(new Feed
        //                {
        //                    InfoId = info.ID,
        //                    Action = UserAction.Submit,
        //                    FromUserId = currentNodeData.UserId,
        //                    ToUserId = uid,
        //                    Type = FeedType.Flow,
        //                    Title = info.Title,
        //                    Description = currentNodeData.Content
        //                });
        //            }
        //        }
        //    }
        //    else if (flow.CanBack)
        //    {
        //        var firstNodeData = info.FlowData.GetFirstNodeData();
        //        var nextNodeData = Core.FlowNodeDataManager.CreateBackNodeData(info, firstNodeData);
        //        info.FlowStep = nextNodeData.FlowNodeName;
        //        Core.UserFormInfoManager.Save(new UserFormInfo
        //        {
        //            InfoId = info.ID,
        //            FormId = info.FormId,
        //            UserId = nextNodeData.UserId,
        //            Status = FlowStatus.Back,
        //            FlowNodeDataId = nextNodeData.ID,
        //        });
        //    }
        //    else
        //    {
        //        //如果不可以退回，则直接结束流程
        //        info.FlowData.Completed = true;
        //        Core.FlowDataManager.Save(info.FlowData);
        //    }
        //}

        public void Cancel(FlowData flowData, int userId)
        {
            var nodeData = flowData.GetLastNodeData(userId);
            var nextNode = flowData.Flow.GetNextStep(nodeData.FlowNodeId);
            //如果下一步不为空，则需要删除最后的节点记录，如果最后节点
            if (nextNode != null)
            {
                var nextNodeData = flowData.GetNextNodeData(nodeData.ID);
                if (nextNodeData != null)
                {
                    DB.FlowNodeDatas.Remove(nextNodeData);
                }
            }
            else
            {
                flowData.Completed = false;
            }
            nodeData.UpdateTime = null;
            nodeData.Result = null;
            DB.SaveChanges();
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