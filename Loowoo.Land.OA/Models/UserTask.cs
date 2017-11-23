using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_task")]
    public class UserTask : UserInfo
    {
        public string Number { get; set; }

        public TaskFromType FromType { get; set; }

        public string From { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public string Goal { get; set; }
    }
}
