using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    /// <summary>
    /// 流程模板
    /// </summary>
    public class FlowController : LoginControllerBase
    {
        /// <summary>
        /// 作用：获取表单流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日16:14:47
        /// </summary>
        /// <param name="formId">表单ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFlow(int formId)
        {
            var model= Core.FlowManager.GetByFormId(formId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：获取第一个节点，如果流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日14:26:42
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFirstNode([FromUri] Flow flow)
        {
            TaskName = "获取第一个节点";
            if (flow == null)
            {
                return BadRequest($"{TaskName}:未获取流程模板信息");
            }
            var nodes = Core.FlowNodeManager.GetByFlowID(flow.ID);
            FlowNode first = null;
            if (nodes.Count > 0)
            {
                first = nodes.First();
            }
            else
            {
                var user = Core.UserManager.Get(CurrentUser.ID);
                if (user == null)
                {
                    return BadRequest($"{TaskName}:未获取当前用户信息");
                }
                first = new FlowNode
                {
                    FlowId = flow.ID,
                    UserId = user.ID,
                    DepartmentId = user.DepartmentId.HasValue ? user.DepartmentId.Value : 0,
                    Order = 1
                };
                var id = Core.FlowNodeManager.Save(first);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:创建节点失败");
                }
            }
            return Ok(first);

        }

        /// <summary>
        /// 作用：创建表单流程记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日15:52:02
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CreateFlowData(int infoId, int formId)
        {
            TaskName = "创建流程记录";
            if (Core.FlowDataManager.Exist(infoId, formId))
            {
                return BadRequest($"{TaskName}:系统中已存在流程记录");
            }
            var flowData = new FlowData
            {
                InfoId = infoId,
                FormId = formId
            };
            var id = Core.FlowDataManager.Save(flowData);
            if (id <= 0)
            {
                return BadRequest($"{TaskName}:创建失败");
            }
            return Ok(flowData);
        }


        
    }
}
