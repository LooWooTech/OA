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
    public class DepartmentController : ControllerBase
    {
        [HttpPost]
        public IHttpActionResult Save([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return BadRequest("未获取部门信息、部门信息不能为空");
            }
            Core.DepartmentManager.Save(department);
            return Ok(department);
        }

        [HttpGet]
        public object List()
        {
            return Core.DepartmentManager.GetList().Select(e => new
            {
                e.AttendanceGroupId,
                AttendanceGroup = e.AttendanceGroup == null ? null : e.AttendanceGroup.Name,
                e.ID,
                e.ParentId,
                e.Sort,
                e.Name
            }).OrderBy(x => x.Sort);
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
                return BadRequest(string.Format("未查找到ID为{0}的部门信息", id));
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：删除部门
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:29:53
        /// 备注：删除某个部门信息的时候，需要验证部门是否
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.DepartmentManager.Used(id))
            {
                return BadRequest("删除部门：当前部门信息已在用户、审核流程节点使用");
            }
            if (Core.DepartmentManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest(string.Format("删除失败，未查找到ID为{0}的部门信息", id));
        }
    }
}
