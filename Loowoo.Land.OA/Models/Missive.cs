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
    public class GONG_WEN
    {
        /// <summary>
        /// 公文文号
        /// </summary>
        public string GW_WH { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public GWMJ GW_MJ { get; set; }
        /// <summary>
        /// 承办人ID
        /// </summary>
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User UnderTaker { get; set; }
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
        public DateTime? YF_SJ { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? SX_SJ { get; set; }
        /// <summary>
        /// 主题词
        /// </summary>
        public string GW_ZTC { get; set; }
        /// <summary>
        /// 公文机关部门
        /// </summary>
        public int JG_BM { get; set; }
        /// <summary>
        /// 发往单位
        /// </summary>
        public string FW_DW { get; set; }
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
