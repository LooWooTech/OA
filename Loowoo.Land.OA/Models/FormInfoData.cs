using Loowoo.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("form_info_data")]
    public class FormInfoData
    {
        public FormInfoData() { }

        public FormInfoData(NameValueCollection values)
        {
            var doc = new Common.Document();
            foreach (var key in values.AllKeys)
            {
                doc[key] = values[key];
            }
            Json = doc.ToJson();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, ForeignKey("Info")]
        public int InfoId { get; set; }

        public virtual FormInfo Info { get; set; }

        [JsonIgnore]
        public string Json { get; set; }

        [NotMapped]
        public object Data
        {
            get
            {
                return JsonConvert.DeserializeObject(Json);
            }
        }

        public object ToObject(string typeName)
        {
            var type = Type.GetType(this.GetType().Namespace + "." + typeName);
            return JsonConvert.DeserializeObject(Json, type);
        }

        public T ToObject<T>()
        {
            return Json.ToObject<T>();
        }
    }
}
