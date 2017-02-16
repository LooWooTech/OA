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
    /// 安全日志记录
    /// </summary>
    [Table("diary")]
    public class Diary
    {
        public Diary()
        {
            Time = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public int Equipment { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; }
        [NotMapped]
        public User User { get; set; }
    }
}
