using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("freeflow_data")]
    public class FreeFlowData
    {
        public FreeFlowData()
        {
            CreateTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("FlowNodeData")]
        public int ID { get; set; }

        public virtual FlowNodeData FlowNodeData { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        private bool _completed;
        public bool Completed
        {
            get
            {
                if (_completed) return _completed;
                return Nodes != null && Nodes.All(e => e.Submited);
            }
            set
            {
                _completed = value;
            }
        }

        [NotMapped]
        public bool IsEmpty
        {
            get { return Nodes == null || Nodes.Count == 0; }
        }

        public virtual List<FreeFlowNodeData> Nodes { get; set; }

        public int CompletedUserId { get; set; }

        public FreeFlowNodeData GetLastNodeData(int userId)
        {
            return Nodes.OrderByDescending(e => e.ID).FirstOrDefault(e => e.UserId == userId);
        }

        public bool CanSubmit(int userId)
        {
            if (FlowNodeData.UserId == userId)
            {
                return true;
            }
            return !Completed && Nodes.Any(e => e.UserId == userId);
        }
    }
}
