using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("form_info")]
    public class FormInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int FormId { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }

        public int PostUserId { get; set; }

        public int FlowDataId { get; set; }

        public virtual FormInfoData InfoData { get; set; }
    }

    public class FormInfoParameter
    {
        public int FormId { get; set; }

        public string SearchKey { get; set; }

        public int CategoryId { get; set; }

        public int PostUserId { get; set; }

        public PageParameter Page { get; set; }
    }
}
