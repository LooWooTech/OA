using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Missive
{
    public class MissiveSendService
    {
        private Managers.ManagerCore Core = Managers.ManagerCore.Instance;
        private IMissiveWebService _service;
        private Thread _worker;
        private bool _stop = true;

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (!_stop)
                {
                    //查找需要上报的公文，需要上报的公文是需要
                    var log = Core.MissiveManager.GetLastNeedReportMissiveServiceLog();
                    if (log == null)
                    {
                        Thread.Sleep(1000 * 60);
                    }
                    if (_service == null)
                    {
                        _service = WebServiceManager.GetInstance();
                    }

                    var result = _service.Report(log);
                    var msg = $"[{DateTime.Now}]\t {log.MissiveId}上报{(result ? "成功" : "失败")}\r\n";
                    Console.Write(msg);
                    Common.LogWriter.Instance.WriteLog(msg);
                }
            });
            _worker.Start();
        }

        public void Stop()
        {
            _stop = true;
        }
    }
}
