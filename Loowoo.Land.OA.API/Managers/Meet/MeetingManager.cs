using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class MeetingManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存会议申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日14:51:16
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public int Save(Meeting meeting)
        {
            using (var db = GetDbContext())
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return meeting.ID;
            }
        }

        /// <summary>
        /// 作用：验证会议申请是否与其他会议冲突  主要验证时间 会议室
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日14:58:12
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public bool Validate(Meeting meeting)
        {
            using(var db = GetDbContext())
            {
                var query = db.Meetings.Where(e =>
                (e.StartTime < meeting.StartTime && e.EndTime < meeting.StartTime)
                || (e.StartTime < meeting.EndTime && e.EndTime > meeting.EndTime)).Where(e => e.Room == meeting.Room).ToList();
                return query.Count == 0;
            }
        }
    }
}