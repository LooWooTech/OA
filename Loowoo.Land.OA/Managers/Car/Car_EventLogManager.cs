using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class Car_EventLogManager:ManagerBase
    {
        /// <summary>
        /// 作用：生成用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日11:29:29
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int Save(CarEventLog log)
        {
            using (var db = GetDbContext())
            {
                db.Car_Eventlogs.Add(log);
                db.SaveChanges();
                return log.ID;
            }
        }
        /// <summary>
        /// 作用：编辑用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日13:46:46
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool Edit(CarEventLog log)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Car_Eventlogs.Find(log.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(log);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：撤销用车申请
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日13:54:16
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Car_Eventlogs.Find(id);
                if (entry != null)
                {
                    entry.Deleted = true;
                    db.SaveChanges();
                }
            }
        }
    }
}