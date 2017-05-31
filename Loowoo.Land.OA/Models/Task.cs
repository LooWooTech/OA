using Loowoo.Common;
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


        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        [Column("content")]
        public string ContentValue { get; set; }

        [NotMapped]
        public TaskContent Content
        {
            get
            {
                return ContentValue.ToObject<TaskContent>();
            }
            set
            {
                ContentValue = value.ToJson();
            }
        }

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
        public int CreatorId { get; set; }


        /// <summary>
        /// 负责人ID
        /// </summary>
        public int ReceiverId { get; set; }

        [NotMapped]
        public User Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }
    }

    public class TaskContent
    {
        /// <summary>
        /// 任务来源
        /// </summary>
        public string LY { get; set; }

        /// <summary>
        /// 主办单位
        /// </summary>
        public string ZB_DW { get; set; }

        /// <summary>
        /// 协办单位
        /// </summary>
        public string XB_DW { get; set; }

        /// <summary>
        /// 工作目标
        /// </summary>
        public string GZ_MB { get; set; }
        /// <summary>
        /// 工作进展
        /// </summary>
        public List<GZJZ> GZ_JZ { get; set; }
        /// <summary>
        /// 领导批示
        /// </summary>
        public LDPS LD_PS { get; set; }

        public class LDPS
        {
            public LDPS()
            {
                CreateTime = DateTime.Now;
            }
            public string Content { get; set; }

            public DateTime CreateTime { get; set; }

            public int UserId { get; set; }
        }

        public class GZJZ
        {
            public GZJZ()
            {
                CreateTime = DateTime.Now;
            }

            public string Content { get; set; }

            public DateTime CreateTime { get; set; }

            public int UserId { get; set; }

            public string Signature { get; set; }
        }
    }
}