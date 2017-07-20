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

        public int TaskId { get; set; }

        public string Content { get; set; }

        public int ToUserId { get; set; }

        public int ToDepartmentId { get; set; }
        /// <summary>
        /// 为否为主办科室
        /// </summary>
        public bool IsMaster { get; set; }

        public DateTime ScheduleDate { get; set; }

        public DateTime CreateTime { get; set; }

        public int GroupId { get; set; }

        public virtual SubTaskGroup Group { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompleteDate { get; set; }

    }
}
