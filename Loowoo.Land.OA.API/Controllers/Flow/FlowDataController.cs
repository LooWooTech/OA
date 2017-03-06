using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FlowDataController : LoginControllerBase
    {
        /// <summary>
        /// 作用：提交流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日14:57:13
        /// 具体操作如下：
        /// 1、验证nodedata是否为null;
        /// 2、验证nodedata的userId、flowdataId、flownodeId是否为有效；
        /// 3、验证result  是否hasValue;
        /// 4、根据当前审核结果获取下一个流程节点，通过:生成下一个流程节点记录；不通过：生成上一个流程节点记录，如果为流程节点第一个或者流程最后，返回null
        /// 5、保存nodedate,更新UserForm待办事情表 
        /// 6、当下一个流程节点不为null时，保存并生成下一个流程审核的工作表
        /// 7、发布动态以及短消息
        /// </summary>
        /// <param name="nodedata"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Submit([FromBody] FlowNodeData nodedata)
        {
            TaskName = "保存流程节点记录";
            #region 验证数据正确性
            if (nodedata == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点记录信息");
            }
            var user = Core.UserManager.Get(nodedata.UserId);
            if (user == null)
            {
                return BadRequest($"{TaskName}:未找到UserID{nodedata.UserId}的用户信息");
            }
            var flowdata = Core.FlowDataManager.Get(nodedata.FlowDataId);
            if (flowdata == null)
            {
                return BadRequest($"{TaskName}:未找到流程记录信息");
            }
            if (!nodedata.Result.HasValue)
            {
                return BadRequest($"{TaskName}：未获取审核结果，请核对审核结果");
            }
            var flowNode = Core.FlowNodeManager.Get(nodedata.FlowNodeId);
            if (flowNode == null)
            {
                return BadRequest($"{TaskName}:未获取FlowNode流程节点信息");
            }

            #endregion

            #region  下个流程节点信息
            FlowNodeData nextFlowNodeData = GetNextNode(nodedata, flowNode);
            #endregion

            #region 保存nodedata
            nodedata.UpdateTime = DateTime.Now;

            if (!Core.FlowNodeDataManager.Edit(nodedata))
            {
                return BadRequest($"{TaskName}:未找到需要更新的流程节点记录信息");
            }
            if (flowNode.BackNodeId > 0)
            {
                UpdateUserForm(new UserForm
                {
                    UserID = nodedata.UserId,
                    FlowNodeDataID = nodedata.ID,
                    FormID = flowdata.FormId,
                    InfoID = flowdata.InfoId,
                    State = nodedata.Result.Value ? (nextFlowNodeData != null ? State.Done : State.Finish) : State.Roll
                });
            }
            #endregion

            #region 下一个流程 动态  短消息
            if (nextFlowNodeData != null)//保存下一个节点信息
            {
                var id = Core.FlowNodeDataManager.Save(nextFlowNodeData);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:生成下一个流程节点");
                }
                SaveUserForm(new UserForm
                {
                    UserID = nextFlowNodeData.UserId,
                    FlowNodeDataID = id,
                    FormID = flowdata.FormId,
                    InfoID = flowdata.InfoId,
                    State = State.None
                });
            }
            SaveMessage(new Message//发送短消息
            {
                Content = nodedata.Result.Value ? "提交审核" : "退回",
                FormUserId = nodedata.UserId,
                ToUserId = nextFlowNodeData != null ? nextFlowNodeData.UserId : 0,
                FeedId = SaveFeed(new Feed//发布动态
                {
                    FormId = flowdata.FormId,
                    InfoId = flowdata.InfoId,
                    FromUserId = nodedata.UserId,
                    ToUserId = nextFlowNodeData != null ? nextFlowNodeData.UserId : 0
                })
            });
            #endregion

            return Ok(nodedata);
        }
        /// <summary>
        /// 作用：判断当前用户能否撤销当前已提交的节点
        /// 具体操作：
        /// 1、获取ID对应的FlowNodeData；
        /// 2、
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CanCancel(int id)
        {
            TaskName = "判断能否撤销";
            var flownodeData = Core.FlowNodeDataManager.Get(id);
            if (flownodeData == null)
            {
                return BadRequest($"{TaskName}:未获取ID为：{id}的流程节点记录");
            }
            if (flownodeData.FlowNode == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点信息，无法判断能否撤销");
            }
            var next = Core.FlowNodeManager.GetNext(flownodeData.FlowNode.ID);
            if (next == null)
            {
                return BadRequest($"{TaskName}:未获取下一个流程节点信息");
            }
            var nextdata = Core.FlowNodeDataManager.Get(flownodeData.FlowDataId, flownodeData.UserId, next.ID);
            if (nextdata == null||nextdata.Result.HasValue)
            {
                return Ok(false);
            }
            return Ok(true);
        }


        [HttpGet]
        public IHttpActionResult Cancel(int id)
        {
            TaskName = "撤销节点";
            var flownodedata = Core.FlowNodeDataManager.Get(id);
            if (flownodedata == null)
            {
                //return 
            }
            return Ok();
        }

        /// <summary>
        /// 作用：获取表单流程记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日16:10:06
        /// </summary>
        /// <param name="formId">表单ID</param>
        /// <param name="infoId">公文等信息ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int formId,int infoId)
        {
            var model = Core.FlowDataManager.Get(formId, infoId);
            if (model == null)
            {
                return BadRequest("获取表单流程记录：未查询到流程记录");
            }
            model.Nodes = Core.FlowNodeDataManager.GetList(model.ID);
            return Ok(model);
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
        public IHttpActionResult CurrentUserNode(int infoId,int formId)
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
    }
}
