using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("flow_data")]
    public class FlowData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 信息ID
        /// </summary>
        public int InfoId { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public int FormId { get; set; }
        /// <summary>
        /// 流程模板ID
        /// </summary>
        public int FlowId { get; set; }

        public virtual Flow Flow { get; set; }

        public bool Completed { get; set; }

        public virtual List<FlowNodeData> Nodes { get; set; }

        public FlowNodeData GetLastNodeData()
        {
            if (Nodes == null) return null;
            return Nodes.Where(e => e.ParentId == 0).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public FlowNodeData GetLastNodeData(int userId)
        {
            if (Nodes == null) return null;
            return Nodes.Where(e => e.UserId == userId).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public bool CanComplete(FlowNodeData data)
        {
            var lastNode = Flow.GetLastNode();
            return lastNode.ID == data.FlowNodeId;
        }

        public FlowNodeData GetFirstNodeData()
        {
            return Nodes.Where(e => e.ParentId == 0).OrderBy(e => e.ID).FirstOrDefault();
        }

        public FlowNodeData GetNextNodeData(int currentNodeDataId)
        {
            return Nodes.Where(e => e.ID > currentNodeDataId && e.ParentId == 0).OrderBy(e => e.ID).FirstOrDefault();
        }

        public FlowNodeData GetLastNodeDataByNodeId(int nodeId)
        {
            return Nodes.Where(e => e.FlowNodeId == nodeId && e.ParentId == 0).OrderBy(e => e.ID).LastOrDefault();
        }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        public bool CanCancel(int userId)
        {
            var nodeData = GetLastNodeData(userId);
            if (nodeData == null || !nodeData.Submited) return false;

            var nextNode = Flow.GetNextStep(nodeData.FlowNodeId);
            //如果当前步骤是最后一步，想从归档中撤回
            if (nextNode == null)
            {
                return nodeData.UserId == userId;
            }
            //否则判断当前步骤的下一步是否已经提交，如果提交，则不能撤回
            var nextNodeData = GetNextNodeData(nodeData.ID);
            return nextNodeData == null || (!nextNodeData.HasChanged() && !Nodes.Any(e => e.ParentId == nextNodeData.ID));
        }

        /// <summary>
        /// 是否可以退回
        /// </summary>
        public bool CanBack()
        {
            if (Nodes.Count == 0) return false;
            if (Nodes.Count == 1 && Nodes[0].Submited) return false;
            var lastNodeData = GetLastNodeData();
            var lastNode = Flow.GetFirstNode();
            return lastNodeData.FlowNodeId != lastNode.ID;
        }

        public FlowNodeData GetFlowNodeDataByStep(int flowStep)
        {
            var step = Flow.GetStep(flowStep);
            return Nodes.Where(e => e.ParentId == 0 && e.FlowNodeId == step.ID).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        //public bool CanEdit(int userId)
        //{
        //    //最新的节点是不是第一个节点
        //    var lastNode = Nodes.OrderByDescending(e => e.CreateTime).FirstOrDefault();
        //    return !lastNode.Result.HasValue && userId == lastNode.UserId;
        //}
    }
}
