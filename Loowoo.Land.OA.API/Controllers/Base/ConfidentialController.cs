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
    /// 密级管理
    /// </summary>
    public class ConfidentialController : LoginControllerBase
    {
        /// <summary>
        /// 作用：密级保存或更新
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日13:52:54
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] ConfidentialLevel level)
        {
            TaskName = "保存密级";
            if (level == null || string.IsNullOrEmpty(level.Name))
            {
                return BadRequest($"{TaskName}:未获取密级信息，密级名称不能为空");
            }
            if (Core.ConfidentialLevelManager.Exist(level.Name))
            {
                return BadRequest($"{TaskName}:当前系统中已存在相同密级名称");
            }
            if (level.ID > 0)
            {
                if (!Core.ConfidentialLevelManager.Edit(level))
                {
                    return BadRequest($"{TaskName}:编辑失败,未找到需要编辑的信息");
                }
            }
            else
            {
                var id = Core.ConfidentialLevelManager.Save(level);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:保存失败");
                }
            }
            return Ok(level);
        }

        /// <summary>
        /// 作用：获取某一个密级信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日13:54:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Model(int id)
        {
            var model = Core.ConfidentialLevelManager.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// 作用：删除密级信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日13:56:28
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (Core.ConfidentialLevelManager.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
        /// <summary>
        /// 作用：获取密级信息列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日13:57:54
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ConfidentialLevel> List()
        {
            var list = Core.ConfidentialLevelManager.GetList();
            return list;
        }
    }
}
