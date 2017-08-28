using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    /// <summary>
    /// 车辆、会议室、公章、请假的申请表单
    /// </summary>
    [Table("form_info_extend1")]
    public class FormInfoExtend1
    {
        /// <summary>
        /// 申请ID，和FormInfoID 一一对应
        /// </summary>
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        /// <summary>
        /// 对应所申请的信息的ID（车辆ID，会议室ID等）
        /// </summary>
        public int InfoId { get; set; }

        public int Category { get; set; }

        [ForeignKey("ID")]
        public virtual FormInfo Info { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime ScheduleBeginTime { get; set; }

        public DateTime? ScheduleEndTime { get; set; }

        public DateTime? RealEndTime { get; set; }

        public string Reason { get; set; }

        /// <summary>
        /// 最后审批人
        /// </summary>
        public int ApprovalUserId { get; set; }
        public virtual User ApprovalUser { get; set; }
        /// <summary>
        /// 最后审批结果
        /// </summary>
        public bool? Result { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
