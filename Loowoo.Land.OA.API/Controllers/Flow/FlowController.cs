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



        /// <summary>
        /// 作用：获取审批记录列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日10:00:56
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public List<FlowNodeData> NodeList(int infoId,int formId)
        {
            var flowData = Core.FlowDataManager.Get(formId, infoId);
            if (flowData == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("没有找到InfoID={0}&&FormID={1}的流程记录", infoId, formId)),
                    ReasonPhrase = "Object is not found"
                });
            }
            var list = Core.FlowNodeDataManager.GetList(flowData.ID);
            return list;
        }
        /// <summary>
        /// 作用：保存或者更新流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日17:30:30
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Flow flow)
        {
            TaskName = "保存流程模板";
            if (flow == null || string.IsNullOrEmpty(flow.Name))
            {
                return BadRequest($"{TaskName}:未获取流程模板信息，流程模板名称不能为空");
            }
            if (flow.ID > 0)
            {
                if (!Core.FlowManager.Edit(flow))
                {
                    return BadRequest($"{TaskName}:未找到需要编辑的流程模板信息");
                }
            }
            else
            {
                var id = Core.FlowManager.Save(flow);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存流程模板失败");
                }
            }
            return Ok(flow);
        }
        /// <summary>
        /// 作用：获取某一个模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日09:09:22
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            TaskName = "获取流程模板";
            var model = Core.FlowManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        /// <summary>
        /// 作用：删除流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日09:29:40
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.FlowManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest("删除流程模板：删除失败，未找到删除流程模板信息");
        }

        /// <summary>
        /// 作用：获取当前用户指定表单的审批记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日13:44:42
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CurrentUserNode(int infoId,int formId)
        {
            TaskName = "获取审批记录";
            if (CurrentUser == null)
            {
                return BadRequest($"{TaskName}:未获取当前用户信息");
            }
            var flowdata = Core.FlowDataManager.Get(formId, infoId);
            if (flowdata == null)
            {
                return BadRequest($"{TaskName}:未获取流程记录信息");
            }
            var flowNodeData = Core.FlowNodeDataManager.Get(flowdata.ID, CurrentUser.ID);
            if (flowNodeData == null)
            {
                return NotFound();
            }
            return Ok(flowNodeData);
        }



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
