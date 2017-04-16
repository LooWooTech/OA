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
    /// 公文
    /// </summary>
    public class Missive
    {
        [FormInfoField(Name = "Title")]
        public string WJ_BT { get; set; }

        /// <summary>
        /// 公文文号
        /// </summary>
        [FormInfoField(Name = "Keywords")]
        public string GW_WH { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public GWMJ GW_MJ { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public int ZZR_ID { get; set; }
        public string ZRR { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        public string GW_ZL { get; set; }
        /// <summary>
        /// 缓急
        /// </summary>
        public GWHJ GW_HJ { get; set; }
        /// <summary>
        /// 印发时间
        /// </summary>
        public DateTime? FW_RQ { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? SX_RQ { get; set; }
        /// <summary>
        /// 主题词
        /// </summary>
        public string GW_ZTC { get; set; }
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
        public bool SF_FB_WWW { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        public DateTime? QX_RQ { get; set; }

        public File Word { get; set; }

        public File[] Excels { get; set; }
    }

    /// <summary>
    /// 保密级别
    /// </summary>
    public enum GWMJ
    {

    }

    public enum GWHJ
    {

    }
}
