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
        /// 作用：新建部门
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:23:26
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Department department)
        {
            TaskName = "新建部门";
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return BadRequest($"{TaskName}:未获取部门信息、部门信息不能为空");
            }
            if (department.ParentID > 0)
            {
                var parent = Core.DepartmentManager.Get(department.ParentID);
                if (parent == null)
                {
                    return BadRequest($"{TaskName}:未找到上级部门信息");
                }
            }
            var id = Core.DepartmentManager.Save(department);
            if (id > 0)
            {
                return Ok(department);
            }
            return BadRequest($"{TaskName}:新建部门失败");
           
        }

        /// <summary>
        /// 作用：编辑部门
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:42:00；
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] Department department)
        {
            TaskName = "编辑部门";
            if (department == null || string.IsNullOrEmpty(department.Name) || department.ID == 0)
            {
                return BadRequest($"{TaskName}:未获取编辑部门信息、编辑部门ID不能为0、部门名称不能为空");
            }
            if (department.ParentID > 0)
            {
                var parent = Core.DepartmentManager.Get(department.ParentID);
                if (parent == null)
                {
                    return BadRequest($"{TaskName}:未找到上级部门信息");
                }
            }
            if (Core.DepartmentManager.Edit(department))
            {
                return Ok(department);
            }
            return BadRequest($"{TaskName}:未找到编辑部门信息");
                
        }

        /// <summary>
        /// 作用：获取部门列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:50:31
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Department> GetList()
        {
            var list = Core.DepartmentManager.GetList();
            return list;
        }
    }
}
