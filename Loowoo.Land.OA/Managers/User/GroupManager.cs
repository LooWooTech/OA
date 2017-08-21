using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
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
            DB.Groups.Add(group);
            DB.SaveChanges();
            return group.ID;
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
            var entry = DB.Groups.Find(group.ID);
            if (entry == null)
            {
                return false;
            }
            DB.Entry(entry).CurrentValues.SetValues(group);
            DB.SaveChanges();
            return true;
        }
        /// <summary>
        /// 作用：获取用户组
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:38:54
        /// </summary>
        /// <returns></returns>
        public List<Group> GetList()
        {
            return DB.Groups.ToList();
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
            if (id <= 0)
            {
                return null;
            }
            return DB.Groups.Find(id);
        }

        /// <summary>
        /// 作用：验证组是否已存在
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:35:01
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Exist(string name,GroupType type)
        {
            return DB.Groups.Any(e => e.Name.ToLower() == name.ToLower() && e.Type == type);
        }
        /// <summary>
        /// 作用：删除组
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日14:45:01
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var model = DB.Groups.Find(id);
            if (model == null)
            {
                return false;
            }
            DB.Groups.Remove(model);
            DB.SaveChanges();
            return true;
        }
        /// <summary>
        /// 作用：通过用户ID获取用户所在的组列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日15:19:06
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Group> GetByUserId(int userId)
        {
            var relation = DB.UserGroups.Where(e => e.UserId == userId).ToList();
            foreach (var item in relation)
            {
                item.Group = DB.Groups.Find(item.GroupId);
            }
            return relation.Select(e => e.Group).ToList();
        }

        /// <summary>
        /// 作用：验证Group是否使用
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日09:40:14
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Used(int id)
        {
            return DB.UserGroups.Any(e => e.GroupId == id);
        }

        public void UpdateUserGroups(int userId, int[] groupIds)
        {
            var list = DB.UserGroups.Where(e => e.UserId == userId);
            DB.UserGroups.RemoveRange(list);
            foreach (var id in groupIds)
            {
                DB.UserGroups.Add(new UserGroup
                {
                    UserId = userId,
                    GroupId = id
                });
            }
            DB.SaveChanges();
        }
    }
}