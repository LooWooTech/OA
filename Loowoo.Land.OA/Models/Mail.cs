using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("mail")]
    public class Mail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int CreatorId { get; set; }

        public bool HasAttachments { get; set; }

        public int ForwardId { get; set; }

        /// <summary>
        /// 草稿
        /// </summary>
        public bool IsDraft { get; set; }

        public string ToUserIds { get; set; }

        public string CcUserIds { get; set; }

        [NotMapped]
        public int[] AttachmentIds { get; set; }
    }

    [Table("mail_user")]
    public class UserMail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MailId { get; set; }

        public virtual Mail Mail { get; set; }

        public int FromUserId { get; set; }

        public virtual User FromUser { get; set; }

        public int ToUserId { get; set; }

        public virtual User ToUser { get; set; }
        /// <summary>
        /// 是否为抄送
        /// </summary>
        public bool CC { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool HasRead { get; set; }

        public bool Deleted { get; set; }
        /// <summary>
        /// 星标
        /// </summary>
        public bool Star { get; set; }
    }
}
