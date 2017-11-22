using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("message")]
    public class Message
    {
        public Message()
        {
        }

        public Message(Feed feed)
        {
            Content = feed.Title ?? feed.Description;
            InfoId = feed.InfoId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int InfoId { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual FormInfo Info { get; set; }
    }

    [Table("message_user")]
    public class UserMessage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MessageId { get; set; }

        public virtual Message Message { get; set; }

        public int UserId { get; set; }

        public bool HasRead { get; set; }

        public bool Deleted { get; set; }

        public virtual User User { get; set; }
    }


}
