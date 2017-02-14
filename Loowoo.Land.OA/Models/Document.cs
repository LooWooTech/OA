using Newtonsoft.Json;
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
    public class Document
    {
        public Document()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public int ConfidentialLevel { get; set; }
        /// <summary>
        /// 承办（负责）人ID
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 承办人/负责人
        /// </summary>
        [NotMapped]
        public User UnderTaker { get; set; }
        /// <summary>
        /// 归档  立卷登记、销毁登记
        /// </summary>
        public Filing? Filing { get; set; }

        [NotMapped]
        public Flow Flow { get; set; }
    }

    public enum Filing
    {
        [Description("立卷登记")]
        Establish,
        [Description("销毁登记")]
        Destroy
    }



    /// <summary>
    /// 收文
    /// </summary>
    [Table("Receive_Document")]
    public class ReceiveDocument : Document
    {
        /// <summary>
        /// 种类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 缓急
        /// </summary>
        public int Emergency { get; set; }
        /// <summary>
        /// 收文字号
        /// </summary>
        public string ReceiveWord { get; set; }
        /// <summary>
        /// 主题词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 收文机关
        /// </summary>
        public string SWOrgan { get; set; }

        /// <summary>
        /// 来文单位 发往单位
        /// </summary>
        public string FromOrgan { get; set; }
        /// <summary>
        /// 印发时间
        /// </summary>
        public DateTime? PrintTime { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectTime { get; set; }

    }

    /// <summary>
    /// 发文
    /// </summary>
    [Table("Send_Document")]
    public class SendDocument : Document
    {

    }
}
