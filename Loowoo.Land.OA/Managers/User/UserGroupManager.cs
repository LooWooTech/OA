using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class UserGroupManager : ManagerBase
    {

        public void UpdateUserGroups(int userId, int[] groupIds)
        {
            var list = DB.UserGroups.Where(e => e.UserID == userId);
            DB.UserGroups.RemoveRange(list);
            foreach (var id in groupIds)
            {
                DB.UserGroups.Add(new UserGroup
                {
                    UserID = userId,
                    GroupID = id
                });
            }
            DB.SaveChanges();
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
                var entry = db.UserGroups.Find(user_group.ID);
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
                var entry = db.UserGroups.Find(id);
                if (entry != null)
                {
                    db.UserGroups.Remove(entry);
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
                return db.UserGroups.Where(e => e.UserID == userId).ToList();
            }
        }

        public UserGroup Get(int userId, int groupId)
        {
            using (var db = GetDbContext())
            {
                return db.UserGroups.FirstOrDefault(e => e.UserID == userId && e.GroupID == groupId);
            }
        }

        /// <summary>
        /// 作用：验证用户和组存在关联
        /// 作者：汪建龙
        /// 编写时间：2017年3月5日11:42:24
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool Exist(int userId, int groupId)
        {
            return DB.UserGroups.Any(e => e.UserID == userId && e.GroupID == groupId);
        }

    }
}