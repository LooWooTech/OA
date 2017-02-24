using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    /// <summary>
    /// 种类管理
    /// </summary>
    public class CategoryManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存种类
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:45:28
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int Save(Category category)
        {
            using (var db = GetDbContext())
            {
                db.Categorys.Add(category);
                db.SaveChanges();
                return category.ID;
            }
        }
        /// <summary>
        /// 作用：编辑修改种类
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:47:10
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Edit(Category category)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Categorys.Find(category.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(category);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：获取所有种类列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:48:14
        /// </summary>
        /// <returns></returns>
        public List<Category> GetList()
        {
            using (var db = GetDbContext())
            {
                return db.Categorys.Where(e=>e.Deleted==false).OrderBy(e => e.ID).ToList();
            }
        }
        /// <summary>
        /// 作用：删除种类
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:49:31
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var model = db.Categorys.Find(id);
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
        /// 作用：系统中是否已存在
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:55:25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            using (var db = GetDbContext())
            {
                var model = db.Categorys.FirstOrDefault(e => e.Deleted==false && e.Name.ToLower() == name.ToLower());
                return model != null;
            }
        }
        /// <summary>
        /// 作用：获取种类信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日11:22:01
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Categorys.Find(id);
            }
        }
    }
}