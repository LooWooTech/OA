using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class OfficeDocument
    {
        public int ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        public Category Category { get; set; }

        public string Title
        { get; set; }

        public string Organ { get; set; }

        public string FlowStep { get; set; }

        public DateTime SendTime { get; set; }

        public ApproveResult ApproveResult { get; set; } 
    }

    

    public enum ApproveResult
    {
        [Description("未审核")]
        Wait,

        [Description("已通过")]
        Agreed,

        [Description("已退回")]
        Disagreed
    }
}
