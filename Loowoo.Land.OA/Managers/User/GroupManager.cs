using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class GroupManager : ManagerBase
    {
        public int Save(Group group)
        {
            if (group.ID > 0)
            {
                var entity = DB.Groups.FirstOrDefault(e => e.ID == group.ID);
                entity.Name = group.Name;
                if (group.Rights != null)
                {
                    var olds = DB.GroupRights.Where(e => e.GroupId == entity.ID);
                    DB.GroupRights.RemoveRange(olds);
                    entity.Rights = group.Rights.Select(e => new GroupRight { GroupId = group.ID, Name = e.Name }).ToList();
                }
            }
            else
            {
                DB.Groups.Add(group);
            }
            DB.SaveChanges();
            return group.ID;
        }

        public IEnumerable<Group> GetList()
        {
            return DB.Groups.AsQueryable();
        }

        public Group Get(int id)
        {
            return DB.Groups.Find(id);
        }

        public bool Delete(int id)
        {
            var model = DB.Groups.Find(id);
            if (model == null)
            {
                return false;
            }
            var userGroups = DB.UserGroups.Where(e => e.GroupId == id);
            DB.UserGroups.RemoveRange(userGroups);

            var groupRights = DB.GroupRights.Where(e => e.GroupId == id);
            DB.GroupRights.RemoveRange(groupRights);

            DB.Groups.Remove(model);
            DB.SaveChanges();
            return true;
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