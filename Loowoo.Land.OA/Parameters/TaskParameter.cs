using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class TaskParameter : PageParameter
    {
        public bool? Completed { get; set; }

        public string SearchKey { get; set; }
    }
}
