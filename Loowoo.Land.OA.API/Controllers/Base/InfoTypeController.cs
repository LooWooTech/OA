using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class InfoTypeController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存或者更新消息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:41:34
        /// </summary>
        /// <param name="infoType"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] InfoType infoType)
        {
            TaskName = "保存类型";
            if (infoType == null || string.IsNullOrEmpty(infoType.Name))
            {
                return BadRequest($"{TaskName}:未获取类型信息、类型名称不能为空");
            }
            if (infoType.ID > 0)
            {
                if (!Core.InfoTypeManager.Edit(infoType))
                {
                    return BadRequest($"{TaskName}:未找到需要更新的类型");
                }
            }
            else
            {
                var id = Core.InfoTypeManager.Save(infoType);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(infoType);

        }

        /// <summary>
        /// 作用：单独获取某一个信息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:42:44
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public InfoType Get(int id)
        {
            var info = Core.InfoTypeManager.Get(id);
            return info;
        }

        /// <summary>
        /// 作用：删除消息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:48:49
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            TaskName = "删除消息类型";
            if (Core.InfoTypeManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest($"{TaskName}:未找到相关信息或者已删除");
        }

        /// <summary>
        /// 作用：获取所有有效的消息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:49:45
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<InfoType> GetList()
        {
            var list = Core.InfoTypeManager.Get();
            return list;
        }
    }
}
