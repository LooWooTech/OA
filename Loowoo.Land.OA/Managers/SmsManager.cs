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
            return DB.Sms.FirstOrDefault();
        }

        public void Delete(int id)
        {
            var sms = DB.Sms.FirstOrDefault(x => x.ID == id);
            if(sms != null)
            {
                DB.Sms.Remove(sms);
                DB.SaveChanges();
            }
        }
        public void Create(Sms sms)
        {
            DB.Sms.AddOrUpdate(sms);
            DB.SaveChanges();
        }

    }
}