using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("flow_node")]
    public class FlowNode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int FlowId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int DepartmentId { get; set; }
        /// <summary>
        /// 上一个节点
        /// </summary>
        public int BackNodeID { get; set; }
        
        [NotMapped]
        public string Condition { get; set; }
    }

}
