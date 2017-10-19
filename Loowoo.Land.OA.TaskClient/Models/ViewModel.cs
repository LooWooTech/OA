using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.TaskClient.Models
{
    public class TaskViewModel
    {
        public string TaskName { get; set; }

        public List<MasterTaskViewModel> Children { get; set; } = new List<MasterTaskViewModel>();
    }

    public class MasterTaskViewModel
    {
        public int ID { get; internal set; }

        public DateTime? ScheduleDate { get; set; }

        public string TaskName { get; set; }

        public string Department { get; set; }

        public List<SubTaskViewModel> Children { get; set; } = new List<SubTaskViewModel>();

        public int Rows
        {
            get
            {
                //如果没有子任务
                if (Children.Count == 0 || (Children.Count == 1 && Children[0].Name == TaskName))
                {
                    return TaskName.Length / (Config.RowLength1 + Config.RowLength2) + (TaskName.Length % (Config.RowLength1 + Config.RowLength2) > 0 ? 1 : 0);
                }
                var childRows = Children.Sum(r => r.Rows);
                var selfRows = TaskName.Length / Config.RowLength1 + (TaskName.Length % Config.RowLength1 > 0 ? 1 : 0);
                return selfRows > childRows ? selfRows : childRows;
            }
        }
    }

    public class SubTaskViewModel
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public SubTaskStatus Status { get; set; }

        public int Rows => Name.Length / (Config.RowLength2) + (Name.Length % (Config.RowLength2) > 0 ? 1 : 0);
    }
}

