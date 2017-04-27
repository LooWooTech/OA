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
            DB.Departments.Add(department);
            DB.SaveChanges();
            return department.ID;
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
            var entry = DB.Departments.Find(department.ID);
            if (entry == null)
            {
                return false;
            }
            DB.Entry(entry).CurrentValues.SetValues(department);
            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：获取所有部门信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日10:48:08
        /// </summary>
        /// <returns></returns>
        public List<Department> GetList()
        {
            var list = DB.Departments.Where(e => e.ParentID == 0).ToList();
            foreach (var item in list)
            {
                item.Children = DB.Departments.Where(e => e.ParentID == item.ID).ToList();
            }
            return list;
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
            var model = DB.Departments.Find(id);
            if (model == null)
            {
                return false;
            }
            DB.Departments.Remove(model);
            DB.SaveChanges();
            return true;
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
            return  DB.Departments.Any(e => e.Name.ToLower() == name.ToLower());
        }

        public bool Used(int id)
        {
            return DB.FlowNodes.Any(e => e.DepartmentIdsValue.Contains(id.ToString()));
        }
    }
}