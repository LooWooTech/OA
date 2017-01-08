using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public class LogWriter
    {
        public static readonly LogWriter Instance = new LogWriter();

        private string GetFilePath(string filePrefix = null)
        {
            var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            return Path.Combine(dirPath, filePrefix + DateTime.Now.ToString("yyyyMMdd")) + ".txt";
        }

        public void WriteLog(string content, string filePrefix = null)
        {
            if(string.IsNullOrEmpty(content))
            {
                return;
            }
            var filePath = GetFilePath(filePrefix);
            File.AppendAllText(filePath, content);
        }
    }
}
