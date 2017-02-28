using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Parameters
{
    public class FileParameter:ParameterBase
    {
        public int? InfoId { get; set; }
        public int? FormId { get; set; }
        public FileType? Type { get; set; }
    }
}
