using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class TaskController : LoginControllerBase
    {
        /// <summary>
        /// 作用：创建任务
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日15:26:42
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Task task)
        {
            TaskName = "任务登记";
            if (task == null || string.IsNullOrEmpty(task.Title) || string.IsNullOrEmpty(task.Content))
            {
                return BadRequest($"{TaskName}：任务信息为空、任务标题为空、任务内容为空");
            }
            if (task.CreatorID == 0)
            {
                return BadRequest($"{TaskName}：任务创建用户ID为0");
            }
            var user = Core.UserManager.Get(task.CreatorID);
            if (user == null)
            {
                return BadRequest($"{TaskName}：当前系统未找到创建者信息");
            }
            var id = 0;
            try
            {
                id=  Core.TaskManager.Save(task);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:任务创建失败");
                }

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
                return BadRequest($"{TaskName}发生错误");
            }
            try
            {
                var flowId = Core.FlowManager.Save(new Flow { Name = task.Title, InfoID = id, InfoType = 2 });
                if (flowId > 0)
                {
                    return Ok();
                }
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, $"{TaskName}-Flow信息表创建");
            }
            return BadRequest($"{TaskName}:任务保存成功，但是信息关联表FLOW保存失败");

        }

        /// <summary>
        /// 作用：任务编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日15:59:57
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] Task task)
        {
            TaskName = "任务编辑";
            if (task == null || task.ID == 0)
            {
                return BadRequest($"{TaskName}:请核对任务内容，以及需要编辑任务的ID");
            }
            try
            {
                if (Core.TaskManager.Edit(task))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("编辑失败，未找到编辑的任务信息");
                }
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
            return BadRequest($"{TaskName}:编辑发生错误");
        }

        /// <summary>
        /// 作用：获取任务信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:03:09
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            TaskName = "获取任务";
            if (id <= 0)
            {
                return BadRequest($"{TaskName}:ID参数不正确");
            }
            var task = Core.TaskManager.Get(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        /// <summary>
        /// 作用：任务删除
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:23:21
        /// </summary>
        /// <param name="id">任务ID</param>
        [HttpDelete]
        public void Delete(int id)
        {
            TaskName = "任务删除";
            try
            {
                Core.TaskManager.Delete(id);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }
        }
        /// <summary>
        /// 作用：任务查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:22:54
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Task> Search(int page,int rows)
        {
            var parameter = new TaskParameter
            {
                Page = new Common.PageParameter(page, rows)
            };
            var list = Core.TaskManager.Search(parameter);
            return list;
        }
    }
}
