using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public static class WordHelper
    {
        public static XWPFDocument CreateDoc(string templatePath)
        {
            using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath), FileMode.Open))
            {
                return new XWPFDocument(fs);
            }
        }

        public static void WriteTitle(this XWPFDocument doc, string title, string styleId = null, ParagraphAlignment alignment = ParagraphAlignment.CENTER)
        {
            var p = doc.CreateParagraph();
            p.Alignment = alignment;
            var r = p.CreateRun();
            r.SetText(title);
            p.Style = styleId;
        }

        public static void WriteContent(this XWPFDocument doc, string content)
        {
            var p = doc.CreateParagraph();
            var r = p.CreateRun();
            r.FontSize = 16;
            r.SetText("\t\t" + content);
        }

        public static Stream GetStream(this XWPFDocument doc)
        {
            var ms = new MemoryStream();
            doc.Write(ms);
            return ms;
        }
    }
}
