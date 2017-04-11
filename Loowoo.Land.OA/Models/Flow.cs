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

        public FlowNode GetNextStep(int nodeId)
        {
            return Nodes.FirstOrDefault(e => e.PrevId == nodeId);
        }
    }
}
