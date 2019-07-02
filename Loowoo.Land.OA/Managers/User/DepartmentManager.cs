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
            return DB.Departments.Where(e => !e.Deleted).OrderBy(e => e.Sort);
        }

        public bool Delete(int id)
        {
            var model = DB.Departments.FirstOrDefault(x => x.ID == id);
            if (model == null)
            {
                return false;
            }
            if (DB.UserDepartments.Any(e => e.DepartmentId == id && !e.User.Deleted))
            {
                throw new ArgumentException("该部门已有用户使用，无法删除");
            }
            model.Deleted = true;
            DB.SaveChanges();
            return true;
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