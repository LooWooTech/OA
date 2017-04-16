using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table("organization")]
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
        [NotMapped]
        public List<Department> Children { get; set; }
    }
}
