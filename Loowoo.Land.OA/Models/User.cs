using Loowoo.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }

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
