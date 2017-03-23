using Loowoo.Land.OA.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    /// <summary>
    /// 流程模板
    /// </summary>
    public class FlowController : ControllerBase
    {
        /// <summary>
        /// 作用：获取某一个流程模板并包括所有流程节点信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日09:09:22
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int formId)
        {
            TaskName = "获取流程模板";
            var form = Core.FormManager.GetModel(formId);
            if (form == null)
            {
                return BadRequest(string.Format("未找到ID为{0}的表单信息", formId));
            }

            var model = Core.FlowManager.Get(form.FLowId);
            if (model == null)
            {
                return BadRequest($"{TaskName}:未获取到ID为{form.FLowId}的流程模板，请核对ID");
            }
            return Ok(model);
        }
        /// <summary>
        /// 作用：删除流程节点
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteNode(int id)
        {
            TaskName = "删除流程节点";
            if (Core.FlowNodeManager.Used(id))
            {
                return BadRequest($"{TaskName}:当前流程节点ID为{id}关联下一级节点或者系统中已存在相关流程节点记录，请核对");
            }
            if (!Core.FlowNodeManager.Delete(id))
            {
                return BadRequest($"{TaskName}:未找到需要删除的ID为{id}的流程节点信息,请核对");
            }
            return Ok();
        }
        /// <summary>
        /// 作用：获取所有流程模板列表
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日17:21:38
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Flow> List()
        {
            return Core.FlowManager.GetList();
        }
        /// <summary>
        /// 作用：保存流程节点
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日17:10:11
        /// </summary>
        /// <param name="flowNode"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SaveNode([FromBody] FlowNode flowNode)
        {
            TaskName = "保存节点";
            #region  验证数据有效逻辑
            if (flowNode == null || string.IsNullOrEmpty(flowNode.Name))
            {
                return BadRequest($"{TaskName}:未获取流程节点信息、流程节点名称不能为空，请核对");
            }
            var flow = Core.FlowManager.Get(flowNode.FlowId);
            if (flow == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点相关联的ID：{flowNode.FlowId} 流程模板，请核对");
            }
            /*
             UserId,GroupId,DepartmentId三项均可为空,但是至少有一项必填，如果不为空，需要验证是否正确
             */
            if ((flowNode.UserId + flowNode.GroupId + flowNode.DepartmentId) <= 0)
            {
                return BadRequest($"{TaskName}:用户、组、部门三项至少有一项必填");
            }
            if (flowNode.UserId > 0)
            {
                var user = Core.UserManager.Get(flowNode.UserId);
                if (user == null)
                {
                    return BadRequest($"{TaskName}:未查询到ID为{flowNode.UserId}的用户信息");
                }
            }
            if (flowNode.GroupId > 0)
            {
                var group = Core.GroupManager.Get(flowNode.GroupId);
                if (group == null)
                {
                    return BadRequest($"{TaskName}:未获取ID为{flowNode.GroupId}的组，请核对");
                }
            }
            if (flowNode.DepartmentId > 0)
            {
                var organ = Core.DepartmentManager.Get(flowNode.DepartmentId);
                if (organ == null)
                {
                    return BadRequest($"{TaskName}:未获取ID为{flowNode.DepartmentId}的部门，请核对");
                }
            }

            if (flowNode.Step > 0)
            {
                var pre = Core.FlowNodeManager.Get(flowNode.FlowId, flowNode.Step - 1);
                if (pre == null)
                {
                    return BadRequest($"{TaskName}:验证上一级流程节点，未获取为FlowID：{flowNode.FlowId};Step:{flowNode.Step-1}的流程节点");
                }
            }
            #endregion
            if (flowNode.ID > 0)
            {
                if (!Core.FlowNodeManager.Edit(flowNode))
                {
                    return BadRequest($"{TaskName}:更新流程节点失败，未找到更新ID为{flowNode.ID}的流程节点信息，请核对");
                }
            }
            else
            {
                var id = Core.FlowNodeManager.Save(flowNode);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }

            return Ok(flowNode);
        }
        /// <summary>
        /// 作用：获取满足下一节点条件的人员列表
        /// 作者：汪建龙
        /// 编写时间：2017年3月5日12:00:41
        /// </summary>
        /// <param name="currentNodeId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult NextNodeUserList(int currentNodeId)
        {
            TaskName = "获取下一个节点人员列表";
            var currentNode = Core.FlowNodeManager.Get(currentNodeId);
            if (currentNode == null)
            {
                return BadRequest($"{TaskName}:未获取Id为{currentNodeId}的流程节点信息，请核对");
            }
            var nextnode = Core.FlowNodeManager.Get(currentNode.FlowId, currentNode.Step + 1);
            if (nextnode == null)//流程为最后一步
            {
                return Ok();
            }
            var parameter = new Parameters.UserParameter { GroupId = nextnode.GroupId, DepartmentId = nextnode.DepartmentId };
            var list = Core.UserManager.Search(parameter);
            var table = new PagingResult<User>
            {
                List = list,
                Page = parameter.Page
            };
            return Ok(table);
        }








        ///// <summary>
        ///// 作用：获取表单流程模板
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月22日16:14:47
        ///// </summary>
        ///// <param name="formId">表单ID</param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult GetFlow(int formId)
        //{
        //    var model = Core.FlowManager.GetByFormId(formId);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(model);
        //}



        ///// <summary>
        ///// 作用：获取审批记录列表
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日10:00:56
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public List<FlowNodeData> NodeList(int infoId, int formId)
        //{
        //    var flowData = Core.FlowDataManager.Get(formId, infoId);
        //    if (flowData == null)
        //    {
        //        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
        //        {
        //            Content = new StringContent(string.Format("没有找到InfoID={0}&&FormID={1}的流程记录", infoId, formId)),
        //            ReasonPhrase = "Object is not found"
        //        });
        //    }
        //    var list = Core.FlowNodeDataManager.GetList(flowData.ID);
        //    return list;
        //}
        ///// <summary>
        ///// 作用：保存或者更新流程模板
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日17:30:30
        ///// </summary>
        ///// <param name="flow"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult Save([FromBody] Flow flow)
        //{
        //    TaskName = "保存流程模板";
        //    if (flow == null || string.IsNullOrEmpty(flow.Name))
        //    {
        //        return BadRequest($"{TaskName}:未获取流程模板信息，流程模板名称不能为空");
        //    }
        //    if (flow.ID > 0)
        //    {
        //        if (!Core.FlowManager.Edit(flow))
        //        {
        //            return BadRequest($"{TaskName}:未找到需要编辑的流程模板信息");
        //        }
        //    }
        //    else
        //    {
        //        var id = Core.FlowManager.Save(flow);
        //        if (id <= 0)
        //        {
        //            return BadRequest($"{TaskName}:保存流程模板失败");
        //        }
        //    }
        //    return Ok(flow);
        //}

        ///// <summary>
        ///// 作用：删除流程模板
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月28日09:29:40
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete]
        //public IHttpActionResult Delete(int id)
        //{
        //    if (Core.FlowManager.Delete(id))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("删除流程模板：删除失败，未找到删除流程模板信息");
        //}

        ///// <summary>
        ///// 作用：获取当前用户指定表单的审批记录
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月28日13:44:42
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult CurrentUserNode(int infoId, int formId)
        //{
        //    TaskName = "获取审批记录";
        //    if (CurrentUser == null)
        //    {
        //        return BadRequest($"{TaskName}:未获取当前用户信息");
        //    }
        //    var flowdata = Core.FlowDataManager.Get(formId, infoId);
        //    if (flowdata == null)
        //    {
        //        return BadRequest($"{TaskName}:未获取流程记录信息");
        //    }
        //    var flowNodeData = Core.FlowNodeDataManager.Get(flowdata.ID, CurrentUser.ID, 0);
        //    if (flowNodeData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(flowNodeData);
        //}




  


        ///// <summary>
        ///// 作用：获取第一个节点，如果流程
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月25日14:26:42
        ///// </summary>
        ///// <param name="flow"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult GetFirstNode([FromUri] Flow flow)
        //{
        //    TaskName = "获取第一个节点";
        //    if (flow == null)
        //    {
        //        return BadRequest($"{TaskName}:未获取流程模板信息");
        //    }
        //    var nodes = Core.FlowNodeManager.GetByFlowID(flow.ID);
        //    FlowNode first = null;
        //    if (nodes.Count > 0)
        //    {
        //        first = nodes.First();
        //    }
        //    else
        //    {
        //        var user = Core.UserManager.Get(CurrentUser.ID);
        //        if (user == null)
        //        {
        //            return BadRequest($"{TaskName}:未获取当前用户信息");
        //        }
        //        first = new FlowNode
        //        {
        //            FlowId = flow.ID,
        //            UserId = user.ID,
        //            DepartmentId = user.DepartmentId.HasValue ? user.DepartmentId.Value : 0,
        //            Order = 1
        //        };
        //        var id = Core.FlowNodeManager.Save(first);
        //        if (id <= 0)
        //        {
        //            return BadRequest($"{TaskName}:创建节点失败");
        //        }
        //    }
        //    return Ok(first);

        //}

        ///// <summary>
        ///// 作用：创建表单流程记录
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月25日15:52:02
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public IHttpActionResult CreateFlowData(int infoId, int formId)
        //{
        //    TaskName = "创建流程记录";
        //    if (Core.FlowDataManager.Exist(infoId, formId))
        //    {
        //        return BadRequest($"{TaskName}:系统中已存在流程记录");
        //    }
        //    var flowData = new FlowData
        //    {
        //        InfoId = infoId,
        //        FormId = formId
        //    };
        //    var id = Core.FlowDataManager.Save(flowData);
        //    if (id <= 0)
        //    {
        //        return BadRequest($"{TaskName}:创建失败");
        //    }
        //    return Ok(flowData);
        //}



    }
}
