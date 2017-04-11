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
        /// <summary>
        /// 作用：获取当前用户指定表单的审批记录
        /// 作者：汪建龙
        /// 编写时间：2017年3月5日12:19:07
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CurrentUserNode(int infoId, int formId)
        {
            TaskName = "获取用户表单审批记录";
            if (CurrentUser == null)
            {
                return BadRequest($"{TaskName}:未获取当前用户信息");
            }
            var flowData = Core.FlowDataManager.Get(formId, infoId);
            if (flowData == null)
            {
                return BadRequest($"{TaskName}:未获取FormID为{formId}并且InfoId为{infoId}的流程记录");
            }

            var flownodedata = Core.FlowNodeDataManager.Get(flowData.ID, CurrentUser != null ? CurrentUser.ID : 0);
            return Ok(flownodedata);
        }
        /// <summary>
        /// 作用：提交流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日14:57:13
        /// 具体操作如下：
        /// 1、验证flownodedata是否为null;
        /// 2、验证flownodedata的userId、flowdataId、flownodeId是否为有效；
        /// 3、验证result  是否hasValue;
        /// 4、根据当前审核结果获取下一个流程节点，通过:生成下一个流程节点记录；不通过：生成上一个流程节点记录，如果为流程节点第一个或者流程最后，返回null
        /// 5、保存flownodedate,更新UserForm待办事情表 
        /// 6、当下一个流程节点不为null时，保存并生成下一个流程审核的工作表
        /// 7、发布动态以及短消息
        /// </summary>
        /// <param name="flownodedata"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Submit([FromBody] FlowNodeData flownodedata)
        {
            TaskName = "保存流程节点记录";
            #region 验证数据正确性
            if (flownodedata == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点记录信息");
            }
            var user = Core.UserManager.Get(flownodedata.UserId);
            if (user == null)
            {
                return BadRequest($"{TaskName}:未找到UserID{flownodedata.UserId}的用户信息");
            }
            var flowdata = Core.FlowDataManager.Get(flownodedata.FlowDataId);
            if (flowdata == null)
            {
                return BadRequest($"{TaskName}:未找到流程记录信息");
            }
            if (!flownodedata.Result.HasValue)
            {
                return BadRequest($"{TaskName}：未获取审核结果，请核对审核结果");
            }
            var flowNode = Core.FlowNodeManager.Get(flownodedata.FlowNodeId);
            if (flowNode == null)
            {
                return BadRequest($"{TaskName}:未获取FlowNode流程节点信息");
            }

            #endregion

            #region 处理逻辑 生成新的FlowNodeData节点记录
            flownodedata.UpdateTime = DateTime.Now;
            FlowNodeData nextFlowNodeData = null;
            if (flownodedata.Result.Value == false)//如果提交不同意
            {
                flownodedata.UserId = 0;
                /*
                    获取上一个审核通过的最新时间的流程节点记录，指定flowdataID、指定flownodID,同时需要Result.hasvalue ==true    
                */
                var preflownodedata = Core.FlowNodeDataManager.GetPreFlowNodeData(flownodedata.FlowDataId, flownodedata.FlowNodeId);
                if (preflownodedata == null)
                {
                    return BadRequest($"{TaskName}:获取上一个审核流程节点记录失败");
                }
                nextFlowNodeData = new FlowNodeData
                {
                    FlowDataId = preflownodedata.FlowDataId,
                    ParentId = preflownodedata.ParentId,
                    UserId = preflownodedata.UserId,
                    DepartmentId = preflownodedata.DepartmentId,
                    FlowNodeId = preflownodedata.FlowNodeId
                };
            }
            else//审核通过
            {
                var nextflowNode = Core.FlowNodeManager.GetNext(flownodedata.FlowNodeId);
                if (nextflowNode != null)
                {
                    nextFlowNodeData = new FlowNodeData
                    {
                        FlowDataId = flownodedata.FlowDataId,
                        ParentId = flownodedata.ParentId,
                        UserId = nextflowNode.UserId,
                        DepartmentId = nextflowNode.DepartmentId,
                        FlowNodeId = nextflowNode.ID
                    };
                }
            }
            #endregion


            #region 保存flownodedata,并且生成新的flownodedata
            if (!Core.FlowNodeDataManager.Edit(flownodedata))
            {
                return BadRequest($"{TaskName}:未找到更新的流程节点记录信息");
            }
            if (nextFlowNodeData != null)
            {
                var nextID = Core.FlowNodeDataManager.Save(nextFlowNodeData);
                if (nextID <= 0)
                {
                    return BadRequest($"{TaskName}:保存下一步流程节点记录信息失败");
                }
            }
            #endregion

            #region  动态  短消息
            if (nextFlowNodeData != null)
            {
                //保存动态
                var feedId = Core.FeedManager.Save(new Feed
                {
                    FormId = flowdata.FormId,
                    InfoId = flowdata.InfoId,
                    FromUserId = flownodedata.UserId,
                    ToUserId = flownodedata.UserId
                });
                if (feedId > 0)
                {
                    //发布消息
                    Core.MessageManager.Save(new Message
                    {
                        Content = flownodedata.Result.Value ? "通过审核" : "退回",
                        FormUserId = flownodedata.UserId,
                        ToUserId = nextFlowNodeData.UserId,
                        FeedId = feedId
                    });
                }

            }
            #endregion

            return Ok(flownodedata);
        }
    }
}
