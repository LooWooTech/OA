using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 订阅类型管理
    /// </summary>
    public class SubScriptionManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:06:54
        /// </summary>
        /// <param name="scription"></param>
        /// <returns></returns>
        public int Save(Subscription scription)
        {
            using (var db = GetDbContext())
            {
                db.SubScriptions.Add(scription);
                db.SaveChanges();
                return scription.ID;
            }
        }

        /// <summary>
        /// 作用：编辑订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:08:51
        /// </summary>
        /// <param name="scription"></param>
        /// <returns></returns>
        public bool Edit(Subscription scription)
        {
            using (var db = GetDbContext())
            {
                var model = db.SubScriptions.Find(scription.ID);
                if (model == null)
                {
                    return false;
                }
                db.Entry(model).CurrentValues.SetValues(scription);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：获取一个订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:10:21
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Subscription Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.SubScriptions.Find(id);
            }
        }
        /// <summary>
        /// 作用：删除订阅类型
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:11:41
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var model = db.SubScriptions.Find(id);
                if (model == null||model.Deleted==true)
                {
                    return false;
                }
                model.Deleted = true;
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：获取所有有效的订阅类型列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:12:49
        /// </summary>
        /// <returns></returns>
        public List<Subscription> GetList()
        {
            using (var db = GetDbContext())
            {
                return db.SubScriptions.Where(e => e.Deleted == false).OrderBy(e => e.ID).ToList();
            }
        }
        /// <summary>
        /// 作用：验证系统中是否存在
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日17:14:25
        /// </summary>
        /// <param name="name">订阅类型名称</param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            using (var db = GetDbContext())
            {
                var model = db.SubScriptions.FirstOrDefault(e => e.Name.ToLower() == name.ToLower() && e.Deleted == false);
                return model != null;
            }
        }
    }
}