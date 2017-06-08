using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class CarApplyParameter
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public bool? Result { get; set; }

        public DateTime? BeginTime { get; set; }

        public PageParameter Page { get; set; }
    }
}
