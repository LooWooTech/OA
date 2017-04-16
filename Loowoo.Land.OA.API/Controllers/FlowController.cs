using Loowoo.Land.OA.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FlowController : ControllerBase
    {
        [HttpGet]
        public IHttpActionResult Model(int formId)
        {
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
           
            if (flowNode.PrevId > 0)
            {
                var pre = Core.FlowNodeManager.Get(flowNode.PrevId);
                if (pre == null)
                {
                    return BadRequest($"{TaskName}:未找到上一节点信息,请核对");
                }
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

        [HttpPost]
        public IHttpActionResult Save([FromBody] Flow flow)
        {
            Core.FlowManager.Save(flow);
            return Ok(flow);
        }

    }
}
