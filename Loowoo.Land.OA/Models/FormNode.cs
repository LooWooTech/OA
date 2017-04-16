using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Models
{
    public class FormNode
    {
        public int ID { get; set; }

        public int FormId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string HelpText { get; set; }

        public string ErrorText { get; set; }

        public FormNodeType Type { get; set; }

        public bool Required { get; set; }

        public string Pattern { get; set; }

        public int Row { get; set; }

        public int Cell { get; set; }
    }

    public enum FormNodeType
    {

    }
}
