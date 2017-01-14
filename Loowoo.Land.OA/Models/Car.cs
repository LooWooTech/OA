using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Car
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public CarType Type { get; set; }

        public CarState State { get; set; }

    }

    public class CarEventLog
    {
        public int ID { get; set; }

        public int CarID { get; set; }

        public int UserID { get; set; }

        public string Description { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime? EndTime { get; set; }
    }

    public enum CarType
    {
        [Description("越野车")]
        Suv,
        [Description("轿车")]
        Sedan
    }

    public enum CarState
    {
        [Description("闲置")]
        Unused,
        [Description("使用中")]
        Using,
        [Description("维修中")]
        Repair
    }
}
