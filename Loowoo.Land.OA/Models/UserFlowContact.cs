using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_flow_contact")]
    public class UserFlowContact
    {
        [Key]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int ContactId { get; set; }

        public virtual User Contact { get; set; }
    }
}
