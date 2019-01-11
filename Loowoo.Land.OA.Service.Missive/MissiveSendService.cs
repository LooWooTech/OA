using Loowoo.Common;
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
        private bool _stop = false;

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (!_stop)
                {
                    Models.MissiveServiceLog log = null;
                    //查找需要上报的公文，需要上报的公文是需要
                    try
                    {
                        log = Core.MissiveManager.GetLastNeedReportMissiveServiceLog();
                        if (log == null)
                        {
                            Thread.Sleep(1000 * 60);
                            continue;
                        }
                        if (_service == null)
                        {
                            _service = WebServiceManager.GetInstance();
                        }

                        var result = _service.Report(log);
                        var msg = $"[{DateTime.Now}]\t {log.MissiveId}上报{(result ? "成功" : "失败")}\r\n";
                        Console.Write(msg);
                        LogWriter.Instance.WriteLog(msg);
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}{ex.ToJson()}{log.ToJson()}\r\n");
                        Thread.Sleep(1000 * 60);
                    }
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
