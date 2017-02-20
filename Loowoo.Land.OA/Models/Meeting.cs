using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("meeting")]
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 会议时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 会议简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 会议室
        /// </summary>
        public int Room { get; set; }

    }

    /// <summary>
    /// 会议室
    /// </summary>
    [Table("meeting_room")]
    public class MeetingRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
    }
}
