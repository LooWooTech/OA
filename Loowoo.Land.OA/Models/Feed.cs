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
    /// 动态广播
    /// </summary>
    [Table("Feed")]
    public class Feed
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int CreatorID { get; set; }

        public int InfoID { get; set; }

        public int InfoType { get; set; }

        public DateTime CreateTime { get; set; }

        public string Summary { get; set; }

        public int CommentCount { get; set; }

        public int StarCount { get; set; }
        public bool Deleted { get; set; }
    }
}
