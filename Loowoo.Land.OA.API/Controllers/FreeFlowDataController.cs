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
        public void Submit([FromBody]FreeFlowNodeData model, int infoId, string toUserIds)
        {
            if (model == null || infoId == 0)
            {
                throw new Exception("参数不正确");
            }
            if (string.IsNullOrWhiteSpace(toUserIds))
            {
                throw new ArgumentException("请选择发送人");
            }
            var flowNodeData = Core.FlowNodeDataManager.GetModel(model.FlowNodeDataId);
            if (flowNodeData.Nodes.Count > 0)
            {
                model.UserId = CurrentUser.ID;
                model = Core.FreeFlowDataManager.Update(model);
            }

            FreeFlowNodeData parent = null;
            if (model.ParentId > 0)
            {
                parent = Core.FreeFlowDataManager.GetModel(model.ParentId);
            }
            var info = Core.FormInfoManager.GetModel(infoId);
            var targetUserIds = toUserIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(str => int.Parse(str)).ToArray();

            foreach (var userId in targetUserIds)
            {
                //如果是发送给上级，则不创建新的节点
                if (parent != null && userId == parent.UserId)
                {
                    continue;
                }
                if (parent == null && userId == flowNodeData.UserId)
                {
                    continue;
                }
                Core.FreeFlowDataManager.Add(new FreeFlowNodeData
                {
                    FlowNodeDataId = model.FlowNodeDataId,
                    ParentId = model.ID,
                    UserId = userId,
                });
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = info.ID,
                    FormId = info.FormId,
                    UserId = userId,
                    Status = FlowStatus.Doing,
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
                    case DepartmentLimitMode.Sender:
                        var user = Core.UserManager.GetModel(flowNodeData.UserId);
                        parameters.DepartmentId = user.DepartmentId;
                        break;
                    case DepartmentLimitMode.Poster:
                        var flowData = Core.FlowDataManager.Get(flowNodeData.FlowDataId);
                        var firstData = flowData.Nodes.OrderBy(e => e.ID).FirstOrDefault();
                        var sender = Core.UserManager.GetModel(firstData.UserId);
                        parameters.DepartmentId = sender.DepartmentId;
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
                .Select(e => new UserViewModel
                {
                    ID = e.ID,
                    RealName = e.RealName,
                    Department = e.Department == null ? null : e.Department.Name,
                    DepartmentId = e.DepartmentId,
                    JobTitle = e.JobTitle == null ? null : e.JobTitle.Name,
                    JobTitleId = e.JobTitleId,
                    Username = e.Username,
                    Role = e.Role
                });
        }
    }
}