using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loowoo.Land.OA.Models
{
    [Table("sms")]
    public class Sms
    {
        public Sms()
        {
            CreateTime = DateTime.Now;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        /// <summary>
        /// 发送的手机号，逗号分隔
        /// </summary>
        public string Numbers { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
