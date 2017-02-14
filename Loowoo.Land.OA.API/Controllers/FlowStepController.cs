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
    /// 收文办理、发文领导审核
    /// </summary>
    public class FlowStepController : LoginControllerBase
    {
        /// <summary>
        /// 作用：添加审核流程步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日17:25:10
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] FlowStep step)
        {
            if (step == null || string.IsNullOrEmpty(step.Name) || !step.Result.HasValue||step.FlowID==0)
            {
                return BadRequest("审核流程信息获取失败、审核信息FlowID参数不正确、审核名称不能为空、审核结果不能为NUll");
            }
            var last = Core.FlowStepManager.GetLastStep(step.FlowID);
            step.Step = last == null ? 0 : step.Step++;
            var id = Core.FlowStepManager.Save(step);
            if (id > 0)
            {
                return Ok();
            }

            return BadRequest("保存审核流程失败！");
        }
        /// <summary>
        /// 作用：获取
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日18:34:29
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID 参数不正确!");
            }
            var step = Core.FlowStepManager.Get(id);
            if (step == null)
            {
                return NotFound();
            }
            return Ok(step);
        }

        /// <summary>
        /// 作用：删除
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日18:43:13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.FlowStepManager.Delete(id);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex,"删除审核流程");
            }
        }

    }
}
