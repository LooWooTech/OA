using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("missive_service_log")]
    public class MissiveServiceLog
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MissiveId { get; set; }

        /// <summary>
        /// 第三方OA的公文ID
        /// </summary>
        public string Uid { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

        public bool? Result { get; set; }

        public FormType Type { get; set; }
    }
}
