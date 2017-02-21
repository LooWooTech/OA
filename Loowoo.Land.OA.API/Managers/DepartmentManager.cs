using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
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
            using (var db = GetDbContext())
            {
                return db.Departments.Find(id);
            }
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
    }
}