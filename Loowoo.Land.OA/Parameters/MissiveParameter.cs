using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class MissiveParameter
    {
        public int FormId { get; set; }

        public string SearchKey { get; set; }

        public int[] Ids { get; set; }
    }
}
