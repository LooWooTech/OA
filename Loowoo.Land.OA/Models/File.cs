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
    [Table("file")]
    public class File
    {
        public File()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string FileName { get; set; }

        public string SavePath { get; set; }
        /// <summary>
        /// 最小单位
        /// </summary>
        public long Size { get; set; }

        [NotMapped]
        public string DisplaySize
        {
            get
            {
                var k = 1024;
                var arr = new[] { "B", "K", "M", "G", "P", "B" };
                var i = (int)Math.Log(Size, k);
                var size = Size / Math.Pow(k, i);
                return (int)size + arr[i];

            }
        }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int InfoId { get; set; }

        public int ParentId { get; set; }

        public bool Inline { get; set; }
    }

    public enum FileType
    {
        [Description("文档")]
        Document,
        [Description("图片")]
        Image,
        [Description("视频")]
        Video,
        [Description("其他")]
        Other
    }
}
