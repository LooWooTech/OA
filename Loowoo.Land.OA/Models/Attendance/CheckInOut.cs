using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loowoo.Land.OA.Models
{
    /// <summary>
    /// 打卡记录
    /// </summary>
    [Table("check_in_out")]
    public class CheckInOut
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;


        public bool? ApiResult { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
