using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Attendance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if DEBUG
            new Service1().Start();
#else
            ServiceBase.Run(new[] { new Service1() });
#endif
        }
    }
}
