using Newtonsoft.Json;
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
        /// <summary>
        /// 主键和FormInfo表主键一致
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public virtual FormInfo Info { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public bool HasAttachments { get; set; }

        public int ForwardId { get; set; }

        public int CreatorId { get; set; }

        public int ReplyId { get; set; }

        /// <summary>
        /// 草稿
        /// </summary>
        public bool IsDraft { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey("InfoId")]
        public virtual List<UserFormInfo> Users { get; set; }

        [NotMapped]
        public List<File> Attachments { get; set; }
    }

    [Table("user_mail")]
    public class UserMail : UserInfo
    {
        public string Subject { get; set; }

        public bool HasAttachments { get; set; }

        public int ForwardId { get; set; }

        public int ReplyId { get; set; }

        public bool IsDraft { get; set; }
    }

    //[Table("mail_user")]
    //public class UserMail
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int ID { get; set; }

    //    public int MailId { get; set; }

    //    public virtual Mail Mail { get; set; }

    //    public int UserId { get; set; }

    //    public virtual User User { get; set; }

    //    /// <summary>
    //    /// 是否为抄送
    //    /// </summary>
    //    public bool CC { get; set; }
    //    /// <summary>
    //    /// 是否已读
    //    /// </summary>
    //    public bool HasRead { get; set; }

    //    public bool Deleted { get; set; }
    //    /// <summary>
    //    /// 星标
    //    /// </summary>
    //    public bool Star { get; set; }
    //}
}
