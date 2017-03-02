using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class MessageManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存短消息
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:06:41
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int Save(Message message)
        {
            using (var db = GetDbContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return message.ID;
            }
        }
    }
}