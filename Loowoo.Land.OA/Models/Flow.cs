using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class FlowTemplate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int InfoType { get; set; }

        public bool Disabled { get; set; }

        public List<FlowStepTemplate> Steps { get; set; }
    }

    public class FlowStepTemplate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int FlowID { get; set; }

        public int DepartmentID { get; set; }

        public int UserID { get; set; }

        /// <summary>
        /// 属于当前流程第几步
        /// </summary>
        public int Step { get; set; }
    }
    [Table("Flow")]
    public class Flow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public int InfoID { get; set; }
        /// <summary>
        /// 信息类型 0：收文 1：发文  2：任务 3:车辆审批 4：会议
        /// </summary>
        public int InfoType { get; set; }
        [NotMapped]
        public List<FlowStep> Steps { get; set; }
    }

    /// <summary>
    /// 办理情况
    /// </summary>
    [Table("FlowStep")]
    public class FlowStep
    {
        public FlowStep()
        {
            CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public int FlowID { get; set; }
        [NotMapped]
        public User User { get; set; }

        public bool? Result { get; set; }

        public string Content { get; set; }

        public int Step { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }


}
