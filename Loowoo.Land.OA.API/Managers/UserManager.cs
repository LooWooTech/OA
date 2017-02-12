using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class UserManager:ManagerBase
    {
        /// <summary>
        /// 作用：用户登陆  输入参数密码为明文，无需加密
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:33:25
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string name,string password)
        {
            password = password.MD5();
            using (var db = GetDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Name.ToLower() == name.ToLower() && e.Password == password);
                return user;
            }
        }
        /// <summary>
        /// 作用：通过用户ID获取用户信息
        /// 作者：汪建龙
        /// 编写时间：2017-02-11 12:34:45
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public User Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Users.Find(id);
            }
        }
        /// <summary>
        /// 作用：通过登陆名系统中是否存在用户
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日12:50:05
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            using (var db = GetDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
                return user != null;
            }
        }
        /// <summary>
        /// 作用：添加新用户
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日13:00:18
        /// </summary>
        /// <param name="user"></param>
        public void Register(User user)
        {
            using (var db = GetDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 作用：查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日13:11:29
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<User> Search(UserParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Users.AsQueryable();
                query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
                return query.ToList();
            }
        }
        /// <summary>
        /// 作用：编辑用户信息  通过ID查找用户 未找到用户不进行修改编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日14:37:46
        /// </summary>
        /// <param name="user"></param>
        public void  Edit(User user)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Users.Find(user.ID);
                if (entry == null)
                {
                    return ;
                }
                db.Entry(entry).CurrentValues.SetValues(user);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 作用：删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Users.Find(id);
                if (entry == null)
                {
                    return false;
                }
                db.Users.Remove(entry);
                db.SaveChanges();
                return true;
            }
        }
    }
}