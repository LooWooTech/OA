using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class AttendanceStatistic
    {
        public int Normal { get; set; }
        public int Late { get; set; }
        public int Early { get; set; }
        public int Absent { get; set; }
        public int OfficialLeave { get; set; }
        public int PersonLeave { get; set; }
    }
}