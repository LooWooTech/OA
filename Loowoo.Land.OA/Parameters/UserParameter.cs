using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class UserParameter : ParameterBase
    {
        public int UserId { get; set; }

        public int[] UserIds { get; set; }

        public int DepartmentId { get; set; }

        public int[] DepartmentIds { get; set; }

        public int GroupId { get; set; }

        public int[] GroupIds { get; set; }

        public int TitleId { get; set; }

        public int[] TitleIds { get; set; }

        public string SearchKey { get; set; }
    }
}
