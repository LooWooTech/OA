using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("task")]
    public class Task
    {
        public Task()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? ScheduledTime { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? CompletedTime { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreatorID { get; set; }

        [NotMapped]
        public User Creator { get; set; }

        public int ParentID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }
    }
}