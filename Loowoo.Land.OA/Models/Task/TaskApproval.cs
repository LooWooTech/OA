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
    /// 任务审批记录
    /// </summary>
    [Table("task_approval")]
    public class TaskApproval
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int SubTaskId { get; set; }

        public bool? Result { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public string Department { get; set; }

        public int UserId { get; set; }

        public TaskFlowStep Step { get; set; }
    }

    /// <summary>
    /// 审批步骤
    /// </summary>
    public enum TaskFlowStep
    {
        XBKS = 0,
        ZBKS = 1,
        FGLD = 2,
        JLD = 3
    }

}
