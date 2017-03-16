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
    [Table("form_info_data")]
    public class FormInfoData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Json { get; set; }

        public T ToObject<T>()
        {
            return Json.ToObject<T>();
        }
    }
}
