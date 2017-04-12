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
        public string Department { get; set; }

        public DateTime? UpdateTime { get; set; }
        public bool? Result { get; set; }
        public string Content { get; set; }

        public int Step { get; internal set; }
    }
}
