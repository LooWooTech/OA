using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.TaskClient.Models
{
    public class Config
    {
        public static readonly int MaxFontSize = 30;
        public static readonly int MaxRows = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxRows"] ?? "8");
        public static readonly int RowHeight = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowHeight"] ?? "55");
        public static readonly int RowLength1 = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowLength1"] ?? "15");
        public static readonly int RowLength2 = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RowLength2"] ?? "20");
        public static readonly int FontSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["FontSize"] ?? "20");
        public static readonly int PlaySeconds = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PlaySeconds"] ?? "10");
    }
}
