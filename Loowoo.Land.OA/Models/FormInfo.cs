using Loowoo.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public virtual Form Form { get; set; }

        public string Title { get; set; }

        public string Keywords { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }

        public int PostUserId { get; set; }

        public int FlowDataId { get; set; }

        public virtual FormInfoData Data { get; set; }

        public void UpdateFileds()
        {
            if (Data == null || Form == null) return;

            var data = Data.ToObject(Form.DataType);
            var dataType = Type.GetType(this.GetType().Namespace + "." + Form.DataType,false,true);
            if (dataType == null) return;

            foreach (var p in dataType.GetProperties())
            {
                foreach (var attr in p.GetCustomAttributes())
                {
                    if (attr is FormInfoFieldAttribute)
                    {
                        var name = (attr as FormInfoFieldAttribute).Name;
                        var value = p.GetValue(data);
                        switch (name.ToLower())
                        {
                            case "title":
                                Title = value.ToString();
                                break;
                            case "keywords":
                                Keywords += value.ToString() + ",";
                                break;
                            case "categoryid":
                                CategoryId = int.Parse(value.ToString());
                                break;
                        }
                    }
                }
            }
        }
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
