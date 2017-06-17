using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loowoo.Land.OA.Models
{
    [Table("task_progress")]
    public class TaskProgress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Content { get; set; }
    }
}
