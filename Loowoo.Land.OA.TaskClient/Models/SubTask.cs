using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.TaskClient.Models
{
    /// <summary>
    /// 子任务
    /// </summary>
    public class SubTask
    {
        public int ID { get; set; }

        public int ParentId { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

        public int TaskId { get; set; }

        public string Content { get; set; }

        public int ToUserId { get; set; }

        public string ToUserName { get; set; }

        public int LeaderId { get; set; }

        public string LeaderName { get; set; }

        public int ToDepartmentId { get; set; }

        public string ToDepartmentName { get; set; }

        public bool IsMaster { get { return ParentId == 0; } }

        public DateTime? ScheduleDate { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

        public SubTaskStatus Status { get; set; }

        public List<SubTask> Children { get; set; } = new List<SubTask>();
    }

    public enum SubTaskStatus
    {
        Doing,
        Checking,
        Complete,
        Back
    }
}
