using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 表单类型
        /// </summary>
        public int FormID { get; set; }

        public int Sort { get; set; }

        public bool Deleted { get; set; }

        public int ParentID { get; set; }

        public int Type { get; set; }
    }
}
