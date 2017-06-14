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

        [ForeignKey("FreeFlowDataId")]
        public virtual FreeFlowData FreeFlowData { get; set; }

        [NotMapped]
        public bool Submited
        {
            get { return Result.HasValue; }
        }

        public bool CanSubmit()
        {
            if (Result.HasValue) return false;
            //如果设置了自由流程
            if (FreeFlowData != null)
            {
                return FreeFlowData.Completed;
            }
            return true;
        }

        public bool CanCancel()
        {
            if (Result.HasValue) return false;
            return FreeFlowData == null;
        }

        public FreeFlowNodeData GetLastFreeNodeData(int userId)
        {
            return FreeFlowData?.GetLastNodeData(userId);
        }

        public bool CanSubmitFreeFlow(int userId)
        {
            if (FlowNode.FreeFlowId == 0) return false;

            if (FreeFlowData == null && userId == UserId) return true;

            if (FreeFlowData != null)
            {
                return FreeFlowData.CanSubmit(userId);
            }
            return false;
        }

        public bool CanCompleteFreeFlow(User user)
        {
            if (FlowNode.FreeFlowId == 0) return false;
            if (FreeFlowData == null || FreeFlowData.Completed) return false;
            return FlowNode.FreeFlow.IsCompleteUser(user);
        }
    }
}
