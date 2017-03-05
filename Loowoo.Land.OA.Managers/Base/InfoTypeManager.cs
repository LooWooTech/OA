using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class InfoTypeManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存消息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:23:35
        /// </summary>
        /// <param name="infoType"></param>
        /// <returns></returns>
        public int Save(InfoType infoType)
        {
            using (var db = GetDbContext())
            {
                db.InfoTypes.Add(infoType);
                db.SaveChanges();
                return infoType.ID;
            }
        }

        /// <summary>
        /// 作用：编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:26:23
        /// </summary>
        /// <param name="infoType"></param>
        /// <returns></returns>
        public bool Edit(InfoType infoType)
        {
            using (var db = GetDbContext())
            {
                var entry = db.InfoTypes.Find(infoType.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(infoType);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：删除  false:未找到相关信息或者已删除
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:30:43
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.InfoTypes.Find(id);
                if (entry == null||entry.Deleted==true)
                {
                    return false;
                }
                entry.Deleted = true;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：通过ID获取信息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:32:29
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InfoType Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.InfoTypes.Find(id);
            }
        }

        /// <summary>
        /// 作用：获取系统中所有有效的信息类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日19:33:25
        /// </summary>
        /// <returns></returns>
        public List<InfoType> Get()
        {
            using (var db = GetDbContext())
            {
                return db.InfoTypes.Where(e => e.Deleted == false).ToList();
            }
        }
    }
}