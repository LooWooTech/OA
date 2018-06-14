using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("user_missive")]
    public class UserMissive : UserInfo
    {
        public string WJ_ZH { get; set; }

        public WJMJ WJ_MJ { get; set; }

        public string WJ_LY { get; set; }

        public DateTime? QX_RQ { get; set; }

        public bool Important { get; set; }

        public string Uid { get; set; }
    }
}
