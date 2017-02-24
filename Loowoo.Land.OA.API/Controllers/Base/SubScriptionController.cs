using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class SubScriptionController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存或者更新订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:23:51
        /// </summary>
        /// <param name="scription"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody]Subscription scription)
        {
            TaskName = "保存订阅类型";
            if (scription == null || string.IsNullOrEmpty(scription.Name))
            {
                return BadRequest($"{TaskName}:未获取订阅类型信息，订阅类型名称不能为空");
            }
            if (Core.SubScriptionManager.Exist(scription.Name))
            {
                return BadRequest($"{TaskName}:系统中已存在相同名称的订阅类型");
            }
            if (scription.ID > 0)
            {
                if (!Core.SubScriptionManager.Edit(scription))
                {
                    return BadRequest($"{TaskName}:未找到编辑的订阅类型信息");
                }
            }
            else
            {
                var id = Core.SubScriptionManager.Save(scription);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(scription);
        }
        /// <summary>
        /// 作用：获取订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:25:36
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.SubScriptionManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：删除订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:27:10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.SubScriptionManager.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
        /// <summary>
        /// 作用：获取订阅类型列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:30:17
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Subscription> List()
        {
            var list = Core.SubScriptionManager.GetList();
            return list;
        }
    }
}
