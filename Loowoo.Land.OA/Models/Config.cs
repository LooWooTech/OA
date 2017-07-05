using Loowoo.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    [Table("config")]
    public class Config
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
