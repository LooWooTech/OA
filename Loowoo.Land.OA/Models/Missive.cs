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

        public int RedTitleId { get; set; }

        public string WJ_BT { get; set; }
        /// <summary>
        /// 文件字号
        /// </summary>
        public string WJ_ZH { get; set; }
        /// <summary>
        /// 文件密级
        /// </summary>
        public WJMJ WJ_MJ { get; set; }
        /// <summary>
        /// 加急等级
        /// </summary>
        public JJDJ JJ_DJ { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string ZRR { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string DJR { get; set; }
        /// <summary>
        /// 政务公开
        /// </summary>
        public ZWGK ZW_GK { get; set; }
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
        /// 发文日期
        /// </summary>
        public DateTime? FW_RQ { get; set; }
        /// <summary>
        /// 交办日期
        /// </summary>
        public DateTime? JB_RQ { get; set; }
        /// <summary>
        /// 办理期限
        /// </summary>
        public DateTime? QX_RQ { get; set; }
        /// <summary>
        /// 公文来源
        /// </summary>
        public string WJ_LY { get; set; }
        /// <summary>
        /// 公开发布
        /// </summary>
        public int GK_FB { get; set; }

        public int ContentId { get; set; }

        [ForeignKey("ContentId")]
        public virtual File Content { get; set; }

        /// <summary>
        /// 是否为重要（局长、副局长填写了意见）
        /// </summary>
        public bool Important { get; set; }

        /// <summary>
        /// 是否不需要上报到市局OA
        /// </summary>
        public bool NotReport { get; set; }
    }

    /// <summary>
    /// 保密级别
    /// </summary>
    public enum WJMJ
    {
        [Description("普通")]
        Normal = 1,
        [Description("保密")]
        Secret,
    }

    public enum ZWGK
    {
        主动公开 = 1,
        依申请公开 = 2,
        不公开 = 3
    }

    /// <summary>
    /// 加急等级
    /// </summary>
    public enum JJDJ
    {
        [Description("普通")]
        Normal = 0,
        [Description("加急")]
        Fast = 1,
    }
}
