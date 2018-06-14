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

        public string Title { get; set; }

        public int Year { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string FilePath { get; set; }
    }

    [Table("salary_data")]
    public class SalaryData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int SalaryId { get; set; }

        public virtual Salary Salary { get; set; }

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

    public class SalaryHeader
    {
        public int StartRow { get; set; }

        public int RowHeight
        {
            get
            {
                return Columns.Max(c => c.RowSpan);
            }
        }

        public List<SalaryColumn> Columns { get; set; } = new List<SalaryColumn>();
    }

    public class SalaryColumn
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int RowSpan { get; set; } = 1;

        public int ColumnSpan { get; set; } = 1;

        public string Name { get; set; }
    }
}
