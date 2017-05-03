using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Models
{
    public class CarApply
    {
        public int CarId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        public int UserId { get; set; }
    }
}