using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class GroupManager:ManagerBase
    {
        /// <summary>
        /// 作用：创建组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:29:55
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int Save(Group group)
        {
            using (var db = GetDbContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return group.ID;
            }
        }
        /// <summary>
        /// 作用：组编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:37:39
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool Edit(Group group)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Groups.Find(group.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(group);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：获取用户组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:38:54
        /// </summary>
        /// <returns></returns>
        public List<Group> GetList()
        {
            using (var db = GetDbContext())
            {
                return db.Groups.ToList();
            }
        }

        /// <summary>
        /// 作用：获取组信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日17:10:48
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Group Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Groups.Find(id);
            }
        }
    }
}