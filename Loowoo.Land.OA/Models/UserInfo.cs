using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_info")]
    public class UserInfo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int InfoId { get; set; }

        public int FormId { get; set; }

        public virtual Form Form { get; set; }

        public string Title { get; set; }

        public int PostUserId { get; set; }

        [ForeignKey("PostUserId")]
        public virtual User Poster { get; set; }

        public DateTime CreateTime { get; set; }

        public int FlowDataId { get; set; }

        public virtual FlowData FlowData { get; set; }

        public string FlowStep { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User ToUser { get; set; }

        public FlowStatus FlowStatus { get; set; }

        /// <summary>
        /// 是否为抄送
        /// </summary>
        public bool CC { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// 是否移动到回收站
        /// </summary>
        public bool Trash { get; set; } 
        /// <summary>
        /// 是否星标
        /// </summary>
        public bool Starred { get; set; }
        /// <summary>
        /// 是否提醒、催办
        /// </summary>
        public bool Reminded { get; set; }
        /// <summary>
        /// 是否为抄送
        /// </summary>
        public bool Cc { get; set; }
    }
}
