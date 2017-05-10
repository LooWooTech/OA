using Loowoo.Common;
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
    public class FeedParameter
    {
        public int FormId { get; set; }

        public int[] InfoIds { get; set; }

        public DateTime? BeginTime { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public int[] UserIds { get; set; }

        public PageParameter Page { get; set; }
    }
}
