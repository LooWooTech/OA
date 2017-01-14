using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public int ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Deleted { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public int ConfidentialLevel { get; set; }
    }

    public class ReceiveDocument : Document
    {
        /// <summary>
        /// 来文单位
        /// </summary>
        public string FromOrgan { get; set; }
        /// <summary>
        /// 签收日期
        /// </summary>
        public DateTime ReceiveDate { get; set; }
    }

    public class SendDocument : Document
    {
        /// <summary>
        /// 主题词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 主送机关
        /// </summary>
        public string ToOrgan { get; set; }
        /// <summary>
        /// 抄送机关
        /// </summary>
        public string CcOrgan { get; set; }
        /// <summary>
        /// 期限日
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// 发文日期
        /// </summary>
        public DateTime SendTime { get; set; }

        public User Creator { get; set; }

        public FlowStep FlowStep { get; set; }
    }
}
