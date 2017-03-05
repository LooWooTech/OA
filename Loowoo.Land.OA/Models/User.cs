using Loowoo.Security;
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
        public string Username { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public UserRole Role { get; set; }

        [NotMapped]
        public List<Group> Groups { get; set; }
        [NotMapped]
        public string Ticket { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new ArgumentException("用户名不能为空");
            }
            if (ID == 0)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    throw new ArgumentException("密码不能为空");
                }
                if (string.IsNullOrEmpty(Name))
                {
                    throw new ArgumentException("姓名不能为空");
                }
            }
        }
    }
}
