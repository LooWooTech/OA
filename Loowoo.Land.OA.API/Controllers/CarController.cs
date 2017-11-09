using Loowoo.Common;
using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class CarController : ControllerBase
    {
        [HttpGet]
        public object List()
        {
            var list = Core.CarManager.GetList();
            return list.ToList().Select(e => new
            {
                e.Name,
                e.Number,
                e.PhotoId,
                e.Status,
                e.ID,
            });
        }

        [HttpPost]
        public void Save([FromBody]Car data)
        {
            var model = Core.CarManager.Get(data.ID) ?? data;
            model.Name = data.Name;
            model.Number = data.Number;
            model.Type = data.Type;

            Core.CarManager.Save(model);
        }

        [HttpPost]
        public void Apply([FromBody]FormInfoExtend1 data)
        {
            var car = Core.CarManager.Get(data.InfoId);
            if (car == null)
            {
                throw new ArgumentException("参数不正确，没有找该车");
            }
            if (car.Status != CarStatus.Unused)
            {
                throw new Exception("当前车辆在使用中，无法申请");
            }
            if (data.ApprovalUserId == 0)
            {
                throw new Exception("没有选择审核人");
            }
            data.UserId = CurrentUser.ID;
            if (Core.FormInfoExtend1Manager.HasApply(data))
            {
                throw new Exception("你已经申请过该车辆，还未通过审核");
            }
            Core.CarManager.Apply(data);

            var feed = new Feed
            {
                Action = UserAction.Apply,
                Title = "申请用车：" + car.Name + "（" + car.Number + "）",
                InfoId = data.ID,
                Type = FeedType.Flow,
                ToUserId = data.ApprovalUserId,
                FromUserId = CurrentUser.ID,
            };
            Core.FeedManager.Save(feed);
            Core.MessageManager.Add(feed);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.CarManager.Delete(id);
        }
    }
}