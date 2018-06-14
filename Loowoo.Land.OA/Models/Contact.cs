using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("contact")]
    public class Contact
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OwnerId { get; set; }

        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual User ContactUser { get; set; }

        public string Mobile { get; set; }

        public string RealName { get; set; }

        public string Description { get; set; }
    }
}
