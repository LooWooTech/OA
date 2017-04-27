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

        public DepartmentLimitMode LimitMode { get; set; } = DepartmentLimitMode.Assign;

        [Column("DepartmentIds")]
        public string DepartmentIdsValue { get; set; }

        [NotMapped]
        public int[] DepartmentIds
        {
            get
            {
                if (string.IsNullOrEmpty(DepartmentIdsValue)) return null;
                return DepartmentIdsValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => int.Parse(id)).ToArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    DepartmentIdsValue = null;
                else
                    DepartmentIdsValue = string.Join(",", value);
            }
        }

        public int JobTitleId { get; set; }

        public int FreeFlowId { get; set; }

        [ForeignKey("FreeFlowId")]
        public virtual FreeFlow FreeFlow { get; set; }

        public int PrevId { get; set; }

        [NotMapped]
        public FlowNode Prev { get; set; }

        [NotMapped]
        public FlowNode Next { get; set; }
    }

}
