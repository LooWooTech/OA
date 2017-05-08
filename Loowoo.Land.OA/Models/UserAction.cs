using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_action")]
    public class UserAction
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }

        public string Description { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public ActionType Type { get; set; }

        public int InfoId { get; set; }

        public string Extend { get; set; }
    }



    public enum ActionType
    {

    }
}
