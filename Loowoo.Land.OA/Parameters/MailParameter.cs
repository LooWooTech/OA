using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class MailParameter
    {
        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public bool? Star { get; set; }

        public bool? Deleted { get; set; }

        public string SearchKey { get; set; }

        public PageParameter Page { get; set; }

        public bool? Draft { get; set; }
    }
}
