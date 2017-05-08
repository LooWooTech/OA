using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_department")]
    public class UserDepartment
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int DepartmentId { get; set; }

        public virtual User User { get; set; }

        public virtual Department Department { get; set; }
    }
}
