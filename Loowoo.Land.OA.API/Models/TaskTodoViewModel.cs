using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class TaskTodoViewModel
    {
        public int ID { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public string ToUserName { get; set; }
        public int ToUserId { get; set; }
        public int SubTaskId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Completed { get; set; }
        public string Content { get; set; }

        public TaskTodoViewModel(Land.OA.Models.TaskTodo e)
        {
            ID = e.ID;
            CreatorId = e.CreatorId;
            CreateTime = e.CreateTime;
            ScheduleDate = e.ScheduleDate;
            ToUserId = e.ToUserId;
            ToUserName = e.ToUser == null ? "" : e.ToUser.RealName;
            SubTaskId = e.SubTaskId;
            UpdateTime = e.UpdateTime;
            Completed = e.Completed;
            Content = e.Content;
        }
    }
}