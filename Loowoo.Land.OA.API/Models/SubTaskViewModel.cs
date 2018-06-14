using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class SubTaskViewModel
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public SubTaskStatus Status { get; set; }
        public string Content { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int ToDepartmentId { get; set; }
        public string ToDepartmentName { get; set; }
        public int ToUserId { get; set; }
        public string ToUserName { get; set; }
        public int TaskId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int ParentId { get; set; }
        public bool IsMaster { get; set; }
        public int LeaderId { get; set; }
        public string LeaderName { get; set; }
        public IEnumerable<TaskTodoViewModel> Todos { get; set; }

        public SubTaskViewModel(SubTask e)
        {
            ID = e.ID;
            CreateTime = e.CreateTime;
            CreatorId = e.CreatorId;
            CreatorName = e.Creator == null ? "" : e.Creator.RealName;
            Status = e.Status;
            Content = e.Content;
            UpdateTime = e.UpdateTime;
            ToDepartmentId = e.ToDepartmentId;
            ToDepartmentName = e.ToDepartmentName;
            ToUserId = e.ToUserId;
            ToUserName = e.ToUser == null ? "" : e.ToUser.RealName;
            TaskId = e.TaskId;
            ScheduleDate = e.ScheduleDate;
            ParentId = e.ParentId;
            IsMaster = e.IsMaster;
            LeaderId = e.LeaderId;
            LeaderName = e.Leader == null ? "" : e.Leader.RealName;
            Todos = e.Todos.Select(t => new TaskTodoViewModel(t));
        }
    }
}