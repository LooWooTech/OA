using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_right")]
    public class UserRight
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int GroupId { get; set; }
        /// <summary>
        /// 权限描述
        /// 固定格式：
        /// Form.Edit.[FormId].[FlowStep]  * 代表所有
        /// Url正则匹配：
        /// </summary>
        public string Name { get; set; }
    }
}
