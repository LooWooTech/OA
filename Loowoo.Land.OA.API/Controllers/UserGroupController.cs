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
    /// 用户与组管理
    /// </summary>
    public class UserGroupController : ControllerBase
    {
        /// <summary>
        /// 作用：保存用户组
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日09:19:48
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody]UserGroup model)
        {
            TaskName = "保存管理用户组";
            if (model == null || model.UserID == 0 || model.GroupID == 0)
            {
                return BadRequest($"{TaskName}:没有获取用户组信息，用户ID不能为0、组ID不能为0");
            }
            var user = Core.UserManager.Get(model.UserID);
            if (user == null)
            {
                return NotFound();
            }
            var group = Core.UserManager.Get(model.GroupID);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                var id = Core.UserGroupManager.Save(model);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return Ok();

        }
        /// <summary>
        /// 作用：编辑部门组信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日09:39:14
        /// </summary>
        /// <param name="user_group"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody]UserGroup user_group)
        {
            TaskName = "编辑用户组";
            if (user_group == null || user_group.ID == 0)
            {
                return BadRequest($"{TaskName}:没有获取用户组信息、ID信息");
            }
            var user = Core.UserManager.Get(user_group.UserID);
            if (user == null)
            {
                return NotFound();
            }
            var group = Core.GroupManager.Get(user_group.GroupID);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                if (Core.UserGroupManager.Edit(user_group))
                {
                    return Ok();
                }
                return BadRequest($"{TaskName}:失败,未找到相关信息");

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}:错误");
        }

        /// <summary>
        /// 作用：删除用户部门关系
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日09:43:37
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                Core.UserGroupManager.Delete(id);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, "删除用户组关系");
            }
        }


    }
}
