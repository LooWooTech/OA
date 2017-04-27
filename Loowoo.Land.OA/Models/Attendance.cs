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
    /// <summary>
    /// 每日考勤结果表
    /// </summary>
    [Table("attendance")]
    public class Attendance
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public AttendanceResult AMResult { get; set; }

        public AttendanceResult PMResult { get; set; }
    }

    public enum AttendanceResult
    {
        [Description("正常")]
        Normal,
        [Description("缺勤")]
        Absent,
        [Description("迟到")]
        Late,
        [Description("早退")]
        Early,
        [Description("请假")]
        Leave
    }
}
