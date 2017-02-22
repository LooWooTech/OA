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
    /// 流程——头
    /// </summary>
    public class FlowController : LoginControllerBase
    {
        /// <summary>
        /// 作用：获取表单流程头信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日16:14:47
        /// </summary>
        /// <param name="formId">表单ID</param>
        /// <returns></returns>
        [HttpGet]
        public Flow GetByFormId(int formId)
        {
            return Core.FlowManager.GetByFormId(formId);
        }
    }
}
