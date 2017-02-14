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

        public void WriteException(Exception ex,string operateName)
        {
            WriteLog(string.Format("进行操作：{0} 发生错误，错误信息：Ex.Message:{1};Ex.Stacktrace:{2}",operateName, ex.Message, ex.StackTrace));
        }
    }
}
