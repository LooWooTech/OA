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
        /// 上一节点ID
        /// </summary>
        public int PrevId { get; set; }
        public virtual FlowNode Prev { get; set; }

        ///// <summary>
        ///// 上一节点
        ///// </summary>
        //[NotMapped]
        //public FlowNode Prev { get; set; }
        /// <summary>
        /// 下一节点
        /// </summary>
        [NotMapped]
        public FlowNode Next { get; set; }

        public virtual User User { get; set; }

        public virtual Group Group { get; set; }

        public virtual Department Department { get; set; }
    }

}
