using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Extend1ApplyParameter
    {
        public int UserId { get; set; }
        public int FormId { get; set; }
        public bool? Result { get; set; }
        public CheckStatus Status { get; set; }
        public PageParameter Page { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class Extend1Parameter : FormInfoParameter
    {
        public int ExtendInfoId { get; set; }

        public int ApprovalUserId { get; set; }

        public bool? Result { get; set; }

        public int Category { get; set; }
    }
}
