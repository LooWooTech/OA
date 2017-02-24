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
    /// 种类管理
    /// </summary>
    public class CategoryController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存更新
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Category category)
        {
            TaskName = "保存种类";
            if (category == null || string.IsNullOrEmpty(category.Name))
            {
                return BadRequest($"{TaskName}:未获取种类信息、种类名称不能为空");
            }
            if (Core.CategoryManager.Exist(category.Name))
            {
                return BadRequest($"{TaskName}:系统中已存在相同名称");
            }
            if (category.ID > 0)
            {
                if (!Core.CategoryManager.Edit(category))
                {
                    return BadRequest($"{TaskName}:未找到需要编辑信息");
                }
            }
            else
            {
                var id = Core.CategoryManager.Save(category);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(category);
        }
        /// <summary>
        /// 作用：获取某一个种类信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日11:22:52
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.CategoryManager.Get(id);
            if (model == null || model.Deleted == true)
            {
                return NotFound();
            }
            return Ok(model);
        }
        /// <summary>
        /// 作用：获取种类列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日11:24:20
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Category> List()
        {
            var list = Core.CategoryManager.GetList();
            return list;
        }
        /// <summary>
        /// 作用：删除种类信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日11:25:53
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.CategoryManager.Delete(id))
            {
                return Ok();
            }
            return BadRequest("该种类已删除或者系统中未找到相关信息");
        }
    }
}
