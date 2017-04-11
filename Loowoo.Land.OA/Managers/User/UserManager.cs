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
            return DB.Users.FirstOrDefault(e => e.Name == name && e.Password == password);
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
                var user = db.Users.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
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
                query = query.Where(e => e.Username.Contains(parameter.SearchKey) || e.Name.Contains(parameter.SearchKey));
            }
            if (parameter.GroupId > 0)
            {
                query = query.Where(e => e.UserGroups.Any(g => g.UserID == e.ID && g.GroupID == parameter.GroupId));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }
        /// <summary>
        /// 作用：编辑用户信息  通过ID查找用户 未找到用户不进行修改编辑 
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日14:37:46
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Users.Find(user.ID);
                if (entry == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = user.Password.MD5();
                }
                user.Name = user.Name.ToLower();
                user.Username = user.Username.ToLower();

                db.SaveChanges();
            }
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