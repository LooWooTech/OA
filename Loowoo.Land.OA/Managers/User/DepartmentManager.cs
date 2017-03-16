using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class DepartmentManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存部门
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日09:14:55
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public int Save(Department department)
        {
            using (var db = GetDbContext())
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return department.ID;
            }
        }

        /// <summary>
        /// 作用：通过ID获取部门信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日09:15:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department Get(int id)
        {
            if (id == 0) return null;
            return DB.Departments.FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// 作用：编辑部门信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日09:17:18
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool Edit(Department department)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Departments.Find(department.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(department);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：获取所有部门信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:48:08
        /// </summary>
        /// <returns></returns>
        public List<Department> GetList()
        {
            using (var db = GetDbContext())
            {
                var list = db.Departments.Where(e=>e.ParentID==0).ToList();
                foreach(var item in list)
                {
                    item.Children = db.Departments.Where(e => e.ParentID == item.ID).ToList();
                }
                return list;

            }
        }

        /// <summary>
        /// 作用：删除部门信息 
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:22:15
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var model = db.Departments.Find(id);
                if (model == null)
                {
                    return false;
                }
                db.Departments.Remove(model);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：验证系统中是否存在
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日11:12:35
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            using (var db = GetDbContext())
            {
                return db.Departments.Any(e => e.Name.ToLower() == name.ToLower());
            }
        }
        /// <summary>
        /// 作用：验证部门ID是否使用
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日09:35:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Used(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Users.Any(e => e.DepartmentId == id) || db.Flow_Node_Datas.Any(e => e.DepartmentId == id) || db.FlowNodes.Any(e => e.DepartmentId == id);
            }
        }
    }
}