using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class FormInfoParameter
    {
        public FlowStatus[] FlowStatus { get; set; }

        public int UserId { get; set; }

        public bool? Completed { get; set; }

        public int FormId { get; set; }

        public int CategoryId { get; set; }

        public int PostUserId { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public PageParameter Page { get; set; }

        public string SearchKey { get; set; }

        public int[] InfoIds { get; set; }

        public bool? Read { get; set; }

        public bool? Trash { get; set; }

        public bool? Starred { get; set; }
    }
}
