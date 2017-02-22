using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    /// <summary>
    /// 表单管理
    /// </summary>
    public class FormManager:ManagerBase
    {
        /// <summary>
        /// 作用：新建表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:23:22
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public int Save(Form form)
        {
            using (var db = GetDbContext())
            {
                db.Forms.Add(form);
                db.SaveChanges();
                return form.ID;
            }
        }
        /// <summary>
        /// 作用：编辑表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:24:31
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool Edit(Form form)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Forms.Find(form.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(form);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：删除表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:25:41
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Forms.Find(id);
                if (entry == null)
                {
                    return false;
                }
                db.Forms.Remove(entry);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：通过ID获取表单
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:26:43
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Form Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Forms.Find(id);
            }
        }
        /// <summary>
        /// 作用：获取表单列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日15:27:52
        /// </summary>
        /// <returns></returns>
        public List<Form> GetList()
        {
            using (var db = GetDbContext())
            {
                return db.Forms.ToList();
            }

        }
    }
}