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
    }
}