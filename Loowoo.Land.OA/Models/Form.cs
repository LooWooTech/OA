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

        [Required]
        public string Name { get; set; }

        [Required]
        public string EName { get; set; }

        [NotMapped]
        public FormType FormType => (FormType)ID;

        public int FLowId { get; set; }
    }

    public enum FormType
    {
        [Description("发文")]
        SendMissive = 1,
        [Description("收文")]
        ReceiveMissive = 2,
        [Description("用车")]
        Car = 3,
        [Description("任务")]
        Task = 4,
        [Description("会议室")]
        MeetingRoom = 5,
        [Description("公章")]
        Seal = 6,
        [Description("请假")]
        Leave = 7,
    }
}
