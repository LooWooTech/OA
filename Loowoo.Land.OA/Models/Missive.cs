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
    /// 公文
    /// </summary>
    [Table("missive")]
    public class Missive
    {
        public Missive()
        {
            CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 公文文号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Deleted { get; set; }
        /// <summary>
        /// 密级
        /// </summary>
        public Confidential Confidential { get; set; }
        //public int ConfidentialLevel { get; set; }
        //[NotMapped]
        //public ConfidentialLevel Level { get; set; }

        /// <summary>
        /// 承办人ID
        /// </summary>
        public int UserID { get; set; }
        [NotMapped]
        public User UnderTaker { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        public int CategoryID { get; set; }
        [NotMapped]
        public Category Category { get; set; }
        /// <summary>
        /// 缓急
        /// </summary>
        public EmergencyEnum EmergencyEnum { get; set; }
        //public int EmergencyID { get; set; }
        //[NotMapped]
        //public Emergency Emergency { get; set; }
        /// <summary>
        /// 印发时间
        /// </summary>
        public DateTime? PrintTime { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? EffectTime { get; set; }
        /// <summary>
        /// 主题词
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 公文机关部门
        /// </summary>
        public int BornOrganID { get; set; }
        [NotMapped]
        public Department Born { get; set; }
        /// <summary>
        /// 发往单位
        /// </summary>
        public int ToOrganID { get; set; }
        [NotMapped]
        public Department To { get; set; }
        public int NodeID { get; set; }
        [NotMapped]
        public FlowNode FlowNode { get; set; }
    }

    /// <summary>
    /// 保密级别
    /// </summary>
    public enum Confidential
    {

    }

    public enum EmergencyEnum
    {

    }
}
