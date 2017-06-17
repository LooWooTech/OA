using Loowoo.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("form_info")]
    public class FormInfo
    {
        public FormInfo()
        {
            CreateTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int FormId { get; set; }

        public int ExtendId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }

        public int PostUserId { get; set; }

        public int FlowDataId { get; set; }

        public virtual FlowData FlowData { get; set; }

        /// <summary>
        /// 当前办理的步骤，可以是人名或节点名称，每次创建新的节点，需要更新，如果完成
        /// </summary>
        private string _flowStep;
        public string FlowStep
        {
            get
            {
                if (FlowDataId == 0)
                {
                    return "未提交";
                }
                if (string.IsNullOrEmpty(_flowStep))
                {
                    if (FlowDataId == 0)
                    {
                        return "未提交";
                    }
                }
                return _flowStep;
            }
            set
            {
                _flowStep = value;
            }
        }

        public string Title { get; set; }
    }

}
