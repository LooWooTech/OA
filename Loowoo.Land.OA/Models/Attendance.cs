using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("attendance")]
    public class Attendance
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public AttendanceState State { get; set; }

    }

    public enum AttendanceState
    {
        Normal,
        Absent,
        Late,
        Early
    }

    public class AttendanceStatistic
    {
        public int Normal { get; set; }
        public int Late { get; set; }
        public int Early { get; set; }
        public int Absent { get; set; }
        public int OfficialLeave { get; set; }
        public int PersonLeave { get; set; }
    }
}
