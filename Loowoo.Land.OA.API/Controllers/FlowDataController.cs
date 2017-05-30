using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FlowDataController : ControllerBase
    {
        [HttpGet]
        public IHttpActionResult Cancel(int infoId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (!info.FlowData.CanCancel(CurrentUser.ID))
            {
                return BadRequest("无法撤销");
            }
            Core.FlowDataManager.Cancel(info, CurrentUser.ID);
            return Ok();
        }

        /// <summary>
        /// 获取审批数据
        /// </summary>
        [HttpGet]
        public object Model(int id)
        {
            var flowData = Core.FlowDataManager.Get(id);
            if (flowData == null)
            {
                return BadRequest("参数不正确，没有获取到流程数据");
            }
            var flowNodeData = flowData.GetLastNodeData(CurrentUser.ID);
            return new
            {
                flowData,
                flowNodeData,
                canBack = flowData.CanBack(),
                canComplete = flowData.CanComplete(flowNodeData)
            };
        }

        [HttpPost]
        public void Submit([FromBody]FlowNodeData data, int infoId, int toUserId = 0)
        {
            Core.FlowDataManager.Submit(infoId, CurrentUser.ID, toUserId, data.Result.Value, data.Content);
        }

        [HttpGet]
        public object UserList(int flowId, int nodeId, int flowDataId = 0)
        {
            if (flowId == 0)
            {
                return BadRequest("没有指定流程");
            }

            var flow = Core.FlowManager.Get(flowId);
            if (flow == null)
            {
                return BadRequest("没有找到该流程");
            }
            //如果没有指定node，那默认为第一个node的下一步
            if (nodeId == 0)
            {
                var node = flow.GetFirstNode();
                nodeId = node.ID;
            }
            var nextNode = flow.GetNextStep(nodeId);

            var parameter = new Parameters.UserParameter();
            if (nextNode != null)
            {
                parameter.TitleIds = nextNode.JobTitleIds;
                parameter.UserIds = nextNode.UserIds;
                if (nextNode.LimitMode == DepartmentLimitMode.Assign)
                {
                    parameter.DepartmentIds = nextNode.DepartmentIds;
                }
                else if (nextNode.LimitMode == DepartmentLimitMode.Sender)
                {
                    if (nodeId == 0)
                    {
                        parameter.DepartmentIds = CurrentUser.DepartmentIds;
                    }
                    else if (flowDataId > 0)
                    {
                        var flowData = Core.FlowDataManager.Get(flowDataId);
                        var senderNodeData = flowData.GetFirstNodeData();
                        var user = Core.UserManager.GetModel(senderNodeData.UserId);
                        parameter.DepartmentIds = user.DepartmentIds;
                    }
                }
            }

            var users = Core.UserManager.GetList(parameter);
            return users.Select(e => new UserViewModel
            {
                ID = e.ID,
                Username = e.Username,
                RealName = e.RealName,
                JobTitle = e.JobTitle == null ? null : e.JobTitle.Name,
                JobTitleId = e.JobTitleId,
                Role = e.Role
            }).Where(e => e.ID != CurrentUser.ID);
        }

        [HttpGet]
        public IHttpActionResult BackList(int infoId, int currentFlowNodeId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("获取表单数据错误");
            }
            var list = info.FlowData.Nodes;
            return Ok(list);
        }

        [HttpGet]
        public bool CanComplete(int flowDataId, int nodeDataId)
        {
            var flowData = Core.FlowDataManager.Get(flowDataId);
            if (flowData == null || flowData.FlowId == 0)
            {
                return false;
            }
            var nodeData = flowData.Nodes.FirstOrDefault(e => e.ID == nodeDataId);
            var flow = Core.FlowManager.Get(flowData.FlowId);
            var lastNode = flow.GetLastNode();
            return nodeData.FlowNodeId == lastNode.ID;
        }
    }
}
