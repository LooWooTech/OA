using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public class ValidateCode
    {
        public ValidateCode()
        {
            Width = 180;
            Height = 60;
            Length = 5;
        }
        public int Width { get; set; }

        public int Height { get; set; }

        public int Length { get; set; }

        private static readonly Random Rnd = new Random();
        private static readonly string BaseStr = "ABCDEFGHKMNPQRSUWXY";
        public string GenerateCode()
        {
            var code = string.Empty;
            var maxVal = BaseStr.Length;
            for (var i = 0; i < Length; i++)
            {
                code += BaseStr[Rnd.Next(0, maxVal)];
            }
            return code;
        }

        private string[] _fontNames = new[] { "Arial Black" };
        private int[] _fontSizes = new[] { 24, 20 };
        private Font GetFont()
        {
            var name = _fontNames[Rnd.Next(_fontNames.Length)];
            return new Font(name, _fontSizes[Rnd.Next(_fontSizes.Length)]);
        }

        private Color _fontBackground = Color.FromArgb(0, 0xff, 0xff, 0xff);

        private Color _fontColor1 = Color.FromArgb(0xff, 0x66, 0x66, 0x66);
        private Color _fontColor2 = Color.FromArgb(0xff, 0xcc, 0xcc, 0xcc);

        private Bitmap GenerateChar(string str)
        {
            var bmp = new Bitmap((Width - 10) / Length, Height - 20);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(_fontBackground);
                var fontColor = Rnd.Next(2) == 1 ? _fontColor1 : _fontColor2;
                var bgColor = fontColor == _fontColor1 ? _fontColor2 : _fontColor1;
                var pen = new Pen(bgColor);
                var font = GetFont();
                graphics.DrawString(str, font, pen.Brush, 1, 1);
                pen.Color = fontColor;
                graphics.DrawString(str, font, pen.Brush, 0, 0);
            }
            return bmp;
        }

        private Color[] _backGroupColor = new[] { Color.FromArgb(0xff, 0x33, 0x33, 0x33), Color.FromArgb(0xff, 0x66, 0x66, 0x66), Color.FromArgb(0xff, 0xcc, 0xcc, 0xcc) };
        private Bitmap GenerateBackground()
        {
            var length = 6;
            var tileWidth = Height / length;
            var backgroundImage = new Bitmap(Width, Height);

            using (var g = Graphics.FromImage(backgroundImage))
            {
                for (var i = 0; i < Width / tileWidth + 1; i++)
                {
                    for (var j = 0; j < length; j++)
                    {
                        var tile = new Bitmap(tileWidth, tileWidth);
                        using (var g1 = Graphics.FromImage(tile))
                        {
                            g1.Clear(_backGroupColor[Rnd.Next(_backGroupColor.Length)]);
                        }
                        g.DrawImage(tile, new Point(i * tileWidth, j * tileWidth));
                    }
                }
            }
            return backgroundImage;
        }

        public byte[] GenerateImage(string code)
        {
            var backgroundImage = GenerateBackground();

            using (var g = Graphics.FromImage(backgroundImage))
            {
                for (var i = 0; i < code.Length; i++)
                {
                    var codeImage = GenerateChar(code[i].ToString());
                    g.DrawImage(codeImage, new Point(i * codeImage.Width + 10, 10));
                }
            }

            using (var ms = new MemoryStream())
            {
                backgroundImage.Save(ms, ImageFormat.Jpeg);

                return ms.GetBuffer();
            }

        }

    }
}
