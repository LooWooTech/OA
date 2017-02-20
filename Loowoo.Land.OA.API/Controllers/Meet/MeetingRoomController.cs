using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers.Meet
{
    public class MeetingRoomController : LoginControllerBase
    {
        /// <summary>
        /// 作用：保存会议室信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日11:37:38
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Save([FromBody] MeetingRoom room)
        {
            TaskName = "保存会议室";
            if (room == null || string.IsNullOrEmpty(room.Name))
            {
                return BadRequest($"{TaskName}:未获取会议室信息、会议室名称不能为空");
            }
            try
            {
                Core.Meeting_RoomManager.Save(room);

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
                return BadRequest($"{TaskName}:发生错误");
            }
            return Ok();
        }
        /// <summary>
        /// 作用：编辑会议室信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日13:46:10
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Edit([FromBody] MeetingRoom room)
        {
            TaskName = "会议室编辑";
            if (room == null || room.ID == 0 || string.IsNullOrEmpty(room.Name))
            {
                return BadRequest($"{TaskName}:未获取需要编辑的会议室信息、ID或者会议室名称不能为空");
            }
            try
            {

            }catch(Exception ex)
            {
                LogWriter.WriteException(ex, TaskName);
            }

            return Ok();
        }
    }
}
