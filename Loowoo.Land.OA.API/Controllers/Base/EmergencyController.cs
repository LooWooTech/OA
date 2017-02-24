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
    /// 缓急管理
    /// </summary>
    public class EmergencyController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存或更新缓急名称
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:04:03
        /// </summary>
        /// <param name="emergency"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Emergency emergency)
        {
            TaskName = "保存缓急";
            if (emergency == null || string.IsNullOrEmpty(emergency.Name))
            {
                return BadRequest($"{TaskName}:未获取缓急信息、缓急名称不能为空");
            }
            if (Core.EmergencyManager.Exist(emergency.Name))
            {
                return BadRequest($"{TaskName}:系统中已存在相同名称的缓急");
            }
            if (emergency.ID > 0)
            {
                if (!Core.EmergencyManager.Edit(emergency))
                {
                    return BadRequest($"{TaskName}:未找到需要编辑的缓急信息");
                }
            }
            else
            {
                var id = Core.EmergencyManager.Save(emergency);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(emergency);
        }

        /// <summary>
        /// 作用：获取缓急信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:07:22
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.EmergencyManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：获取缓急列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:08:10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Emergency> List()
        {
            var list = Core.EmergencyManager.GetList();
            return list;
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.EmergencyManager.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
