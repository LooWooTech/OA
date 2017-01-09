using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public bool Deleted { get; set; }

        public int ParentID { get; set; }

        public int Type { get; set; }
    }
}
