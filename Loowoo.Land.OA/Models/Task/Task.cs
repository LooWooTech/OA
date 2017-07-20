using Loowoo.Common;
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
    [Table("task")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public virtual FormInfo Info { get; set; }

        public string MC { get; set; }

        /// <summary>
        /// 任务来源
        /// </summary>
        public RWLY_LX LY_LX { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string LY { get; set; }

        public int ZRR_ID { get; set; }

        [ForeignKey("ZRR_ID")]
        public virtual User ZRR { get; set; }

        /// <summary>
        /// 工作目标
        /// </summary>
        public string GZ_MB { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? JH_SJ { get; set; }

        public string BZ { get; set; }
    }

    public enum RWLY_LX
    {
        [Description("省")]
        Province = 1,
        [Description("市")]
        City,
        [Description("区")]
        Area
    }
}