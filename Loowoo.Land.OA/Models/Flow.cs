using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class FlowTemplate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int InfoType { get; set; }

        public bool Disabled { get; set; }

        public List<FlowStepTemplate> Steps { get; set; }
    }

    public class FlowStepTemplate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int FlowID { get; set; }

        public int DepartmentID { get; set; }

        public int UserID { get; set; }

        /// <summary>
        /// 属于当前流程第几步
        /// </summary>
        public int Step { get; set; }
    }

    public class Flow
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int InfoID { get; set; }

        public int InfoType { get; set; }

        public List<FlowStep> Steps { get; set; }
    }

    public class FlowStep
    {
        public FlowStep()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int FlowID { get; set; }

        public User User { get; set; }

        public bool? Result { get; set; }

        public string Content { get; set; }

        public int Step { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }


}
