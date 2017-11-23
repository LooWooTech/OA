using Loowoo.Common;
using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FreeFlowDataController : ControllerBase
    {
        [HttpPost]
        public object Submit([FromBody]FreeFlowNodeData model, int flowNodeDataId, int infoId, string toUserIds = null, string ccUserIds = null)
        {
            if (model == null)
            {
                throw new Exception("缺少参数model");
            }
            if (infoId == 0)
            {
                throw new Exception("缺少参数infoId");
            }

            var flowNodeData = Core.FlowNodeDataManager.GetModel(flowNodeDataId);
            if (flowNodeData.FreeFlowData == null)
            {
                flowNodeData.FreeFlowData = new FreeFlowData { ID = flowNodeData.ID };
                Core.FreeFlowDataManager.Save(flowNodeData.FreeFlowData);
            }
            model.FreeFlowDataId = flowNodeData.FreeFlowData.ID;
            model.UserId = CurrentUser.ID;
            model.UpdateTime = DateTime.Now;
            Core.FreeFlowNodeDataManager.Save(model);
            var info = Core.FormInfoManager.GetModel(infoId);
            //已阅则放到已读箱
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                UserId = CurrentUser.ID,
                InfoId = infoId,
                FlowStatus = FlowStatus.Done
            });
            var targetUserIds = toUserIds.ToIntArray();
            var ccTargetUserIds = ccUserIds.ToIntArray();
            //如果没有选择发送人，则代表此流程结束
            if ((targetUserIds == null || targetUserIds.Length == 0)
                && (ccTargetUserIds == null || ccTargetUserIds.Length == 0))
            {
                Core.FreeFlowDataManager.TryComplete(model.FreeFlowDataId, CurrentUser.ID);
                return model;
            }

            //如果有选择发送人，则标记为没结束
            flowNodeData.FreeFlowData.Completed = false;
            if (targetUserIds != null)
            {
                foreach (var userId in targetUserIds)
                {
                    SubmitFreeFlowNodeData(model, info, userId);
                }
            }

            if (ccTargetUserIds != null)
            {
                foreach (var userId in ccTargetUserIds)
                {
                    if (targetUserIds != null && targetUserIds.Contains(userId)) continue;
                    SubmitFreeFlowNodeData(model, info, userId, true);
                }
            }
            return model;
        }

        private void SubmitFreeFlowNodeData(FreeFlowNodeData model, FormInfo info, int userId, bool isCc = false)
        {
            //传阅流程不需要发给自己
            if (userId == CurrentUser.ID) return;

            var added = Core.FreeFlowNodeDataManager.Add(new FreeFlowNodeData
            {
                FreeFlowDataId = model.FreeFlowDataId,
                ParentId = model.ID,
                UserId = userId,
                IsCc = isCc
            });
            if (added)
            {
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = info.ID,
                    UserId = userId,
                    FlowStatus = FlowStatus.Doing,
                });

                var feed = new Feed
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = userId,
                    Action = UserAction.Submit,
                    InfoId = info.ID,
                    Title = info.Title,
                    Type = FeedType.FreeFlow,
                };
                Core.FeedManager.Save(feed);
                Core.MessageManager.Add(feed);
            }
        }

        [HttpGet]
        public object UserList(int flowNodeDataId, string key)
        {
            var flowNodeData = Core.FlowNodeDataManager.GetModel(flowNodeDataId);
            if (flowNodeData.FlowNode.FreeFlow == null)
            {
                return null;
            }
            var parameters = new UserParameter { SearchKey = key };
            var freeFlow = flowNodeData.FlowNode.FreeFlow;
            if (!freeFlow.CrossDepartment)
            {
                switch (freeFlow.LimitMode)
                {
                    case DepartmentLimitMode.Assign:
                        parameters.DepartmentIds = freeFlow.DepartmentIds;
                        break;
                    case DepartmentLimitMode.Self:
                        var user = Core.UserManager.GetModel(flowNodeData.UserId);
                        parameters.DepartmentIds = user.DepartmentIds;
                        break;
                    case DepartmentLimitMode.Poster:
                        var flowData = Core.FlowDataManager.Get(flowNodeData.FlowDataId);
                        var firstData = flowData.Nodes.OrderBy(e => e.ID).FirstOrDefault();
                        var sender = Core.UserManager.GetModel(firstData.UserId);
                        parameters.DepartmentIds = sender.DepartmentIds;
                        break;
                }
            }
            if (!freeFlow.CrossLevel)
            {
                var parentTitle = Core.JobTitleManager.GetParent(CurrentUser.JobTitleId);
                var subTitle = Core.JobTitleManager.GetSub(CurrentUser.JobTitleId);
                parameters.TitleIds = new[] { parentTitle == null ? 0 : parentTitle.ID, CurrentUser.JobTitleId, subTitle == null ? 0 : subTitle.ID };
            }

            return Core.UserManager.GetList(parameters).Where(e => e.ID != CurrentUser.ID)
                .Select(e => new UserViewModel(e));
        }

        [HttpGet]
        public void Complete(int id, int infoId)
        {
            var user = Core.UserManager.GetModel(CurrentUser.ID);
            var info = Core.FormInfoManager.GetModel(infoId);
            if (Core.FreeFlowDataManager.CanComplete(info.FlowData, user))
            {
                Core.FreeFlowDataManager.TryComplete(id, CurrentUser.ID, true);

                var freeFlowData = Core.FreeFlowDataManager.GetModel(id);

                var feed = new Feed
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = freeFlowData.FlowNodeData.UserId,
                    Action = UserAction.Complete,
                    InfoId = infoId,
                    Title = info.Title,
                    Description = CurrentUser.RealName + "结束了传阅流程",
                    Type = FeedType.FreeFlow
                };
                Core.FeedManager.Save(feed);
                Core.MessageManager.Add(feed);
            }
        }
    }
}