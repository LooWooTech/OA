using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("Flow")]
    public class Flow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual List<FlowNode> Nodes { get; set; }

        public FlowNode GetFirstNode()
        {
            return GetNextStep(0);
        }

        public FlowNode GetLastNode()
        {
            foreach(var node in Nodes)
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
    }
}
