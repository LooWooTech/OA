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
        public DateTime Time { get; set; }
        /// <summary>
        /// 早上上班时间
        /// </summary>
        public DateTime MorningTime { get; set; }
        /// <summary>
        /// 早上打卡时间
        /// </summary>
        public DateTime? SigninTime { get; set; }
        /// <summary>
        /// 下午下班时间
        /// </summary>
        public DateTime AfternoonTime { get; set; }
        public DateTime? ExistTime { get; set; }
        public int UserID { get; set; }
        [NotMapped]
        public AttendanceState On
        {
            get
            {
                if (SigninTime.HasValue)
                {
                    if (SigninTime.Value > MorningTime)
                    {
                        return AttendanceState.迟到;
                    }
                    return AttendanceState.正常;
                }
                return AttendanceState.缺勤;
            }
        }
        [NotMapped]
        public AttendanceState Off
        {
            get
            {
                if (ExistTime.HasValue)
                {
                    if (AfternoonTime > ExistTime.Value)
                    {
                        return AttendanceState.早退;
                    }

                    return AttendanceState.正常;
                }
                return AttendanceState.缺勤;
            }
        }

    }

    public enum AttendanceState
    {
        正常,
        缺勤,
        迟到,
        早退
    }
}
