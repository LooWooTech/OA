using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Attendance
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        private AttendanceService _service = new AttendanceService();

        public void Start()
        {
            OnStart(null);
        }

        protected async override void OnStart(string[] args)
        {
            while (true)
            {
                await _service.Start();
                Thread.Sleep(10);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
