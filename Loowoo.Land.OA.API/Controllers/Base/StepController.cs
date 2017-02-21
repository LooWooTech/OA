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
                
            }
        }
    }
}
