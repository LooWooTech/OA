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

        public virtual Form Form { get; set; }

        public string Title { get; set; }

        public string Keywords { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public bool Deleted { get; set; }

        public int PostUserId { get; set; }

        public int FlowDataId { get; set; }

        [JsonIgnore]
        [Column("Data")]
        public string Json { get; set; }

        [NotMapped]
        public object Data
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Json)) return null;
                //if (Form != null)
                //{
                //    var dataType = GetDataType(Form.DataType);
                //    if (dataType != null)
                //    {
                //        return JsonConvert.DeserializeObject(Json, dataType);
                //    }
                //}
                return JsonConvert.DeserializeObject(Json);
            }
            set
            {
                Json = JsonConvert.SerializeObject(value);
            }
        }

        private Type GetDataType(string dataType)
        {
            return Type.GetType(GetType().Namespace + "." + dataType, false, true);
        }

        public void SetData(NameValueCollection values, string dataTypeName)
        {
            var dataType = GetDataType(dataTypeName);
            if (dataType == null) return;

            var obj = Activator.CreateInstance(dataType);
            foreach (var p in dataType.GetProperties())
            {
                foreach (var key in values.AllKeys)
                {
                    if (key.ToLower() == p.Name.ToLower())
                    {
                        var val = values[key];
                        try
                        {
                            var pValue = Convert.ChangeType(val, p.PropertyType);
                            p.SetValue(obj, pValue);
                        }
                        catch { }
                    }
                }

                foreach (var attr in p.GetCustomAttributes())
                {
                    if (attr is FormInfoFieldAttribute)
                    {
                        var name = (attr as FormInfoFieldAttribute).Name;
                        var value = p.GetValue(obj);
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

            Data = obj;
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
