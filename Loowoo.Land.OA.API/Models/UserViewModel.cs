using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loowoo.Land.OA.Models;

namespace Loowoo.Land.OA.API.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(User user)
        {
            ID = user.ID;
            RealName = user.RealName;
            JobTitle = user.JobTitle == null ? null : user.JobTitle.Name;
            JobTitleId = user.JobTitleId;
            Username = user.Username;
            Role = user.Role;
            Departments = user.UserDepartments.Select(d => new
            {
                Name = d.Department == null ? null : d.Department.Name,
                ID = d.Department == null ? 0 : d.Department.ID,
                ParentId = d.Department == null ? 0 : d.Department.ParentId,
            });
            Groups = user.UserGroups.Select(g => new
            {
                Name = g.Group == null ? null : g.Group.Name,
                ID = g.Group == null ? 0 : g.Group.ID
            });
        }

        public int ID { get; set; }
        public string Username { get; set; }
        public string RealName { get; set; }

        public string JobTitle { get; set; }
        public int JobTitleId { get; set; }

        public UserRole Role { get; set; }

        public IEnumerable<dynamic> Departments { get; set; }
        public IEnumerable<dynamic> Groups { get; set; }
    }
}