using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class MeetingRoomManager : ManagerBase
    {
        public IEnumerable<MeetingRoom> GetList()
        {
            return DB.MeetingRooms.Where(e => !e.Deleted);
        }

        public void Save(MeetingRoom model)
        {
            model.Number = model.Number.ToUpper();
            if (DB.MeetingRooms.Any(e => e.Number == model.Number && (e.ID == 0 || e.ID != model.ID)))
            {
                throw new Exception("该车牌号已被使用");
            }

            DB.MeetingRooms.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public MeetingRoom Get(int id)
        {
            return DB.MeetingRooms.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            if (DB.FormInfoApplies.Any(e => e.InfoId == id))
            {
                throw new Exception("车辆已被使用，无法删除");
            }
            var entity = Get(id);
            entity.Deleted = true;
            DB.SaveChanges();
        }
    }
}