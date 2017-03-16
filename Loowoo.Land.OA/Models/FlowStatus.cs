using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public enum FlowStatus
    {
        [Description("草稿")]
        Draft,
        [Description("在办")]
        Doing,
        [Description("已办")]
        Done,
        [Description("完结")]
        Completed,
        [Description("退回")]
        Back
    }
}
