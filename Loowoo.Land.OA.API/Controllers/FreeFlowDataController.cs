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
        public void Submit([FromBody]FreeFlowNodeData data, int infoId, string toUserIds)
        {
            if (data == null || infoId == 0)
            {
                throw new Exception("参数不正确");
            }
            if (string.IsNullOrWhiteSpace(toUserIds))
            {
                throw new ArgumentException("请选择发送人");
            }

            data.UserId = CurrentUser.ID;
            var model = Core.FreeFlowDataManager.Update(data);

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
            //如果没有关键字，则默认包含发给当前结点的人
            if (string.IsNullOrEmpty(key))
            {

            }
            if (!freeFlow.CrossDepartment)
            {
                switch (freeFlow.LimitMode)
                {
                    case FreeFlowLimitDepartmentMode.Assign:
                        parameters.DepartmentIds = freeFlow.DepartmentIds;
                        break;
                    case FreeFlowLimitDepartmentMode.Sender:
                        var user = Core.UserManager.GetModel(flowNodeData.UserId);
                        parameters.DepartmentId = user.DepartmentId;
                        break;
                    case FreeFlowLimitDepartmentMode.Poster:
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
            return Core.UserManager.GetList(parameters).Where(e => e.ID != CurrentUser.ID);
        }
    }
}