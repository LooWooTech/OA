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
    [Table("user_form")]
    public class UserForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FormID { get; set; }
        public int InfoID { get; set; }
        public State State { get; set; }
    }

    public enum State
    {
        [Description("待办")]
        None,
        [Description("办结")]
        Done,
        [Description("退回")]
        Roll,
        [Description("完结")]
        Finish
    }
}
