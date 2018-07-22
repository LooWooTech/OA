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
    [Table("flow_node_data")]
    public class FlowNodeData
    {
        public FlowNodeData()
        {
            CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        //public int ParentId { get; set; }
        public int FlowNodeId { get; set; }

        public string FlowNodeName { get; set; }

        [ForeignKey("FlowNodeId")]
        public virtual FlowNode FlowNode { get; set; }

        public int FlowDataId { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }

        public string Signature { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool? Result { get; set; }

        public string Content { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        /// 相关联的id
        /// </summary>
        public int ExtendId { get; set; }

        [ForeignKey("FreeFlowDataId")]
        public virtual FreeFlowData FreeFlowData { get; set; }

        [NotMapped]
        public bool Submited
        {
            get { return Result.HasValue; }
        }

        public bool HasChanged()
        {
            return Result.HasValue || FreeFlowData != null;
        }

        public FreeFlowNodeData GetLastFreeNodeData(int userId)
        {
            return FreeFlowData?.GetLastNodeData(userId);
        }
    }
}
