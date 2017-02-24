using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class UserParameter:ParameterBase
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string SearchKey { get; set; }
    }
}
