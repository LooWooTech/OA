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
        Draft = 0,
        [Description("在办")]
        Doing = 1,
        [Description("已办")]
        Done = 2,
        [Description("完结")]
        Completed = 3,
        [Description("退回")]
        Back = 4
    }
}
