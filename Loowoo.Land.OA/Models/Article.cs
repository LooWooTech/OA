using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class Article
    {
        public Article()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public DateTime CreateTime { get; set; }

        public string Description { get; set; }

        public User Creator { get; set; }

        public int Views { get; set; }

        public string DefaultImage { get; set; }

        public bool Checked { get; set; }

        public bool Deleted { get; set; }

        public string Content { get; set; }
    }
}
