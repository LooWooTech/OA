using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class PagingResult<T>
    {
        public IEnumerable<T> List { get; set; }
        public PageParameter Page { get; set; }
    }

    public class PagingResult : PagingResult<dynamic>
    {
    }
}
