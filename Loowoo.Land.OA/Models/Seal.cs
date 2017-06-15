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
    [Table("seal")]
    public class Seal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public SealStatus Status { get; set; }

        public bool Deleted { get; set; }
    }

    public enum SealStatus
    {
        [Description("空闲")]
        Unused,
        [Description("使用中")]
        Using
    }
}
