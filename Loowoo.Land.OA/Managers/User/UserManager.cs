using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class UserManager : ManagerBase
    {
        public User Login(string name, string password)
        {
            name = name.ToLower();
            password = password.MD5();
            return DB.Users.FirstOrDefault(e => e.Username == name && e.Password == password);
        }

        public User Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return DB.Users.Find(id);
        }

        public bool Exist(string name)
        {
            using (var db = GetDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Username.ToLower() == name.ToLower());
                return user != null;
            }
        }

        public void Register(User user)
        {
            user.Password = user.Password.MD5();
            using (var db = GetDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public IEnumerable<User> Search(UserParameter parameter)
        {
            var query = DB.Users.AsQueryable();
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.ID == parameter.UserId);
            }
            if (parameter.UserIds != null && parameter.UserIds.Length > 0)
            {
                query = query.Where(e => parameter.UserIds.Contains(e.ID));
            }
            if (parameter.DepartmentId > 0)
            {
                query = query.Where(e => e.DepartmentId == parameter.DepartmentId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                parameter.SearchKey = parameter.SearchKey.ToLower();
                query = query.Where(e => e.RealName.Contains(parameter.SearchKey) || e.Username.Contains(parameter.SearchKey));
            }
            if (parameter.GroupId > 0)
            {
                query = query.Where(e => e.UserGroups.Any(g => g.UserID == e.ID && g.GroupID == parameter.GroupId));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(User user)
        {
            user.Username = user.Username.ToLower();
            user.RealName = user.RealName.ToLower();
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = user.Password.MD5();
            }
            if (user.ID == 0)
            {
                if (DB.Users.Any(e => e.Username == user.Username))
                {
                    throw new Exception("用户名已被使用");
                }
                DB.Users.Add(user);
                DB.SaveChanges();
            }
            else
            {
                var entity = DB.Users.Find(user.ID);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    entity.Password = user.Password;
                }
                entity.RealName = user.RealName;
                entity.DepartmentId = user.DepartmentId;
                entity.Role = user.Role;
            }
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = DB.Users.Find(id);
            if (user != null)
            {
                var userGroups = DB.UserGroups.Where(e => e.UserID == user.ID);
                DB.UserGroups.RemoveRange(userGroups);
                DB.Users.Remove(user);

                DB.SaveChanges();
            }
        }
    }
}