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
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info.FlowDataId == 0)
            {
                //创建flowdata
                info.FlowData = Core.FlowDataManager.Create(info);
                info.FlowDataId = info.FlowData.ID;

                //如果第一次提交，则先创建flowdata
                var currentUser = Core.UserManager.Get(CurrentUser.ID);
                data = Core.FlowNodeDataManager.CreateNextNodeData(info, currentUser, 0, data.Content, true);
            }
            else
            {
                data = Core.FlowNodeDataManager.Save(data);
            }
            //更新userforminfo的状态
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                FormId = info.FormId,
                InfoId = info.ID,
                Status = FlowStatus.Done,
                UserId = data.UserId,
                FlowNodeDataId = data.ID,
            });
            if (data.Result == true)
            {
                //判断是否流程结束
                if (!info.FlowData.CanComplete(data))
                {
                    var nextUser = Core.UserManager.Get(toUserId);
                    var nextNodedata = Core.FlowNodeDataManager.CreateNextNodeData(info, nextUser, data.FlowNodeId);

                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        FormId = info.FormId,
                        InfoId = info.ID,
                        Status = FlowStatus.Doing,
                        UserId = nextNodedata.UserId,
                        FlowNodeDataId = nextNodedata.ID,
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
                    FlowNodeDataId = newBackData.ID
                });
            }
            //TODO发布短消息通知（此流程所有参与的人都会得到通知）



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
        public IHttpActionResult BackList(int infoId, int currentFlowNodeId)
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
