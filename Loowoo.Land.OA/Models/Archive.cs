using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Archive
    {
        public Archive()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Code { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public int ReadTimes { get; set; }

        public ArchiveState State { get; set; }


    }

    public enum ArchiveState
    {

    }
}
