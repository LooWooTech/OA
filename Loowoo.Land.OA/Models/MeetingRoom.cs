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
    [Table("meetingroom")]
    public class MeetingRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        public int PhotoId { get; set; }

        public virtual File Photo { get; set; }

        public MeetingRoomType Type { get; set; }

        public MeetingRoomStatus Status { get; set; }

        /// <summary>
        /// 最后一次状态更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }
    }

    public enum MeetingRoomType
    {
        [Description("小型")]
        Small = 1,
        [Description("中型")]
        Middle,
        [Description("大型")]
        Big
    }

    public enum MeetingRoomStatus
    {
        [Description("空闲")]
        Unused,
        [Description("使用中")]
        Using,
        [Description("维护中")]
        Repair
    }
}
