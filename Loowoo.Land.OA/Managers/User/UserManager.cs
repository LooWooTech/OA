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
        public User Login(string username, string password)
        {
            username = username.ToLower();
            password = password.MD5();
            return DB.Users.FirstOrDefault(e => e.Username == username && e.Password == password);
        }

        public User GetModel(int id)
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

        public IEnumerable<User> GetList(UserParameter parameter)
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
            if (parameter.DepartmentIds != null && parameter.DepartmentIds.Length > 0)
            {
                query = query.Where(e => parameter.DepartmentIds.Contains(e.DepartmentId));
            }
            if (parameter.TitleId > 0)
            {
                query = query.Where(e => e.JobTitleId == parameter.TitleId);
            }
            if (parameter.TitleIds != null && parameter.TitleIds.Length > 0)
            {
                query = query.Where(e => parameter.TitleIds.Contains(e.JobTitleId));
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
            if (parameter.GroupIds != null && parameter.GroupIds.Length > 0)
            {
                query = query.Where(e => e.UserGroups.Any(g => parameter.GroupIds.Contains(g.ID)));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(User model)
        {
            model.Username = model.Username.ToLower();
            model.RealName = model.RealName.ToLower();
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = model.Password.MD5();
            }
            if (model.ID == 0)
            {
                if (DB.Users.Any(e => e.Username == model.Username))
                {
                    throw new Exception("用户名已被使用");
                }
                DB.Users.Add(model);
                DB.SaveChanges();
            }
            else
            {
                var entity = DB.Users.FirstOrDefault(e => e.ID == model.ID);
                if (DB.Users.Any(e => e.Username == model.Username && e.ID != model.ID))
                {
                    throw new Exception("用户名已被使用");
                }
                if (!string.IsNullOrEmpty(model.Password))
                {
                    entity.Password = model.Password;
                }
                entity.RealName = model.RealName;
                entity.DepartmentId = model.DepartmentId;
                entity.Role = model.Role;
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