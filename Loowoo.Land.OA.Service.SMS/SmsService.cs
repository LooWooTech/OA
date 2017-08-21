using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.SMS
{
    public class SmsService
    {
        private Managers.ManagerCore Core = Managers.ManagerCore.Instance;
        private bool _stop = false;
        private Thread _worker;

        private readonly string _applicationCode = ConfigurationManager.AppSettings["ApplicationID"];
        private readonly string _password = ConfigurationManager.AppSettings["Password"];
        private readonly string _extendCode = ConfigurationManager.AppSettings["ExtendCode"];
        private readonly string _masAddress = ConfigurationManager.AppSettings["MASAddress"];

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (!_stop)
                {
                    try
                    {
                        Dowork();
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}\r\n{ex.StackTrace}\r\n");
                    }
                }
            });
            _worker.Start();
        }

        public void Stop()
        {
            _stop = true;
            if (_worker.Join(500) == false)
            {
                _worker.Abort();
                _worker = null;
            }
        }

        private void Dowork()
        {
            var sms = Core.SmsManager.PeekNew();
            if (sms != null)
            {
                try
                {
                    SendSms(sms);
                }
                catch (Exception ex)
                {
                    LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t发送短信失败：{sms.Content}===>{sms.Numbers}\r\n{ex}\r\n");
                }

                Core.SmsManager.Delete(sms);
            }
        }
        private OpenMas.Sms _client = new OpenMas.Sms(ConfigurationManager.AppSettings["MASAddress"]);

        private void SendSms(Sms sms)
        {
            var mobileList = sms.Numbers.Trim().Split(',');
            var msgID = _client.SendMessage(mobileList, sms.Content, _extendCode, _applicationCode, _password);
            LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t发送短信成功：{sms.Content}===>{sms.Numbers}\r\n");
        }
    }

}
