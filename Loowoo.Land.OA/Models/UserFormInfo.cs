using Newtonsoft.Json;
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
    /// <summary>
    /// 用户待办、办结、退回记录
    /// </summary>
    [Table("user_form_info")]
    public class UserFormInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 兼容旧版本的json序列化字段
        /// </summary>
        [JsonProperty("$id")]
        [NotMapped]
        public int JsonID => ID;
        [JsonProperty("$ref")]
        [NotMapped]
        public int JsonRefID => ID;

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public int InfoId { get; set; }

        [JsonIgnore]
        public virtual FormInfo Info { get; set; }

        /// <summary>
        /// 是否为抄送
        /// </summary>
        public bool CC { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 是否星标
        /// </summary>
        public bool Starred { get; set; }
        /// <summary>
        /// 是否提醒、催办
        /// </summary>
        public bool Reminded { get; set; }

        public FlowStatus FlowStatus { get; set; }
        /// <summary>
        /// 是否在回收站
        /// </summary>
        public bool Trash { get; set; }
    }
}
