using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class SalaryParameter
    {
        public int Year { get; set; }

        public string SearchKey { get; set; }

        public int UserId { get; set; }

        public int SalaryId { get; set; }

        public PageParameter Page { get; set; }
    }
}
