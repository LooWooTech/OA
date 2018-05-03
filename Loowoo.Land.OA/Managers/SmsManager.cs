using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Loowoo.Land.OA.Managers
{
    public class SmsManager : ManagerBase
    {
        public Sms PeekNew()
        {
            return DB.Sms.FirstOrDefault(e => e.SendTime == null);
        }

        public void Create(Sms sms)
        {
            //LogWriter.Instance.WriteLog(sms.ToJson() + "\r\n", "sms");
            var entity = DB.Sms.FirstOrDefault(e => e.Content == sms.Content && e.Numbers == sms.Numbers);
            if (entity == null)
            {
                DB.Sms.Add(sms);
                DB.SaveChanges();
            }
        }

        public void SetSentStatus(Sms sms)
        {
            var entity = DB.Sms.FirstOrDefault(e => e.ID == sms.ID);
            entity.SendTime = DateTime.Now;
            entity.MessageID = sms.MessageID;
            DB.SaveChanges();
        }
    }
}