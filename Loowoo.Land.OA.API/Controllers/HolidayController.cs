using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class HolidayController : ControllerBase
    {
        /// <summary>
        /// 作用：保存
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日14:29:48
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] Holiday holiday)
        {
            TaskName = "保存节假日";
            if (holiday == null)
            {
                return BadRequest($"{TaskName}:未获取节假日信息");
            }
            if (!string.IsNullOrEmpty(holiday.Name))
            {
                return BadRequest($"{TaskName}:节假日名称不能为空");
            }
            if (holiday.StartTime==DateTime.MinValue||holiday.EndTime==DateTime.MinValue|| holiday.EndTime < holiday.StartTime)
            {
                return BadRequest($"{TaskName}:节假日起始时间和结束时间逻辑不正确");
            }
            if (Core.HolidayManager.Exist(holiday.StartTime, holiday.EndTime))
            {
                return BadRequest($"{TaskName}:当前节假日时间安排与系统中现有节假日时间冲突");
            }
            if (holiday.ID > 0)
            {
                if (!Core.HolidayManager.Edit(holiday))
                {
                    return BadRequest($"{TaskName}:未找到需要编辑的节假日信息");
                }
            }
            else
            {
                if (Core.HolidayManager.Exist(holiday.Name, holiday.StartTime.Year))
                {
                    return BadRequest($"{TaskName}:{holiday.StartTime.Year}年已存在节假日名称为{holiday.Name}");
                }
                var id = Core.HolidayManager.Save(holiday);
                if (id <= 0)
                {
                    return BadRequest($"{TaskName}:失败");
                }
            }
            return Ok(holiday);
        }

        /// <summary>
        /// 作用：删除节假日信息
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日14:32:08
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            TaskName = "删除节假日";
            if (!Core.HolidayManager.Delete(id))
            {
                return BadRequest($"{TaskName}:未找到ID为{id}的节假日信息");
            }
            return Ok();
        }

        /// <summary>
        /// 作用：获取节假日列表  按照时间
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日14:38:37
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Holiday> List()
        {
            return Core.HolidayManager.GetList();
        }

        /// <summary>
        /// 作用：生成周末
        /// 作者：汪建龙
        /// 编写时间：2017年3月27日14:52:15
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GenerateWeek(int year)
        {
            Core.HolidayManager.GenerateWeek(year);
            return Ok();
        }
    }
}
