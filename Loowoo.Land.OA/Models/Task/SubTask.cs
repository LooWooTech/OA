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
    /// 子任务
    /// </summary>
    [Table("sub_task")]
    public class SubTask
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ParentId { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public int TaskId { get; set; }

        public string Content { get; set; }

        public int ToUserId { get; set; }

        public virtual User ToUser { get; set; }

        public int ToDepartmentId { get; set; }

        public string ToDepartmentName { get; set; }

        /// <summary>
        /// 为否为主办科室
        /// </summary>
        [NotMapped]
        public bool IsMaster { get { return ParentId == 0; } }

        public DateTime ScheduleDate { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public bool Completed { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual List<TaskTodo> Todos { get; set; }

    }
}
