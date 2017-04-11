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
        /// 判断用户是否可以撤销流程
        /// </summary>
        public bool CanCancel(int userId)
        {
            if (Completed) return false;
            if (Nodes == null || Nodes.Count == 0)
            {
                return false;
            }
            //获取用户最后一次提交的记录
            var lastNode = Nodes.Where(e => e.UserId == userId).OrderByDescending(e => e.CreateTime).FirstOrDefault();
            if (lastNode == null || !lastNode.Result.HasValue) return false;

            //如果已经有更新的提交，则不能撤销
            return !Nodes.Any(e => e.CreateTime > lastNode.CreateTime && e.Result.HasValue);
        }
    }
}
