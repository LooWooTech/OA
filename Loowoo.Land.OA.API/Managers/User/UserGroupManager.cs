using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class UserGroupManager:ManagerBase
    {
        /// <summary>
        /// 作用：
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日18:56:17
        /// </summary>
        /// <param name="user_group"></param>
        /// <returns></returns>
        public int Save(UserGroup user_group)
        {
            using (var db = GetDbContext())
            {
                db.User_Groups.Add(user_group);
                db.SaveChanges();
                return user_group.ID;
            }
        }

        /// <summary>
        /// 作用：编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日09:16:04
        /// </summary>
        /// <param name="user_group"></param>
        /// <returns></returns>
        public bool Edit(UserGroup user_group)
        {
            using (var db = GetDbContext())
            {
                var entry = db.User_Groups.Find(user_group.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(user_group);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：删除用户部门关系
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日09:42:31
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.User_Groups.Find(id);
                if (entry != null)
                {
                    db.User_Groups.Remove(entry);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：获取用户所有的组
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日15:04:52
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserGroup> GetByUserId(int userId)
        {
            using (var db = GetDbContext())
            {
                return db.User_Groups.Where(e => e.UserID == userId).ToList();
            }
        }


    }
}