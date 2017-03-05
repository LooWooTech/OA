using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 用户动态管理  
    /// 生成动态情况：
    /// 1、某用户发起申请用车
    /// 2、用户A对用户B发布任务
    /// 3、
    /// 
    /// </summary>
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
            db.Feeds.Add(feed);
            db.SaveChanges();
            return feed.ID;
         
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
            var entry = db.Feeds.Find(feed.ID);
            if (entry == null)
            {
                return false;
            }

            db.Entry(entry).CurrentValues.SetValues(feed);
            db.SaveChanges();
            return true;
          
        }
        /// <summary>
        /// 作用：删除动态
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:26:04
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var entry = db.Feeds.Find(id);
            if (entry != null)
            {
                entry.Deleted = true;
                db.SaveChanges();
            }
          
        }

        /// <summary>
        /// 作用：动态查询列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日14:59:32
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Feed> Search(FeedParameter parameter)
        {
            var query = db.Feeds.Where(e => e.Deleted == false).AsQueryable();
            if (parameter.InfoType.HasValue)
            {
                query = query.Where(e => e.InfoId == parameter.InfoType);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.FromUserId == parameter.UserId.Value);
            }
            query = query.OrderByDescending(e => e.CreateTime).SetPage(parameter.Page);
            return query.ToList();
          
        }
    }
}