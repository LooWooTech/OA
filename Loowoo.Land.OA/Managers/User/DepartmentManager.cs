using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class DepartmentManager : ManagerBase
    {
        public void Save(Department model)
        {
            if (DB.Departments.Any(e => e.Name == model.Name && e.ID != model.ID))
            {
                throw new Exception("部门名称已使用");
            }

            DB.Departments.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public Department Get(int id)
        {
            if (id == 0) return null;
            return DB.Departments.FirstOrDefault(e => e.ID == id);
        }


        public IEnumerable<Department> GetList()
        {
            return DB.Departments.OrderBy(e => e.Sort);
        }

        public bool Delete(int id)
        {
            var model = DB.Departments.Find(id);
            if (model == null)
            {
                return false;
            }
            if (DB.UserDepartments.Any(e => e.DepartmentId == id))
            {
                throw new ArgumentException("该部门已有用户使用，无法删除");
            }
            if (DB.FlowNodes.Any(e => e.DepartmentIdsValue.Contains(id.ToString())))
            {
                throw new Exception("该部门已被流程使用，无法删除");
            }
            DB.Departments.Remove(model);
            DB.SaveChanges();
            return true;
        }

        public bool Used(int id)
        {
            return DB.FlowNodes.Any(e => e.DepartmentIdsValue.Contains(id.ToString()));
        }

        public void UpdateUserDepartments(int userId, int[] departmentIds)
        {
            var list = DB.UserDepartments.Where(e => e.UserId == userId);
            DB.UserDepartments.RemoveRange(list);
            foreach (var id in departmentIds)
            {
                DB.UserDepartments.Add(new UserDepartment
                {
                    UserId = userId,
                    DepartmentId = id
                });
            }
            DB.SaveChanges();
        }
    }
}