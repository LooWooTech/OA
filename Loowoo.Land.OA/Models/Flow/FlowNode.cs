using Loowoo.Common;
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

        [Column("UserIds")]
        public string UserIdsValues { get; set; }

        [NotMapped]
        public int[] UserIds
        {
            get
            {
                if (string.IsNullOrEmpty(UserIdsValues)) return null;
                return UserIdsValues.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    UserIdsValues = null;
                else
                    UserIdsValues = string.Join(",", value);
            }
        }

        public DepartmentLimitMode LimitMode { get; set; } = DepartmentLimitMode.Assign;

        [Column("DepartmentIds")]
        public string DepartmentIdsValue { get; set; }

        [NotMapped]
        public int[] DepartmentIds
        {
            get
            {
                if (string.IsNullOrEmpty(DepartmentIdsValue)) return null;
                return DepartmentIdsValue.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    DepartmentIdsValue = null;
                else
                    DepartmentIdsValue = string.Join(",", value);
            }
        }

        [Column("JobTitleIds")]
        public string JobTitleIdsValue { get; set; }

        [NotMapped]
        public int[] JobTitleIds
        {
            get
            {
                if (string.IsNullOrEmpty(JobTitleIdsValue)) return null;
                return JobTitleIdsValue.ToIntArray();
            }
            set
            {
                if (value == null || value.Length == 0)
                    JobTitleIdsValue = null;
                else
                    JobTitleIdsValue = string.Join(",", value);
            }
        }

        public int FreeFlowId { get; set; }

        [ForeignKey("FreeFlowId")]
        public virtual FreeFlow FreeFlow { get; set; }

        public int PrevId { get; set; }

        /// <summary>
        /// 是否可以结束流程
        /// </summary>
        public bool CanComplete { get; set; }
    }

}
