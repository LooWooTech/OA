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
    /// <summary>
    /// 用户待办、办结、退回记录
    /// </summary>
    [Table("user_form_info")]
    public class UserFormInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int InfoId { get; set; }

        public int FormId { get; set; }

        public virtual FormInfo Info { get; set; }

        public FlowStatus Status { get; set; }

        public int FlowNodeDataId { get; set; }
    }

    public class UserFormInfoParameter : FormInfoParameter
    {
        public FlowStatus? Status { get; set; }

        public int UserId { get; set; }

        public bool? Completed { get; set; }
    }
}
