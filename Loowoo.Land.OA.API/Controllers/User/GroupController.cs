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
    /// 组管理
    /// </summary>
    public class GroupController : ControllerBase
    {
        /// <summary>
        /// 作用：获取组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:42:28
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Group> List()
        {
            return Core.GroupManager.GetList();
        }

        /// <summary>
        /// 作用：保存或者更新编辑组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:54:44
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Group group)
        {
            TaskName = "保存组";
            if (group == null || string.IsNullOrEmpty(group.Name))
            {
                return BadRequest($"{TaskName}:无法获取组信息，以及组名称不能为空");
            }
            if (Core.GroupManager.Exist(group.Name, group.Type))
            {
                return BadRequest($"{TaskName}:系统中已存在相同类型名称的组");
            }
            if (group.ID > 0)
            {
                if (!Core.GroupManager.Edit(group))
                {
                    return BadRequest($"{TaskName}:未找到当前更新的组");
                }
            }
            else
            {
                var id = Core.GroupManager.Save(group);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(group);
        }

        /// <summary>
        /// 作用：通过ID获取组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日17:17:12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var group = Core.GroupManager.Get(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        /// <summary>
        /// 作用：删除组
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:45:58
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.GroupManager.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }

        


        

    }
}
