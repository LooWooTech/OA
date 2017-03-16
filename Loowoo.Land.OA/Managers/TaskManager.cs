using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class TaskManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存任务  返回任务ID
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日15:01:56
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int Save(Task task)
        {
            using (var db = GetDbContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return task.ID;
            }
        }
        /// <summary>
        /// 作用：任务编辑  成功：true 
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日15:49:24
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool Edit(Task task)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Tasks.Find(task.ID);
                if (entry != null)
                {
                    db.Entry(entry).CurrentValues.SetValues(task);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 作用：通过ID获取任务
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:02:23
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Tasks.Find(id);
            }
        }

        /// <summary>
        /// 作用：删除任务
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:05:03
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Tasks.Find(id);
                if (entry != null)
                {
                    entry.Deleted = true;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 作用：任务查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日16:10:29
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Task> Search(TaskParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Tasks.Where(e => e.Deleted == false).AsQueryable();
                query = query.OrderByDescending(e => e.CreateTime).SetPage(parameter.Page);
                return query.ToList();
            }
        }
    }
}