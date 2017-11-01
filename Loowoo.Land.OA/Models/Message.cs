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
            CreateTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public int FeedId { get; set; }

        public virtual Feed Feed { get; set; }        
    }

    [Table("message_user")]
    public class UserMessage
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MessageId { get; set; }

        public virtual Message Message { get; set; }

        public int FromId { get; set; }

        public int ToUserId { get; set; }

        public bool HasRead { get; set; }

        public bool Deleted { get; set; }

        public virtual User FromUser { get; set; }

        public virtual User ToUser { get; set; }
    }


}
