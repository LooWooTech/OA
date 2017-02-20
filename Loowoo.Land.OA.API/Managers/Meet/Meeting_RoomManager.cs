using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    /// <summary>
    /// 会议室管理
    /// </summary>
    public class Meeting_RoomManager:ManagerBase
    {
        /// <summary>
        /// 作用：创建会议室
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日11:08:08
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int Save(MeetingRoom room)
        {
            using (var db = GetDbContext())
            {
                db.Meeting_Rooms.Add(room);
                db.SaveChanges();
                return room.ID;
            }
        }
        /// <summary>
        /// 作用：编辑会议室
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日11:09:43
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool Edit(MeetingRoom room)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Meeting_Rooms.Find(room.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(room);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：删除会议室
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日11:11:26
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Meeting_Rooms.Find(id);
                if (entry == null)
                {
                    return false;
                }
                entry.Deleted = true;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：获取所有会议室
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日11:27:57
        /// </summary>
        /// <returns></returns>
        public List<MeetingRoom> Get()
        {
            using (var db = GetDbContext())
            {
                return db.Meeting_Rooms.Where(e => e.Deleted == false).ToList();
            }
        }
    }
}