using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
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

        public static void CopyElements(this XWPFDocument doc, XWPFDocument srcDoc)
        {
            var position = doc.Paragraphs.Count;
            foreach (var element in srcDoc.BodyElements)
            {
                switch (element.ElementType)
                {
                    case BodyElementType.PARAGRAPH:
                        var srcPr = (XWPFParagraph)element;
                        doc.CopyStyle(srcDoc, srcDoc.GetStyles().GetStyle(srcPr.StyleID));
                        doc.CreateParagraph();
                        doc.SetParagraph(srcPr, position);
                        break;
                }
                position++;
            }
        }

        public static void ReplaceContent(this XWPFDocument doc, string template, string content)
        {
            foreach (var p in doc.Paragraphs)
            {
                if (p.Text.Contains(template))
                {
                    foreach (var r in p.Runs)
                    {
                        if (r.Text.Contains(template))
                        {
                            r.SetText(r.Text.Replace(template, content));
                        }
                    }
                }
            }
        }

        public static void SaveAs(this XWPFDocument doc, string savePath)
        {
            using (var ms = new MemoryStream())
            {
                doc.Write(ms);
                ms.Flush();
                using (var fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    var data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    data = null;
                }
            }
        }

        private static void CopyStyle(this XWPFDocument srcDoc, XWPFDocument destDoc, XWPFStyle style)
        {
            if (destDoc == null || style == null)
                return;

            if (destDoc.GetStyles() == null)
            {
                destDoc.CreateStyles();
            }

            foreach (var xwpfStyle in srcDoc.GetStyles().GetUsedStyleList(style))
            {
                destDoc.GetStyles().AddStyle(xwpfStyle);
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
