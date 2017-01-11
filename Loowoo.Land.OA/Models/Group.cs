using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Group
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public GroupType Type { get; set; }
    }

    public enum GroupType
    {
        System,
        User
    }
}
