using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class StepController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存或者更新流程步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:56:08
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Step step)
        {
            TaskName = "保存审批步骤";
            if (step == null || string.IsNullOrEmpty(step.Name))
            {
                return BadRequest($"{TaskName}:未获取审批流程步骤信息、步骤名称不能为空");
            }
            if (step.ID > 0)
            {
                if (!Core.StepManager.Edit(step))
                {
                    return BadRequest($"{TaskName}:更新流程失败，未找到需要更新的流程");
                }
            }
            else
            {
                var id = Core.StepManager.Save(step);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存流程失败");
                }
            }
            return Ok(step);

        }
        /// <summary>
        /// 作用：获取某一类信息类型的审批流程列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:57:07
        /// </summary>
        /// <param name="infoID"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Step> GetList(int infoID)
        {
            var list = Core.StepManager.GetList(infoID);
            return list;
        }
        /// <summary>
        /// 作用：通过当前流程顺序号获取下一步流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:59:19
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int stepId)
        {
            TaskName = "获取下一步流程";
            var currentStep = Core.StepManager.Get(stepId);
            if (currentStep == null)
            {
                return BadRequest($"{TaskName}:未获取当前审批流程信息");
            }

        }
    }
}
