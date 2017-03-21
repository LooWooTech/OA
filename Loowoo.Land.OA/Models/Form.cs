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
    [Table("form")]
    public class Form
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public int FLowId { get; set; }
    }

    public enum FormType
    {
        [Description("公文")]
        Missive = 1,
        [Description("请假")]
        Leave = 2,
        [Description("任务")]
        Task = 3,

    }
}
