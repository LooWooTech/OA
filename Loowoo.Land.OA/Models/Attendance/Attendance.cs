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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public AttendanceResult AMResult { get; set; } = AttendanceResult.Absent;

        public AttendanceResult PMResult { get; set; } = AttendanceResult.Absent;

        private bool IsLeave(IEnumerable<FormInfoExtend1> leaves, DateTime time)
        {
            return leaves.Any(e => e.ScheduleBeginTime <= time && e.ScheduleEndTime > time);
        }

        public void Check(IEnumerable<CheckInOut> logs, IEnumerable<FormInfoExtend1> leaves, AttendanceTime time)
        {
            var amLog = logs.FirstOrDefault(e => e.CreateTime >= time.AMBeginTime && e.CreateTime <= time.AMEndTime);
            if (amLog != null)
            {
                AMResult = AttendanceResult.Normal;
            }
            else if (leaves.Any(e => e.ScheduleBeginTime <= time.AMBeginTime && e.ScheduleEndTime >= time.AMEndTime))
            {
                AMResult = AttendanceResult.Leave;
            }
            else
            {
                AMResult = AttendanceResult.Absent;
            }
            var pmLog = logs.LastOrDefault(e => e.CreateTime >= time.PMBeginTime && e.CreateTime <= time.PMEndTime);
            if (pmLog != null)
            {
                PMResult = AttendanceResult.Normal;
            }
            else if (leaves.Any(e => e.ScheduleBeginTime <= time.AMBeginTime && e.ScheduleEndTime >= time.AMEndTime))
            {
                PMResult = AttendanceResult.Leave;
            }
            else
            {
                PMResult = AttendanceResult.Absent;
            }
        }
    }

    public class AttendanceTime
    {
        /// <summary>
        /// 上班最早打卡时间
        /// </summary>
        public DateTime AMBeginTime { get; set; }
        /// <summary>
        /// 上班最晚打卡时间
        /// </summary>
        public DateTime AMEndTime { get; set; }
        /// <summary>
        /// 下班最早打卡时间
        /// </summary>
        public DateTime PMBeginTime { get; set; }
        /// <summary>
        /// 下班最晚打卡时间
        /// </summary>
        public DateTime PMEndTime { get; set; }
        /// <summary>
        /// 是否未打卡时间
        /// </summary>
        public bool IsCheckTime(DateTime time)
        {
            return (time >= AMBeginTime && time <= AMEndTime) || (time >= PMBeginTime && time <= PMEndTime);
        }

        /// <summary>
        /// 获取最近的时间点
        /// </summary>
        public DateTime GetLatestTime(DateTime time)
        {
            if (time <= AMBeginTime) return AMBeginTime;
            if (time <= AMEndTime) return AMEndTime;
            if (time <= PMBeginTime) return PMBeginTime;
            if (time <= PMEndTime) return PMEndTime;
            return time.Date.AddDays(1);
        }
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

    public enum LeaveType
    {
        [Description("公事")]
        Official = 1,
        [Description("私事")]
        Personal = 2,
        [Description("病假")]
        Sick = 3,

    }
}
