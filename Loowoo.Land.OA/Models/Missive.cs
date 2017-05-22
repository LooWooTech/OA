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
    /// 公文
    /// </summary>
    [Table("missive")]
    public class Missive
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public virtual FormInfo Info { get; set; }

        public string WJ_BT { get; set; }
        /// <summary>
        /// 公文文号
        /// </summary>
        public string WH { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public GWMJ MJ { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string ZRR { get; set; }

        public ZWGK ZWGK { get; set; }
        /// <summary>
        /// 印发时间
        /// </summary>
        public DateTime? FW_RQ { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? SX_SJ { get; set; }
        /// <summary>
        /// 主题词
        /// </summary>
        public string ZTC { get; set; }
        /// <summary>
        /// 主送机关
        /// </summary>
        public string ZS_JG { get; set; }
        /// <summary>
        /// 抄送机关
        /// </summary>
        public string CS_JG { get; set; }
        /// <summary>
        /// 是否上互联网发布
        /// </summary>
        public bool HLW_FB { get; set; }
        /// <summary>
        /// 是否公开发布
        /// </summary>
        public bool GKFB { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        public DateTime? QX_RQ { get; set; }
        /// <summary>
        /// 公文来源
        /// </summary>
        public string LY { get; set; }

        public int WordId { get; set; }

        [ForeignKey("WordId")]
        public virtual File Word { get; set; }
    }

    /// <summary>
    /// 保密级别
    /// </summary>
    public enum GWMJ
    {
        [Description("无")]
        None,
        [Description("保密")]
        Protect
    }

    public enum ZWGK
    {
        主动公开 = 1,
        依申请公开 = 2,
        不公开 = 3
    }
}
