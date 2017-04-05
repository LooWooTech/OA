using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("holiday")]
    public class Holiday
    {
        public Holiday()
        {
            CreateTime = DateTime.Now;
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 节日名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节假日起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 节假日结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
