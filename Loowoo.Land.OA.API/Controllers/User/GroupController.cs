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
        public List<Group> GetList()
        {
            return Core.GroupManager.GetList();
        }

        /// <summary>
        /// 作用：创建组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:54:44
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Group group)
        {
            TaskName = "创建组";
            if (group == null || string.IsNullOrEmpty(group.Name))
            {
                return BadRequest($"{TaskName}:无法获取组信息，以及组名称不能为空");
            }
            try
            {
                var id = Core.GroupManager.Save(group);
                if (id > 0)
                {
                    return Ok();
                }
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}失败");
        }

        /// <summary>
        /// 作用：编辑组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日17:09:04
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] Group group)
        {
            TaskName = "编辑组";
            if (group == null || group.ID == 0)
            {
                return BadRequest($"{TaskName}:编辑组信息为空、组ID不能为0");
            }

            try
            {
                if (Core.GroupManager.Edit(group))
                {
                    return Ok();
                }

                return BadRequest($"{TaskName}:编辑失败");

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}:编辑发生错误");
        }

        /// <summary>
        /// 作用：通过ID获取组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日17:17:12
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var group = Core.GroupManager.Get(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        


        

    }
}
