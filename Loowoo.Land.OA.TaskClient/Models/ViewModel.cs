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

        public List<MasterTaskViewModel> Rows { get; set; } = new List<MasterTaskViewModel>();
    }

    public class MasterTaskViewModel
    {
        public int ID { get; internal set; }

        public DateTime? ScheduleDate { get; set; }

        public string TaskName { get; set; }

        public string Department { get; set; }

        public List<SubTaskViewModel> Rows { get; set; } = new List<SubTaskViewModel>();

        public int RowsHeight => Rows.Sum(e => e.Rows);
    }

    public class SubTaskViewModel
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public SubTaskStatus Status { get; set; }

        public int Rows => Name.Length / Config.RowHeight + Name.Length % Config.RowHeight > 0 ? 1 : 0;
    }
}

