using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class MessageParameter
    {
        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public bool? HasRead { get; set; }

        public PageParameter Page { get; set; }
    }
}
