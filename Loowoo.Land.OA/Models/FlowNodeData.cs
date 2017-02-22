using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class FlowNodeData
    {
        public FlowNodeData()
        {
            CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int FlowDataId { get; set; }
        public int ParentId { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? Result { get; set; }
        public string Content { get; set; }
    }
}
