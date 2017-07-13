using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string Username { get; set; }

        public int JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        [NotMapped]
        public int[] DepartmentIds { get; set; }

        [JsonIgnore]
        public virtual List<UserDepartment> UserDepartments { get; set; }

        public UserRole Role { get; set; }

        [NotMapped]
        public int[] GroupIds { get; set; }

        [JsonIgnore]
        public virtual List<UserGroup> UserGroups { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public int Sort { get; set; }

        public bool HasRight(string rightName)
        {
            return UserGroups.Any(e => e.Group.Rights.Any(r => r.Name.Contains(rightName)));
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(RealName))
            {
                throw new ArgumentException("用户名不能为空");
            }
            if (ID == 0)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    throw new ArgumentException("密码不能为空");
                }
                if (string.IsNullOrEmpty(Username))
                {
                    throw new ArgumentException("姓名不能为空");
                }
            }
        }
    }

    public enum UserRole
    {
        Guest,
        User,
        Manager,
        Administrator
    }
}
