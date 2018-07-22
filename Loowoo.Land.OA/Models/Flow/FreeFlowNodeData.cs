using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("freeflow_nodedata")]
    public class FreeFlowNodeData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 兼容旧版本的json序列化字段
        /// </summary>
        [JsonProperty("$id")]
        [NotMapped]
        public int JsonID => ID;
        [JsonProperty("$ref")]
        [NotMapped]
        public int JsonRefID => ID;

        public int FreeFlowDataId { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        /// 是否为抄送（不需要填写意见）
        /// </summary>
        public bool IsCc { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public string Signature { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

        [NotMapped]
        public bool Submited { get { return UpdateTime.HasValue; } }
    }
}
