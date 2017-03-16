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
    /// 表单管理
    /// </summary>
    public class FormController : ControllerBase
    {
        /// <summary>
        /// 作用：获取所有表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:29:32
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Form> GetList()
        {
            var list = Core.FormManager.GetList();
            return list;
        }

        /// <summary>
        /// 作用：保存或者更新表单 根据ID判断是增加还是更新
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:39:03
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Form form)
        {
            TaskName = "保存表单";
            if (form == null || string.IsNullOrEmpty(form.Name))
            {
                return BadRequest($"{TaskName}:未获取表单信息，表单名称不能为空");
            }
            if (form.ID > 0)
            {
                if (!Core.FormManager.Edit(form))
                {
                    return BadRequest($"{TaskName}:未找到更新的表单信息");
                }
            }
            else
            {
                var id = Core.FormManager.Save(form);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }

            return Ok(form);
        }

        /// <summary>
        /// 作用：获取某一个表单信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:40:20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Form Model(int id)
        {
            var form = Core.FormManager.Get(id);
            return form;
        }

        /// <summary>
        /// 作用：删除表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:42:25
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            TaskName = "删除表单";
            if (Core.FormManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest($"{TaskName}:删除表单失败，未找到删除表单或者表单已删除");
        }
    }
}
