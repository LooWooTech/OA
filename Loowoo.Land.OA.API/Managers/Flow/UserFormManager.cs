using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class UserFormManager:ManagerBase
    {
        /// <summary>
        /// 作用：获取用户相关的表单参与列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日11:19:01
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public List<UserForm> GetList(int userId,int formId)
        {
            using (var db = GetDbContext())
            {
                var models = db.User_Forms.Where(e => e.UserID == userId && e.FormID == formId).ToList();
                return models;
            }
        }
        /// <summary>
        /// 作用：保存
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:29:38
        /// </summary>
        /// <param name="userForm"></param>
        /// <returns></returns>
        public int Save(UserForm userForm)
        {
            using (var db = GetDbContext())
            {
                db.User_Forms.Add(userForm);
                db.SaveChanges();
                return userForm.ID;
            }
        }
        /// <summary>
        /// 作用：编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:44:25
        /// </summary>
        /// <param name="userForm"></param>
        /// <returns></returns>
        public bool Edit(UserForm userForm)
        {
            using (var db = GetDbContext())
            {
                var model = db.User_Forms.Find(userForm.ID);
                if (model == null)
                {
                    model = db.User_Forms.FirstOrDefault(e => e.UserID == userForm.UserID && e.FormID == userForm.FormID && e.InfoID == userForm.InfoID);
                    if (model == null)
                    {
                        return false;
                    }
                }
                db.Entry(model).CurrentValues.SetValues(userForm);
                db.SaveChanges();
                return true;
            }
        }
    }
}