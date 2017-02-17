using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class FeedManager:ManagerBase
    {
        /// <summary>
        /// 作用：生成一个动态
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:20:07
        /// </summary>
        /// <param name="feed"></param>
        /// <returns></returns>
        public int Save(Feed feed)
        {
            using (var db = GetDbContext())
            {
                db.Feeds.Add(feed);
                db.SaveChanges();
                return feed.ID;
            }
        }

        /// <summary>
        /// 作用：编辑修改动态
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:24:42
        /// </summary>
        /// <param name="feed"></param>
        /// <returns></returns>
        public bool Edit(Feed feed)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Feeds.Find(feed.ID);
                if (entry == null)
                {
                    return false;
                }

                db.Entry(entry).CurrentValues.SetValues(feed);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：删除动态
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:26:04
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Feeds.Find(id);
                if (entry != null)
                {
                    entry.Deleted = true;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 作用：查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:39:27
        /// </summary>
        /// <returns></returns>
        public List<Feed> Search()
        {
            using (var db = GetDbContext())
            {
                var query = db.Feeds.Where(e => e.Deleted == false).AsQueryable();
                return query.ToList();
            }
        }
    }
}