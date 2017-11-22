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
            var flowNodeData = info.FlowData.GetUserLastNodeData(CurrentUser.ID);
            if (!Core.FlowDataManager.CanCancel(info.FlowData, flowNodeData))
            {
                return BadRequest("无法撤销");
            }

            Core.FlowDataManager.Cancel(info.FlowData, flowNodeData);
            return Ok();
        }

        /// <summary>
        /// 获取审核数据
        /// </summary>
        [HttpGet]
        public object Model(int id = 0, int infoId = 0)
        {
            if (id == 0 && infoId == 0)
            {
                throw new Exception("缺少参数ID或InfoID");
            }
            FlowData flowData = null;
            if (id > 0)
            {
                flowData = Core.FlowDataManager.Get(id);
            }
            else if (infoId > 0)
            {
                var info = Core.FormInfoManager.GetModel(infoId);
                flowData = info.FlowData;
            }
            if (flowData == null)
            {
                return BadRequest("参数不正确，没有获取到流程数据");
            }
            var flowNodeData = flowData.GetUserLastNodeData(CurrentUser.ID);
            var lastNodeData = flowData.GetLastNodeData();
            return new
            {
                flowData,
                flowNodeData = lastNodeData,
                freeFlowNodeData = lastNodeData.GetLastFreeNodeData(CurrentUser.ID),
                canBack = Core.FlowDataManager.CanBack(flowData),
                canSubmitFlow = Core.FlowDataManager.CanSubmit(flowData, flowNodeData),
                canComplete = Core.FlowDataManager.CanComplete(flowData.Flow, lastNodeData),
                canSubmitFreeFlow = Core.FreeFlowDataManager.CanSubmit(flowData, CurrentUser.ID),
            };
        }

        [HttpPost]
        public IHttpActionResult Submit([FromBody]FlowNodeData data, int infoId, int nextFlowNodeId = 0, int toUserId = 0)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("参数错误，找不到FormInfo");
            }
            if (info.FlowDataId == 0)
            {
                throw new Exception("该信息还是草稿状态，无法提交");
            }
            var currentNodeData = Core.FlowNodeDataManager.GetModel(data.ID);
            if (currentNodeData == null || currentNodeData.UserId != CurrentUser.ID)
            {
                return BadRequest("参数错误，找不到当前流程");
            }
            if (currentNodeData.Result.HasValue)
            {
                return BadRequest("无法提交");
            }
            currentNodeData.Content = data.Content;
            currentNodeData.Result = data.Result;
            Core.FlowNodeDataManager.Save(currentNodeData);

            //更新userforminfo的状态
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = info.ID,
                Status = FlowStatus.Done,
                UserId = CurrentUser.ID,
            });

            if (currentNodeData.Result == true)
            {
                //判断是否流程结束
                if (!Core.FlowDataManager.CanComplete(info.FlowData.Flow, currentNodeData))
                {
                    var nextNodedata = Core.FlowDataManager.SubmitToUser(info.FlowData, toUserId, nextFlowNodeId);
                    info.FlowStep = nextNodedata.FlowNodeName;
                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        InfoId = info.ID,
                        Status = FlowStatus.Doing,
                        UserId = nextNodedata.UserId,
                    });

                    var feed = new Feed
                    {
                        InfoId = info.ID,
                        Action = UserAction.Submit,
                        FromUserId = currentNodeData.UserId,
                        ToUserId = nextNodedata.UserId,
                        Type = FeedType.Flow,
                        Title = info.Title,
                        Description = currentNodeData.Content
                    };
                    Core.FeedManager.Save(feed);
                    Core.MessageManager.Add(feed);
                }
                else
                {
                    Core.FlowDataManager.Complete(info);
                    var userIds = Core.UserFormInfoManager.GetUserIds(infoId);
                    foreach (var uid in userIds)
                    {
                        var feed = new Feed
                        {
                            InfoId = info.ID,
                            Action = UserAction.Submit,
                            FromUserId = currentNodeData.UserId,
                            ToUserId = uid,
                            Type = FeedType.Flow,
                            Title = info.Title,
                            Description = currentNodeData.Content
                        };
                        Core.FeedManager.Save(feed);
                    }
                    Core.MessageManager.Add(new Message { Content = info.Title, InfoId = info.ID, CreatorId = currentNodeData.UserId }, userIds);
                }
            }
            else
            {
                var flow = Core.FlowManager.Get(info.Form.FLowId);
                if (flow.CanBack)
                {
                    var backNodeData = Core.FlowDataManager.SubmitToBack(info.FlowData, currentNodeData);
                    info.FlowStep = backNodeData.FlowNodeName;
                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        InfoId = info.ID,
                        UserId = backNodeData.UserId,
                        Status = FlowStatus.Back,
                    });

                    var feed = new Feed
                    {
                        Action = UserAction.Submit,
                        Type = FeedType.Flow,
                        InfoId = info.ID,
                        FromUserId = CurrentUser.ID,
                        ToUserId = backNodeData.UserId,
                        Title = "[退回流程]" + info.Title,
                    };
                    Core.FeedManager.Save(feed);
                    Core.MessageManager.Add(feed);
                }
                else
                {
                    //如果不可以退回，则直接结束流程
                    info.FlowData.Completed = true;
                    Core.FlowDataManager.Save(info.FlowData);
                }
            }
            return Ok(data);
        }

        [HttpGet]
        public object UserList(int flowNodeId = 0, int flowDataId = 0, int flowId = 0, int flowStep = 1)
        {
            if (flowNodeId == 0 && flowId == 0)
            {
                throw new ArgumentException("缺少参数flowNodeId或FlowId");
            }

            FlowNode flowNode = null;
            FlowData flowData = null;
            if (flowNodeId > 0)
            {
                flowNode = Core.FlowNodeManager.Get(flowNodeId);
            }
            else if (flowId > 0)
            {
                var flow = Core.FlowManager.Get(flowId);
                flowNode = flow.GetStep(flowStep);
            }
            if (flowDataId > 0 && flowNode.LimitMode == DepartmentLimitMode.Self)
            {
                flowData = Core.FlowDataManager.Get(flowDataId);
            }
            return Core.FlowNodeManager.GetUserList(flowNode, flowData).Select(e => new UserViewModel(e));
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
