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

        ///// <summary>
        ///// 作用：创建表单流程记录或者更新
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月25日13:01:12
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IHttpActionResult Save([FromBody]FlowData data)
        //{
        //    TaskName = "保存表单流程记录";
        //    if (data == null)
        //    {
        //        return BadRequest($"{TaskName}:未获取表单流程记录信息");
        //    }
        //    var missive = Core.MissiveManager.Get(data.InfoId);
        //    if (missive == null)
        //    {
        //        return BadRequest($"{TaskName}:未找到Info信息");
        //    }
        //    var form = Core.FormManager.Get(data.FormId);
        //    if (form == null)
        //    {
        //        return BadRequest($"{TaskName}:未找到表单信息");
        //    }

        //}
    }
}
