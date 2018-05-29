using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_extend1")]
    public class UserFormExtend1 : UserInfo
    {
        public int ExtendInfoId { get; set; }

        public int ApplyUserId { get; set; }

        public string ApplyUserName => ApplyUser?.RealName;

        [JsonIgnore]
        public virtual User ApplyUser { get; set; }

        public int ApprovalUserId { get; set; }

        [NotMapped]
        public string ApprovalUserName => ApprovalUser?.RealName;

        [JsonIgnore]
        public virtual User ApprovalUser { get; set; }

        public DateTime ScheduleBeginTime { get; set; }

        public DateTime? ScheduleEndTime { get; set; }

        public DateTime? RealEndTime { get; set; }

        public string Reason { get; set; }

        public int Category { get; set; }

        public bool? Result { get; set; }
    }
}
