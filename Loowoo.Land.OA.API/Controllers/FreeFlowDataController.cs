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
        public void Submit([FromBody]FreeFlowNodeData model, int flowNodeDataId, int infoId, string toUserIds)
        {
            if (model == null || infoId == 0)
            {
                throw new Exception("参数不正确");
            }

            var flowNodeData = Core.FlowNodeDataManager.GetModel(flowNodeDataId);
            if (flowNodeData.FreeFlowData == null)
            {
                var freeFlowData = new FreeFlowData { ID = flowNodeData.ID };
                Core.FreeFlowDataManager.Save(freeFlowData);
                model.UpdateTime = DateTime.Now;
            }

            model.FreeFlowDataId = flowNodeData.FreeFlowData.ID;
            model.UserId = CurrentUser.ID;
            Core.FreeFlowNodeDataManager.Save(model);

            var targetUserIds = toUserIds.ToIntArray();
            //如果没有选择发送人，则代表此流程结束
            if (targetUserIds == null || targetUserIds.Length == 0)
            {
                Core.FreeFlowDataManager.Complete(model.FreeFlowDataId, CurrentUser.ID);
                return;
            }

            var info = Core.FormInfoManager.GetModel(infoId);
            foreach (var userId in targetUserIds)
            {
                //传阅流程不需要发给自己
                if (userId == CurrentUser.ID) continue;

                Core.FreeFlowNodeDataManager.Add(new FreeFlowNodeData
                {
                    FreeFlowDataId = model.FreeFlowDataId,
                    ParentId = model.ID,
                    UserId = userId,
                });
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = info.ID,
                    FormId = info.FormId,
                    UserId = userId,
                    Status = FlowStatus.Doing,
                    FlowNodeDataId = flowNodeDataId
                });
                Core.FeedManager.Save(new Feed()
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = userId,
                    Action = UserAction.Submit,
                    InfoId = infoId,
                    Title = info.Title,
                    Type = FeedType.FreeFlow,
                });
            }
        }

        [HttpGet]
        public object UserList(int flowNodeDataId, string key)
        {
            var flowNodeData = Core.FlowNodeDataManager.GetModel(flowNodeDataId);
            if (flowNodeData.FlowNode.FreeFlowId == 0)
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
            var freeFlowData = Core.FreeFlowDataManager.GetModel(id);
            var user = Core.UserManager.GetModel(CurrentUser.ID);
            var info = Core.FormInfoManager.GetModel(infoId);
            if (freeFlowData.FlowNodeData.CanCompleteFreeFlow(user))
            {
                Core.FreeFlowDataManager.Complete(id, CurrentUser.ID, true);
                Core.FeedManager.Save(new Feed
                {
                    FromUserId = CurrentUser.ID,
                    ToUserId = freeFlowData.FlowNodeData.UserId,
                    Action = UserAction.Complete,
                    InfoId = infoId,
                    Title = info.Title,
                    Description = CurrentUser.RealName + "结束了传阅流程",
                    Type = FeedType.FreeFlow
                });
            }
        }
    }
}