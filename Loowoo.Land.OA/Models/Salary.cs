using Loowoo.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("salary")]
    public class Salary
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Column("data")]
        [Newtonsoft.Json.JsonIgnore]
        public string Json { get; set; }


        private Document _data;
        [NotMapped]
        public Document Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new Document();
                    if (!string.IsNullOrEmpty(Json))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Json);
                        foreach (var kv in data)
                        {
                            _data[kv.Key] = kv.Value;
                        }
                    }
                }
                return _data;
            }
        }
    }

    public enum SalaryDataType
    {
        [Description("正式工")]
        ZSG = 1,
        [Description("临时工")]
        LSG = 2,
        [Description("不动产")]
        BDC = 3
    }

    public class SalaryDataDescriptor
    {
        public SalaryDataType Type { get; set; }

        public int StartRow { get; set; }

        public List<SalaryColumn> Columns { get; set; }
    }

    public class SalaryColumn
    {
        public SalaryColumn(int order, string name)
        {
            Order = order;
            Name = name;
        }

        public int Order { get; set; }

        public string Name { get; set; }
    }
}
