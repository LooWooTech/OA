using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Directory
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Name { get; set; }
    }
}
