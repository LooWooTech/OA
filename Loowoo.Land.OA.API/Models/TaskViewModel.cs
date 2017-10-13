using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class TaskViewModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public string From { get; set; }
        public TaskFromType FromType { get; set; }
        public string Goal { get; set; }
        public int FormId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string FlowStep { get; set; }
        public bool Reminded { get; set; }
        public int FlowDataId { get; set; }
        public bool Completed { get; set; }

        public TaskViewModel(Loowoo.Land.OA.Models.Task model)
        {
            ID = model.ID;
            Number = model.Number;
            Name = model.Name;
            ScheduleDate = model.ScheduleDate;
            From = model.From;
            FromType = model.FromType;
            Goal = model.Goal;
            FormId = model.Info.FormId;
            CreateTime = model.Info.CreateTime;
            UpdateTime = model.Info.UpdateTime;
            FlowStep = model.Info.FlowStep;
            FlowDataId = model.Info.FlowDataId;
            Reminded = model.Info.Reminded;
            Completed = model.Info.FlowData == null ? false : model.Info.FlowData.Completed;
        }
    }
}