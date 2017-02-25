using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    /// <summary>
    /// 动态查询参数类
    /// </summary>
    public class FeedParameter:ParameterBase
    {
        public int? InfoType { get; set; }
        public DateTime? BeginTime { get; set; }
        public int? UserId { get; set; }
    }
}
