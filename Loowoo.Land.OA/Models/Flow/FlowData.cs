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

        public FlowNodeData GetUserLastNodeData(int userId)
        {
            if (Nodes == null) return null;
            return Nodes.Where(e => e.UserId == userId).OrderByDescending(e => e.ID).FirstOrDefault();
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

        public FlowNodeData GetFlowNodeDataByStep(int flowStep)
        {
            var step = Flow.GetStep(flowStep);
            return Nodes.Where(e => e.ParentId == 0 && e.FlowNodeId == step.ID).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public FlowNodeData GetChildNodeData(int parentId)
        {
            return Nodes.Where(e => e.ParentId == parentId).OrderByDescending(e => e.ID).FirstOrDefault();
        }

        //public bool CanEdit(int userId)
        //{
        //    //最新的节点是不是第一个节点
        //    var lastNode = Nodes.OrderByDescending(e => e.CreateTime).FirstOrDefault();
        //    return !lastNode.Result.HasValue && userId == lastNode.UserId;
        //}
    }
}
