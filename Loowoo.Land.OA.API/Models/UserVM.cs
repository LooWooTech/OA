using Loowoo.Land.OA.API.Security;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class UserVM
    {
        public UserVM(User user)
        {
            ID = user.ID;
            Name = user.Name;
            DepartmentId = user.DepartmentId;
            DeparentmentName = user.Department.Name;
            Role = user.Role;
        }
    
        public int ID { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DeparentmentName { get; set; }

        public int GroupIds { get; set; }

        public int GroupNames { get; set; }

        public UserRole Role { get; set; }

        public string Token { get; set; }
    }
}