using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("freeflow")]
    public class FreeFlow
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DepartmentLimitMode LimitMode { get; set; }

        [Column("DepartmentIds")]
        public string DepartmentIdsValue { get; set; }
        /// <summary>
        /// 跨部门
        /// </summary>
        public bool CrossDepartment { get; set; }
        /// <summary>
        /// 跨级别
        /// </summary>
        public bool CrossLevel { get; set; }

        [NotMapped]
        public int[] DepartmentIds
        {
            get
            {
                if (string.IsNullOrEmpty(DepartmentIdsValue)) return null;
                return DepartmentIdsValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => int.Parse(id)).ToArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    DepartmentIdsValue = null;
                else
                    DepartmentIdsValue = string.Join(",", value);
            }
        }
    }

    public enum DepartmentLimitMode
    {
        [Description("指定部门")]
        Assign = 0,
        [Description("拟稿人部门")]
        Poster = 1,
        [Description("自己部门")]
        Sender = 2,
    }
}
