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
                return NotFound();
            }
            return Ok(model);
        }
    }
}
