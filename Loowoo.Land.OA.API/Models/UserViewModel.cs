using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loowoo.Land.OA.Models;

namespace Loowoo.Land.OA.API.Models
{
    public class UserViewModel
    {
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