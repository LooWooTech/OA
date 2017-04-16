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

        /// <summary>
        /// 判断用户是否可以提交
        /// </summary>
        public bool CanSubmit(int userId)
        {
            if (Completed) return false;
            if (Nodes == null || Nodes.Count == 0)
            {
                return true;
            }
            var lastNode = Nodes.Last();
            return lastNode.UserId == userId && !lastNode.Result.HasValue;
        }

        /// <summary>
        /// 判断用户是否可以撤销流程TODO 不支持带分支的流程
        /// </summary>
        public bool CanCancel(int userId)
        {
            if (Completed) return false;
            if (Nodes == null || Nodes.Count == 0)
            {
                return false;
            }
            //获取用户最后一次提交的记录
            var lastNodeData = GetLastNodeData(userId);
            if (lastNodeData == null || !lastNodeData.Result.HasValue) return false;

            //如果已经有更新的提交，则不能撤销
            return !Nodes.Any(e => e.ID > lastNodeData.ID && e.Result.HasValue);
        }

        public FlowNodeData GetLastNodeData(int userId = 0)
        {
            var query = Nodes.AsQueryable();
            if (userId > 0)
            {
                query = query.Where(e => e.UserId == userId);
            }
            return query.OrderByDescending(e => e.ID).FirstOrDefault();
        }

        public bool CanComplete(FlowNodeData data)
        {
            var lastNode = Flow.GetLastNode();
            return data.Result == true && lastNode.ID == data.FlowNodeId;
        }

        public FlowNodeData GetFirstNodeData()
        {
            return Nodes.OrderBy(e => e.ID).FirstOrDefault();
        }

        //public bool CanEdit(int userId)
        //{
        //    //最新的节点是不是第一个节点
        //    var lastNode = Nodes.OrderByDescending(e => e.CreateTime).FirstOrDefault();
        //    return !lastNode.Result.HasValue && userId == lastNode.UserId;
        //}
    }
}
