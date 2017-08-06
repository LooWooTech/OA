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
            Core.FlowDataManager.Cancel(info.FlowData, CurrentUser.ID);

            //撤销后更新userforminfo
            var flowNodeData = info.FlowData.GetLastNodeData(CurrentUser.ID);
            Core.UserFormInfoManager.OnCancelFlowData(info, flowNodeData);
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
        public IHttpActionResult Submit([FromBody]FlowNodeData data, int infoId, int toUserId = 0)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            if (info == null)
            {
                return BadRequest("参数错误，找不到FormInfo");
            }
            if (info.FlowDataId == 0)
            {
                if (info.Form == null)
                {
                    info.Form = Core.FormManager.GetModel(info.FormId);
                }
                //创建流程
                info.FlowData = Core.FlowDataManager.CreateFlowData(info);
                info.FlowDataId = info.FlowData.ID;
            }

            var currentNodeData = info.FlowData.GetLastNodeData(CurrentUser.ID);
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
                if (!info.FlowData.CanComplete(currentNodeData))
                {
                    var nextNodedata = Core.FlowDataManager.SubmitToUser(info.FlowData, toUserId);
                    info.FlowStep = nextNodedata.FlowNodeName;
                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        InfoId = info.ID,
                        Status = FlowStatus.Doing,
                        UserId = nextNodedata.UserId,
                    });
                    Core.FeedManager.Save(new Feed
                    {
                        InfoId = info.ID,
                        Action = UserAction.Submit,
                        FromUserId = currentNodeData.UserId,
                        ToUserId = nextNodedata.UserId,
                        Type = FeedType.Flow,
                        Title = info.Title,
                        Description = currentNodeData.Content
                    });
                }
                else
                {
                    Core.FlowDataManager.Complete(info);
                    var userIds = Core.UserFormInfoManager.GetUserIds(infoId);
                    foreach (var uid in userIds)
                    {
                        Core.FeedManager.Save(new Feed
                        {
                            InfoId = info.ID,
                            Action = UserAction.Submit,
                            FromUserId = currentNodeData.UserId,
                            ToUserId = uid,
                            Type = FeedType.Flow,
                            Title = info.Title,
                            Description = currentNodeData.Content
                        });
                    }
                }
            }
            else
            {
                var flow = Core.FlowManager.Get(info.Form.FLowId);
                if (flow.CanBack)
                {
                    var nextNodeData = Core.FlowDataManager.SubmitToBack(info.FlowData);
                    info.FlowStep = nextNodeData.FlowNodeName;
                    Core.UserFormInfoManager.Save(new UserFormInfo
                    {
                        InfoId = info.ID,
                        UserId = nextNodeData.UserId,
                        Status = FlowStatus.Back,
                    });
                }
                else
                {
                    //如果不可以退回，则直接结束流程
                    info.FlowData.Completed = true;
                    Core.FlowDataManager.Save(info.FlowData);
                }
            }
            return Ok(info.FlowData);
        }

        [HttpGet]
        public object UserList(int flowNodeDataId = 0, int flowId = 0, int flowStep = 1)
        {
            if (flowNodeDataId == 0 && flowId == 0)
            {
                throw new ArgumentException("缺少参数FlowNodeDataId或FlowId");
            }

            FlowNode nextNode = null;
            FlowData flowData = null;
            if (flowNodeDataId > 0)
            {
                var flowNodeData = Core.FlowNodeDataManager.GetModel(flowNodeDataId);
                flowData = Core.FlowDataManager.Get(flowNodeData.FlowDataId);
                nextNode = flowData.Flow.GetNextStep(flowNodeData.FlowNodeId);
            }
            else
            {
                var flow = Core.FlowManager.Get(flowId);
                nextNode = flow.GetStep(flowStep);
            }

            return Core.FlowNodeManager.GetUserList(nextNode, flowData).Select(e => new UserViewModel(e));
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
