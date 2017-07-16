using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("task_todo")]
    public class TaskTodo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int TaskId { get; set; }

        public string Content { get; set; }

        public string Note { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public int ToUserId { get; set; }

        public virtual User ToUser { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? ScheduleTime { get; set; }

        public bool Completed { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
