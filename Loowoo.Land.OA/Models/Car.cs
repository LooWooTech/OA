using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("car")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        public int PhotoId { get; set; }

        public virtual File Photo { get; set; }

        public CarType Type { get; set; }

        public CarStatus Status { get; set; }

        /// <summary>
        /// 最后一次状态更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }
    }

    public enum CarType
    {
        [Description("轿车")]
        Sedan = 1,
        [Description("越野车")]
        Suv,
        [Description("其他")]
        Other
    }

    public enum CarStatus
    {
        [Description("闲置")]
        Unused,
        [Description("使用中")]
        Using,
        [Description("维修中")]
        Repair
    }
}
