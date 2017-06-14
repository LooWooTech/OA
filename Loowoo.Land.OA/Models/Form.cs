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
        public FormType FormType
        {
            get
            {
                FormType type;
                if (Enum.TryParse(EName, true, out type))
                {
                    return type;
                }
                return 0;
            }
        }

        public int FLowId { get; set; }
    }

    public enum FormType
    {
        [Description("公文")]
        Missive = 1,
        [Description("车辆")]
        Car = 2,
        [Description("请假")]
        Leave = 3,
        [Description("任务")]
        Task = 4,
        [Description("会议室")]
        MeetingRoom = 5,
        [Description("公章")]
        Seal = 6,

    }
}
