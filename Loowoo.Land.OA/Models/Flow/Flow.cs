using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("flow")]
    public class Flow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 不同意是否可以退回发起人，如果不可以，则直接结束流程
        /// </summary>
        public bool CanBack { get; set; }

        public virtual List<FlowNode> Nodes { get; set; }

        public FlowNode GetFirstNode()
        {
            return GetNextStep(0);
        }

        public FlowNode GetLastNode()
        {
            foreach (var node in Nodes)
            {
                if (!Nodes.Any(e => e.PrevId == node.ID))
                {
                    return node;
                }
            }
            throw new Exception("配成节点配置有错误");
        }

        public FlowNode GetNextStep(int nodeId)
        {
            return Nodes.FirstOrDefault(e => e.PrevId == nodeId);
        }

        /// <summary>
        /// 获取第几步的FlowNode
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public FlowNode GetStep(int step)
        {
            FlowNode prevStep = null;
            while (step > 0)
            {
                var nextStep = GetNextStep(prevStep == null ? 0 : prevStep.ID);
                if (nextStep == null)
                {
                    break;
                }
                prevStep = nextStep;
                step--;
            }
            return prevStep;
        }
    }
}
