using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Extend1Parameter
    {
        public int InfoId { get; set; }

        public int UserId { get; set; }

        public bool? Result { get; set; }

        public CheckStatus Status { get; set; }

        public DateTime? BeginTime { get; set; }

        public PageParameter Page { get; set; }
    }
}
