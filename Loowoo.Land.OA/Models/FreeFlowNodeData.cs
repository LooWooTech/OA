using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("freeflow_nodedata")]
    public class FreeFlowNodeData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int FreeFlowDataId { get; set; }

        public int ParentId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public string Signature { get; set; }

        public string DepartmentName { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

        [NotMapped]
        public bool Submited { get { return UpdateTime.HasValue; } }
    }
}
