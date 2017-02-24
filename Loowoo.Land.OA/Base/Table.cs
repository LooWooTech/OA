using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Base
{
    public class Table<T>
    {
        public T[] List { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public int Total { get; set; }
    }
}
