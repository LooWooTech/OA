using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public class QrCodeHelper
    {
        public static Bitmap GenerateQrCode(string str, int pixelsPerModule = 20, string darkColor = "#000", string lightColor = "#fff")
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                using (var qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        return qrCode.GetGraphic(pixelsPerModule, darkColor, lightColor);
                    }
                }
            }
        }
    }
}
