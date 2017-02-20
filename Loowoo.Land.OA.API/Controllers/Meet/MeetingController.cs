using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers.Meet
{
    public class MeetingController : LoginControllerBase
    {
        [HttpPost]
        public IHttpActionResult Save([FromBody] Meeting meeting)
        {
            TaskName = "申请会议";
            if (meeting == null || string.IsNullOrEmpty(meeting.Title))
            {
                return BadRequest($"{TaskName}:未获取会议申请信息、会议名称不能为空");
            }
            var room = Core.Meeting_RoomManager.Get(meeting.Room);
            if (room == null)
            {
                return NotFound();
            }

            if (!Core.MeetingManager.Validate(meeting))
            {
                return BadRequest($"{TaskName}:与系统中会议时间会议室冲突");
            }
            var id = 0;
            try
            {
                id = Core.MeetingManager.Save(meeting);
            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
                return BadRequest($"{TaskName}:保存会议申请发生错误");
            }
            if (id <= 0)
            {
                return BadRequest($"{TaskName}:保存会议申请失败");
            }

        }
    }
}
