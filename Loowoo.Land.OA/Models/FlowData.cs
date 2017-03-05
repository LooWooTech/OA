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
        [NotMapped]
        public List<FlowNodeData> Nodes { get; set; }
    }
}
