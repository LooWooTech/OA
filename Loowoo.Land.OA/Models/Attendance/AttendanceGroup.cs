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
    [Table("attendance_group")]
    public class AttendanceGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Default { get; set; }

        public string AMBeginTime { get; set; }

        public string AMEndTime { get; set; }

        public string PMBeginTime { get; set; }

        public string PMEndTime { get; set; }

        public string API { get; set; }
    }
}
