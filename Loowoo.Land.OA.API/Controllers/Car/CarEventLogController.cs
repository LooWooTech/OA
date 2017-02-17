using Loowoo.Common;
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
    /// 车辆使用申请
    /// </summary>
    public class CarEventLogController : LoginControllerBase
    {
        /// <summary>
        /// 作用：生成用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日13:35:32
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] CarEventLog log)
        {
            TaskName = "申请用车";
            if (log == null || string.IsNullOrEmpty(log.Description))
            {
                return BadRequest($"{TaskName}:未获取申请用车信息，用车理由不能为空");
            }
            var car = Core.CarManager.Get(log.CarID);
            if (car == null)
            {
                return NotFound();
            }
            if (car.State != CarState.Unused)
            {
                return BadRequest($"{TaskName}:{car.Name}{car.State.GetDescription()}");
            }
            var user = Core.UserManager.Get(log.UserID);
            if (user == null)
            {
                return NotFound();
            }

            var id = 0;
            try
            {
                id = Core.Car_EventLogManager.Save(log);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
                return BadRequest($"{TaskName}:生成用车申请错误");
            }
            if (id <= 0)
            {
                return BadRequest($"{TaskName}:保存生成用车申请失败");
            }
            try
            {
                var flowId = Core.FlowManager.Save(new Flow { Name = log.Description, InfoID = id, InfoType = 3 });
                if (flowId <= 0)
                {
                    return BadRequest($"{TaskName}:生成审批请求失败");
                }

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName+"生成用车申请，生成审批");
                return BadRequest($"{TaskName}:生成用车申请，但是生成审批信息发生错误");
            }

            return Ok();

        }

        /// <summary>
        /// 作用：编辑用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日13:52:16
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] CarEventLog log)
        {
            TaskName = "编辑用车申请";
            if (log == null || log.ID == 0 || string.IsNullOrEmpty(log.Description))
            {
                return BadRequest($"{TaskName}:未获取用车申请、编辑用车申请ID、用车申请理由不能为空");
            }
            var car = Core.CarManager.Get(log.CarID);
            if (car == null)
            {
                return NotFound();
            }
            if (car.State != CarState.Unused)
            {
                return BadRequest($"{TaskName}:{car.Name}{car.State.GetDescription()}");
            }
            var user = Core.UserManager.Get(log.UserID);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                if (!Core.Car_EventLogManager.Edit(log))
                {
                    return BadRequest($"{TaskName}:未找到编辑的用车申请信息");
                }
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
                return BadRequest($"{TaskName}:编辑用车申请发生错误");
            }
            var flow = Core.FlowManager.Get(log.ID, 3);
            if (flow == null)
            {
                var flowId = Core.FlowManager.Save(new Flow { Name = log.Description, InfoID = log.ID, InfoType = 3 });
                if (flowId <= 0)
                {
                    return BadRequest($"{TaskName}:生成审批信息失败");
                }
            }

            return Ok();
        }

        /// <summary>
        /// 作用：撤销用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日13:55:45
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.Car_EventLogManager.Delete(id);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "撤销用车申请");
            }
        }



    }
}
