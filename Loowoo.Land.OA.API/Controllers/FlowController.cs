using Loowoo.Land.OA.Models;
using System.Collections.Generic;
using System.Linq;
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

        [HttpDelete]
        public void DeleteNode(int id)
        {
            Core.FlowNodeManager.Delete(id);
        }

        [HttpGet]
        public object List()
        {
            return Core.FlowManager.GetList();
        }

        [HttpPost]
        public IHttpActionResult SaveNode(FlowNode model)
        {
            TaskName = "保存节点";
            #region  验证数据有效逻辑
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest($"{TaskName}:未获取流程节点信息、流程节点名称不能为空，请核对");
            }

            var flow = Core.FlowManager.Get(model.FlowId);
            if (flow == null)
            {
                return BadRequest($"{TaskName}:未获取流程节点相关联的ID：{model.FlowId} 流程模板，请核对");
            }

            if (model.PrevId > 0)
            {
                var pre = Core.FlowNodeManager.Get(model.PrevId);
                if (pre == null)
                {
                    return BadRequest($"{TaskName}:未找到上一节点信息,请核对");
                }
            }

            #endregion
            Core.FlowNodeManager.Save(model);
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult Save([FromBody] Flow flow)
        {
            Core.FlowManager.Save(flow);
            return Ok(flow);
        }

    }
}
