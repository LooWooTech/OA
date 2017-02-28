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
    /// 部门管理API
    /// </summary>
    public class DepartmentController : LoginControllerBase
    {
        /// <summary>
        /// 作用：新建部门或者更新部门信息 ID>0为更新
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:23:26
        /// 修改时间：2017年2月24日09:19:00
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Department department)
        {
            TaskName = "保存部门";
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return BadRequest($"{TaskName}:未获取部门信息、部门信息不能为空");
            }
            if (Core.DepartmentManager.Exist(department.Name))
            {
                return BadRequest($"{TaskName}:系统中已存在部门名称为{department.Name}");
            }
            if (department.ParentID > 0)
            {
                var parent = Core.DepartmentManager.Get(department.ParentID);
                if (parent == null)
                {
                    return BadRequest($"{TaskName}:未找到上级部门信息");
                }
            }
            if (department.ID > 0)
            {
                if (!Core.DepartmentManager.Edit(department))
                {
                    return BadRequest($"{TaskName}:未找不更新的部门信息");
                }
            }
            else
            {
                var id = Core.DepartmentManager.Save(department);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存部门失败");
                }
            }

            return Ok(department);
        }

        /// <summary>
        /// 作用：获取部门列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:50:31
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Department> List()
        {
            var list = Core.DepartmentManager.GetList();
            return list;
        }

        /// <summary>
        /// 作用：获取某一个部门信息  未找到返回NOTFound  找到OK(Department)
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:23:13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.DepartmentManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：删除部门
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:29:53
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.DepartmentManager.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
