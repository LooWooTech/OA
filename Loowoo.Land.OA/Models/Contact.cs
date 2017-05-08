using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Avatar { get; set; }

        public string Mobile { get; set; }

        public string Signature { get; set; }

        public string RealName { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
