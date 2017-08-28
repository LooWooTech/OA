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

            DB.MeetingRooms.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public MeetingRoom Get(int id)
        {
            return DB.MeetingRooms.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            if (DB.FormInfoExtend1s.Any(e => e.InfoId == id))
            {
                throw new Exception("会议室已被使用，无法删除");
            }
            var entity = Get(id);
            entity.Deleted = true;
            DB.SaveChanges();
        }

        public FormInfo Apply(FormInfoExtend1 data)
        {
            var model = Get(data.InfoId);
            var info = new FormInfo
            {
                Title = "申请会议室：" + model.Name + "（" + model.Number + "）",
                FormId = (int)FormType.MeetingRoom,
                PostUserId = data.UserId,
            };
            info.Form = Core.FormManager.GetModel(FormType.MeetingRoom);

            Core.FormInfoManager.Save(info);
            Core.FormInfoExtend1Manager.Apply(info, data);
            return info;
        }

        public void UpdateStatus(int roomId, MeetingRoomStatus status)
        {
            var car = Get(roomId);
            car.Status = status;
            DB.SaveChanges();
        }
    }
}