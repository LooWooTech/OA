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



        [HttpPost]
        public IHttpActionResult Submit([FromBody]FlowNodeData data, int infoId, int toUserId = 0)
        {
            if (data == null || data.ID == 0)
            {
                return BadRequest("参数错误");
            }
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("参数错误");
            }
            if (data.ID == 0)
            {
                if (info.FlowDataId > 0)
                {
                    return BadRequest("参数不正确");
                }
                else
                {
                    Core.FlowDataManager.Create(info.Form.FLowId, info);
                    data = info.FlowData.GetFirstNodeData();
                }
            }
            var model = info.FlowData.Nodes.FirstOrDefault(e => e.ID == data.ID);
            if (data.UserId != model.UserId)
            {
                return BadRequest("权限不足");
            }

            Core.FlowNodeDataManager.Save(data);

            //更新userforminfo的状态
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                FormId = info.FormId,
                InfoId = info.ID,
                Status = FlowStatus.Done,
                UserId = data.UserId,
            });

            if (data.Result == true)
            {
                //判断是否流程结束
                if (!info.FlowData.CanComplete(data))
                {
                    var nextUser = Core.UserManager.GetModel(toUserId);
                    var nextNodedata = Core.FlowNodeDataManager.CreateNextNodeData(info, nextUser, data.FlowNodeId);

                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        FormId = info.FormId,
                        InfoId = info.ID,
                        Status = FlowStatus.Doing,
                        UserId = nextNodedata.UserId,
                    });
                }
                else
                {
                    Core.FlowDataManager.Complete(info);
                }
            }
            else
            {
                var firstNodeData = info.FlowData.GetFirstNodeData();
                var newBackData = Core.FlowNodeDataManager.CreateBackNodeData(info, firstNodeData);
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = info.ID,
                    FormId = info.FormId,
                    UserId = newBackData.UserId,
                    Status = FlowStatus.Back,
                });
            }
            //TODO发布短消息通知（此流程所有参与的人都会得到通知）



            return Ok();
        }

        [HttpGet]
        public object UserList(int flowId, int nodeId, int flowDataId = 0)
        {
            if (flowId == 0)
            {
                return BadRequest("没有指定流程");
            }

            var flow = Core.FlowManager.Get(flowId);
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
                parameter.TitleId = nextNode.JobTitleId;
                parameter.UserId = nextNode.UserId;
                if (nextNode.LimitMode == DepartmentLimitMode.Assign)
                {
                    parameter.DepartmentIds = nextNode.DepartmentIds;
                }
                else if (nextNode.LimitMode == DepartmentLimitMode.Sender)
                {
                    if (nodeId == 0)
                    {
                        parameter.DepartmentId = CurrentUser.DepartmentId;
                    }
                    else if (flowDataId > 0)
                    {
                        var flowData = Core.FlowDataManager.Get(flowDataId);
                        var senderNodeData = flowData.GetFirstNodeData();
                        var user = Core.UserManager.GetModel(senderNodeData.UserId);
                        parameter.DepartmentId = user.DepartmentId;
                    }
                }
            }

            var users = Core.UserManager.GetList(parameter);
            return users.Select(e => new UserViewModel
            {
                ID = e.ID,
                Username = e.Username,
                RealName = e.RealName,
                Department = e.Department == null ? null : e.Department.Name,
                DepartmentId = e.DepartmentId,
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
