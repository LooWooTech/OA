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
        public FlowData Model(int id)
        {
            return Core.FlowDataManager.Get(id);
        }

        [HttpGet]
        public bool CanCancel(int id)
        {
            var data = Core.FlowDataManager.Get(id);
            if (data == null)
            {
                return false;
            }

            var nodeData = data.Nodes.OrderByDescending(e => e.CreateTime).FirstOrDefault(e => e.UserId == CurrentUser.ID);
            if (nodeData == null)
            {
                return false;
            }

            var nextNodeData = data.Nodes.Where(e => e.CreateTime > nodeData.CreateTime).OrderBy(e => e.CreateTime).FirstOrDefault();
            if (nextNodeData != null && nextNodeData.Result.HasValue)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public IHttpActionResult Cancel(int id)
        {
            TaskName = "撤销节点";
            var flownodedata = Core.FlowNodeDataManager.Get(id);
            if (flownodedata == null)
            {
                return BadRequest($"{TaskName}:未获取当前撤销的流程节点信息");
            }
            if (flownodedata.FlowNode == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点信息，无法进行撤掉操作");
            }
            var next = Core.FlowNodeManager.GetNext(flownodedata.FlowNodeId);
            if (next == null)
            {
                return BadRequest($"{TaskName}:未获取下一个流程节点信息");
            }
            var nextflownodeData = Core.FlowNodeDataManager.GetFlowNodeData(flownodedata.FlowDataId, next.ID, flownodedata.Step + 1);
            if (nextflownodeData != null && nextflownodeData.Result.HasValue == false)
            {
                if (!Core.FlowNodeDataManager.Delete(nextflownodeData.ID))
                {
                    return BadRequest($"{TaskName}:撤销失败，未获取撤销的流程节点记录信息");
                }
                else
                {
                    return Ok();
                }
            }
            return BadRequest($"{TaskName}:当前流程节点记录信息不能撤销");
        }



        [HttpPost]
        public IHttpActionResult Submit(int infoId, int userId, [FromBody]FlowNodeData data)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info.FlowDataId == 0)
            {
                //创建flowdata
                info.FlowData = Core.FlowDataManager.Create(info);
                info.FlowDataId = info.FlowData.ID;

                //如果第一次提交，则先创建flowdata
                var currentUser = Core.UserManager.Get(CurrentUser.ID);
                data = Core.FlowNodeDataManager.CreateNewNodeData(info, currentUser, 0, data.Content, true);
            }
            else
            {
                data = Core.FlowNodeDataManager.Save(data);
            }
            //更新userforminfo的状态
            Core.UserFormInfoManager.Save(info.ID, info.FormId, CurrentUser.ID, FlowStatus.Done);
            //if (info.FlowData.CanComplete(data))
            //{

            //}
            //判断是否流程结束
            if (!info.FlowData.Completed)
            {
                var nextUser = Core.UserManager.Get(userId);
                var nextNodedata = Core.FlowNodeDataManager.CreateNewNodeData(info, nextUser, data.FlowNodeId);

                Core.UserFormInfoManager.Save(info.ID, info.FormId, userId, FlowStatus.Doing);
            }
            else
            {

            }
            //发布短消息通知（此流程所有参与的人都会得到通知）



            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Back(int infoId, int dataId, int backId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);

            //当前结点为不同意，退回
            var currentNodeData = info.FlowData.Nodes.FirstOrDefault(e => e.ID == dataId);
            currentNodeData.Result = false;
            Core.FlowNodeDataManager.Save(currentNodeData);
            //更新userforminfo的状态
            Core.UserFormInfoManager.Save(info.ID, info.FormId, CurrentUser.ID, FlowStatus.Done);

            //根据指定的退回节点，创建新的退回节点
            var backNodeData = info.FlowData.Nodes.FirstOrDefault(e => e.ID == backId);
            var newBackData = Core.FlowNodeDataManager.CreateBackNodeData(info, backNodeData);
            Core.UserFormInfoManager.Save(info.ID, info.FormId, backNodeData.UserId, FlowStatus.Back);
            //发送消息通知
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult UserList(int infoId, int nodeId)
        {
            FlowNode nextNode = null;
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("获取表单数据错误");
            }

            if (info.Form.FLowId > 0)
            {
                var flow = Core.FlowManager.Get(info.Form.FLowId);
                nextNode = flow.GetNextStep(nodeId);
                //如果当前还没提交过
                if (nodeId == 0)
                {
                    nextNode = flow.GetNextStep(nextNode.ID);
                }
            }

            IEnumerable<User> users = null;
            if (nextNode != null)
            {
                users = Core.UserManager.Search(new Parameters.UserParameter
                {
                    UserId = nextNode.UserId,
                    DepartmentId = nextNode.DepartmentId,
                    GroupId = nextNode.GroupId,
                });
            }
            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult BackList(int infoId, int backId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("获取表单数据错误");
            }
            var list = info.FlowData.Nodes.Where(e => e.UserId != CurrentUser.ID);
            return Ok(list);
        }

        [HttpGet]
        public bool CanComplete(int flowdataId, int nodedataId)
        {
            var flowdata = Core.FlowDataManager.Get(flowdataId);
            if (flowdata == null || flowdata.FlowId == 0) return false;

            var flow = Core.FlowManager.Get(flowdata.FlowId);
            var nodedata = flowdata.Nodes.FirstOrDefault(e => e.ID == nodedataId);
            var last = flow.GetLastNode();
            return nodedata.FlowNodeId == last.ID;
        }
    }
}
