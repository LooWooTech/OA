using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    /// <summary>
    /// 步骤
    /// </summary>
    [Table("step")]
    public class Step
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前流程属于哪一类
        /// </summary>
        public int InfoType { get; set; }
        /// <summary>
        /// 流程顺序号
        /// </summary>
        public int SerialNumber { get; set; }
        [NotMapped]
        public List<StepUser> StepUser { get; set; }

    }
    /// <summary>
    /// 步骤与审核人的关系 
    /// 当前步骤的审核人 一人 或者多人
    /// </summary>
    [Table("step_user")]
    public class StepUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 审核流程ID
        /// </summary>
        public int StepID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
}
