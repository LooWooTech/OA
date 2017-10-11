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

        public List<MasterTaskViewModel> Rows { get; set; }
    }

    public class MasterTaskViewModel
    {
        public DateTime CompleteDate { get; set; }

        public string TaskName { get; set; }

        public string Department { get; set; }

        public List<SubTaskViewModel> Rows { get; set; }
    }

    public class SubTaskViewModel
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public string Status { get; set; }
    }
}

