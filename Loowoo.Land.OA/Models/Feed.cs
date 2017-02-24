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
    /// 动态广播
    /// </summary>
    [Table("Feed")]
    public class Feed
    {
        public Feed()
        {
            CreateTime = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public int FormId { get; set; }
        /// <summary>
        /// 公文等ID
        /// </summary>
        public int InfoId { get; set; }
        /// <summary>
        /// 订阅类型
        /// </summary>
        public int Type { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
