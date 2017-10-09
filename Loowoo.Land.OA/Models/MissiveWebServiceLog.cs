using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("missive_webservice_log")]
    public class MissiveWebServiceLog
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MessiveID { get; set; }

        public string Uid { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }

        public bool Result { get; set; }

        public FormType Type { get; set; }
    }
}
